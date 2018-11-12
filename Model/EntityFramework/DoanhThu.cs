namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoanhThu")]
    public partial class DoanhThu
    {
        public int DoanhThuID { get; set; }

        public int DonHangID { get; set; }

        public DateTime NgayTao { get; set; }

        public decimal SoTien { get; set; }

        public virtual DonHang DonHang { get; set; }
    }
}
