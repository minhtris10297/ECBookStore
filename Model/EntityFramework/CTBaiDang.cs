namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTBaiDang")]
    public partial class CTBaiDang
    {
        [Key]
        public int MaCTBD { get; set; }

        public int? MaBaiDang { get; set; }

        [StringLength(50)]
        public string TieuDe { get; set; }

        [StringLength(100)]
        public string HinhAnh { get; set; }

        [StringLength(500)]
        public string NoiDung { get; set; }
    }
}
