namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuNangTin")]
    public partial class LichSuNangTin
    {
        [Key]
        public int NangTinID { get; set; }

        public int SachID { get; set; }

        public DateTime NgayNang { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
