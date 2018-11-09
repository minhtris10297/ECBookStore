namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuMer")]
    public partial class LichSuMer
    {
        public int LichSuMerID { get; set; }

        public int MerchantID { get; set; }

        public int DonHangID { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTien { get; set; }

        public virtual DonHang DonHang { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}
