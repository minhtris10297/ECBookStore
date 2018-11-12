namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuCustomer")]
    public partial class LichSuCustomer
    {
        public int LichSuCustomerID { get; set; }

        public int CustomerID { get; set; }

        public int DonHangID { get; set; }

        public decimal TongTien { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DonHang DonHang { get; set; }
    }
}
