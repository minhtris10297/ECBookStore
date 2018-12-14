namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaHong")]
    public partial class HoaHong
    {
        public int HoaHongID { get; set; }

        public int PhanTranHoaHong { get; set; }

        public DateTime NgayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        public bool TrangThai { get; set; }
    }
}
