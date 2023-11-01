using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
   
    public record ApartmentDto
    {
        public int Id { get; init; }
        public string Status { get; init; }
        public string Type { get; init; }
        public int Floor { get; init; }
        public int No { get; init; }
    }
}
