namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NangTin")]
    public partial class NangTin
    {
        [Key]
        public int MaLuotNang { get; set; }

        public int MaBaiDang { get; set; }

        public int MaMer { get; set; }
    }
}
