using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure
{
    public class Paged<T>
    {
        public Paged(IEnumerable<T> items,
            int totalNumberOfRecords,
            int pageSize,
            int pageNumber)
        {

            Items = new List<T>(items);
            TotalNumberOfRecords = totalNumberOfRecords;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalNumberOfPages = GetTotalNumberOfPages(totalNumberOfRecords);

        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int TotalNumberOfRecords { get; set; }
        public IEnumerable<T> Items { get; set; }

        private int GetTotalNumberOfPages(int totalNumberOfRecords)
            => (int) Math.Ceiling(totalNumberOfRecords / (float)PageSize);
    }
}
