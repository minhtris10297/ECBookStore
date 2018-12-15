namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        public int AdminID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(256)]
        public string MatKhauMaHoa { get; set; }

        [Required]
        [StringLength(50)]
        public string TenHienThi { get; set; }

        public bool TrangThai { get; set; }
    }
}
