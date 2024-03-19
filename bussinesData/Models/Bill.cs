using System;
using System.Collections.Generic;

namespace CarBill.bussinesData.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillRows = new HashSet<BillRow>();
        }

        public int Id { get; set; }
        public string? LastReading { get; set; }
        public string? MaintancePeriod { get; set; }
        public string? Comment { get; set; }
        public decimal TotalPrice { get; set; }
        public int? DicountPrecentage { get; set; }
        public string? VideoUrl { get; set; }
        public int CarId { get; set; }

        public virtual Car Car { get; set; } = null!;
        public virtual ICollection<BillRow> BillRows { get; set; }
    }
}
