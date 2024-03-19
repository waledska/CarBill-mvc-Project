using System;
using System.Collections.Generic;

namespace CarBill.bussinesData.Models
{
    public partial class BillRowImage
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; } = null!;
        public int BillRowId { get; set; }

        public virtual BillRow BillRow { get; set; } = null!;
    }
}
