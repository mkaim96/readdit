using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Readdit.Infrastructure.Application.SubReaddit.Commands.CreateSubReaddit
{
    public class CreateSubReadditCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
