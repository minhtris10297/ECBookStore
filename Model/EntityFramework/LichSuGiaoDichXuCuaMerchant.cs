namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuGiaoDichXuCuaMerchant")]
    public partial class LichSuGiaoDichXuCuaMerchant
    {
        public int MerchantID { get; set; }

        [Key]
        public int LichSuGiaoDichXuID { get; set; }

        [Required]
        public string PhuongThucSuDung { get; set; }

        public DateTime NgayGiaoDich { get; set; }

        public int SoXu { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}
