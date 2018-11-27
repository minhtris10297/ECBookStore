namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HinhAnh")]
    public partial class HinhAnh
    {
        public int HinhAnhID { get; set; }

        public int SachID { get; set; }

        [Required]
        [StringLength(500)]
        public string DuongDan { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
