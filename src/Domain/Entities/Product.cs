using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public required string Name { get; set; }
        public required DateTime ProduceDate { get; set; }
        public string? ManufacturePhone { get; set; }
        public required string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; }


        public virtual User User { get; set; } = null!;// Navigate property    
    }
}
