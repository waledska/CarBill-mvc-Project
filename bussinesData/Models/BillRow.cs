using System;
using System.Collections.Generic;

namespace CarBill.bussinesData.Models
{
    public partial class BillRow
    {
        public BillRow()
        {
            BillRowImages = new HashSet<BillRowImage>();
        }

        public int Id { get; set; }
        public int Amount { get; set; }
        public decimal TotalPriceForBillRow { get; set; }
        public int BillId { get; set; }
        public int sparePartId { get; set; }

        public virtual Bill Bill { get; set; } = null!;
        public virtual SparePart sparePart { get; set; } = null!;
        public virtual ICollection<BillRowImage> BillRowImages { get; set; }
    }
}
