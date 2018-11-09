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

        public int DonHangID { get; set; }

        public double SoSao { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DonHang DonHang { get; set; }
    }
}
