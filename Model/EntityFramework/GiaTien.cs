namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GiaTien")]
    public partial class GiaTien
    {
        public int GiaTienID { get; set; }

        [Required]
        [StringLength(150)]
        public string TenGiaTien { get; set; }

        public decimal TyGia { get; set; }

        public DateTime NgayTao { get; set; }
    }
}
