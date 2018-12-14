namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuHoaHong")]
    public partial class LichSuHoaHong
    {
        public int LichSuHoaHongID { get; set; }

        public int ChiTietDonHangID { get; set; }

        public DateTime ThoiDiem { get; set; }

        public int GiaTriXu { get; set; }

        public virtual ChiTietDonHang ChiTietDonHang { get; set; }
    }
}
