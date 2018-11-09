namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LichSuCu
    {
        [Key]
        public int LichSuCusID { get; set; }

        public int CustomerID { get; set; }

        public int DonHangID { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTien { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DonHang DonHang { get; set; }
    }
}
