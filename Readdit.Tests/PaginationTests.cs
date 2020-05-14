using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using Readdit.Domain.Models;
using Readdit.Infrastructure;
using Readdit.Infrastructure.Application.Links.Queries.GetLinksList;
using Readdit.Infrastructure.Dto;
using Readdit.Infrastructure.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Readdit.Tests
{
    public class PaginationTests
    {
        public IMapper Mapper { get; set; }
        public List<Link> Links { get; set; }
        public PaginationTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new Mappings()));
            Mapper = new Mapper(config);

            Links = new List<Link>
            {
                new Link{ Id = 1, Ups = 5, Downs = 0},
                new Link{ Id = 2, Ups = 4, Downs = 0},
                new Link{ Id = 3, Ups = 3, Downs = 0},
                new Link{ Id = 4, Ups = 2, Downs = 0},
                new Link{ Id = 5, Ups = 1, Downs = 0},
            };
        }
        [Theory]
        [InlineData(20, 5, 4)]
        [InlineData(10, 4, 3)]
        [InlineData(3, 2, 2)]
        [InlineData(3, 4, 1)]
        public void should_properly_calculate_number_of_pages(int totalNumberOfRecords, int pageSize, int expectedPageSize)
        {
            var pagedLinks = new Paged<Link>(Links, totalNumberOfRecords, pageSize, 1);

            Assert.Equal(pagedLinks.TotalNumberOfPages, expectedPageSize);
        }

        [Fact]
        public async Task GetPagedLinkListHandler_page_size_and_page_number_should_be_the_same_as_in_command()
        {
            var command = new GetPagedLinkList { Page = 2, PageSize = 2 };
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("TestDatabase");

            using (var context = new ApplicationDbContext(builder.Options))
            {
                context.Links.AddRange(Links);
                context.SaveChanges();

                var handler = new GetPagedLinkListHandler(context, Mapper);
                var result = await handler.Handle(command, new CancellationToken());

                Assert.Equal(command.PageSize, result.PageSize);
                Assert.Equal(command.Page, result.PageNumber);
            }
        }

        [Fact]
        public async Task GetPagedSubreadditLinkListHandler_should_return_links_only_from_specified_subreaddit()
        {
            var command = new GetPagedSubReadditLinks { Page = 1, PageSize = 2, SubReadditName = "test" };
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("TestDatabase");

            using (var context = new ApplicationDbContext(builder.Options))
            {
                context.Links.AddRange(Links);
                var link = new Link
                {
                    Id = 6,
                    SubReaddit = new SubReaddit("test", new ApplicationUser(), "test subreddit")
                };

                context.Links.Add(link);

                context.SaveChanges();

                var handler = new GetSubReadditLinksHandler(context, Mapper);
                var result = await handler.Handle(command, new CancellationToken());

                Assert.Single(result.Items);
            }
        } 
    }
}
