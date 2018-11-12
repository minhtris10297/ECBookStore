namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuMerchant")]
    public partial class LichSuMerchant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LichSuMerchantID { get; set; }

        public decimal TongTien { get; set; }

        public int MerchantID { get; set; }

        public int DonHangID { get; set; }

        public virtual DonHang DonHang { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}
