namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhGiaCuaMerchant")]
    public partial class DanhGiaCuaMerchant
    {
        [Key]
        public int DanhGiaMerID { get; set; }

        public int MerchantID { get; set; }

        public int CustomerID { get; set; }

        public DateTime? NgayDanhGia { get; set; }

        [StringLength(1000)]
        public string NoiDung { get; set; }

        public double SoSao { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}
