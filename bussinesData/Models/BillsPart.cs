using System;
using System.Collections.Generic;

namespace CarBill.bussinesData.Models
{
    public partial class BillsPart
    {
        public int Id { get; set; }
        public int BillRowId { get; set; }
        public int SparePartId { get; set; }

        public virtual BillRow BillRow { get; set; } = null!;
        public virtual SparePart SparePart { get; set; } = null!;
    }
}
