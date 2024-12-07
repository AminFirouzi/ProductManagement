using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public string ManufactureEmail { get; set; }
        public DateTime ProduceDate { get; set; }
    }
}
