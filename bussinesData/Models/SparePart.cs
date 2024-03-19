using System;
using System.Collections.Generic;

namespace CarBill.bussinesData.Models
{
    public partial class SparePart
    {
        public SparePart()
        {
            billRows = new HashSet<BillRow>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal PriceOfInstaling { get; set; }
        public string? ImageUrl { get; set; }
        public int TypeId { get; set; }
        public decimal Price { get; set; }

        public virtual MaintananceType Type { get; set; } = null!;
        public virtual ICollection<BillRow> billRows { get; set; }
    }
}
