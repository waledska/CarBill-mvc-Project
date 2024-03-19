using System;
using System.Collections.Generic;

namespace CarBill.bussinesData.Models
{
    public partial class BillRow
    {
        public BillRow()
        {
            BillRowImages = new HashSet<BillRowImage>();
            BillsParts = new HashSet<BillsPart>();
        }

        public int Id { get; set; }
        public int Amount { get; set; }
        public decimal TotalPriceForBillRow { get; set; }
        public int BillId { get; set; }

        public virtual Bill Bill { get; set; } = null!;
        public virtual ICollection<BillRowImage> BillRowImages { get; set; }
        public virtual ICollection<BillsPart> BillsParts { get; set; }
    }
}
