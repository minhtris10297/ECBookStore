namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiDang")]
    public partial class BaiDang
    {
        [Key]
        public int MaBaiDang { get; set; }

        public int? MaMer { get; set; }

        public int? ViTri { get; set; }
    }
}
