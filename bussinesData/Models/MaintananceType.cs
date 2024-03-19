using System;
using System.Collections.Generic;

namespace CarBill.bussinesData.Models
{
    public partial class MaintananceType
    {
        public MaintananceType()
        {
            SpareParts = new HashSet<SparePart>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<SparePart> SpareParts { get; set; }
    }
}
