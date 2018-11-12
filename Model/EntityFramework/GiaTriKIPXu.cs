namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GiaTriKIPXu")]
    public partial class GiaTriKIPXu
    {
        [Key]
        public int XuID { get; set; }

        public decimal? GiaTriXu { get; set; }
    }
}
