using MediatR;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Readdit.Infrastructure.Application.SubReaddits.Commands.CreateSubReaddit
{
    public class CreateSubReadditCommand : IRequest<int>
    {
        public string Name { get; set; }
        public ApplicationUser User { get; set; }
    }
}
