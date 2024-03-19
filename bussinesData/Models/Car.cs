using System;
using System.Collections.Generic;

namespace CarBill.bussinesData.Models
{
    public partial class Car
    {
        public Car()
        {
            Bills = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public string PlateNum { get; set; } = null!;
        public string? Brand { get; set; }
        public string? Color { get; set; }
        public string? Symbol { get; set; }
        public string? LastReading { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
