//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DTO.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Status
    {
        public Status()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int SttID { get; set; }
        public string SttName { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
    }
}
