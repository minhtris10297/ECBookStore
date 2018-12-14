namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhGiaCuaCustomer")]
    public partial class DanhGiaCuaCustomer
    {
        [Key]
        public int DanhGiaCusID { get; set; }

        public int CustomerID { get; set; }

        public int ChiTIetDonHangID { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        public int SachID { get; set; }

        [Required]
        public string TieuDe { get; set; }

        public double SoSao { get; set; }

        public virtual ChiTietDonHang ChiTietDonHang { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
