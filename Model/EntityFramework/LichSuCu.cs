//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class LichSuCu
    {
        public int LichSuCusID { get; set; }
        public int CustomerID { get; set; }
        public int DonHangID { get; set; }
        public Nullable<decimal> TongTien { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual DonHang DonHang { get; set; }
    }
}
