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
    
    public partial class Rating
    {
        public int ProID { get; set; }
        public Nullable<int> Two { get; set; }
        public Nullable<int> Three { get; set; }
        public Nullable<int> Four { get; set; }
        public Nullable<int> Five { get; set; }
        public Nullable<int> One { get; set; }
        public Nullable<int> Rate { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
