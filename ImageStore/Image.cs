//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImageStore
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class Image
    {
        public short Id { get; set; }
        public short UserId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public System.DateTime Created { get; set; }
    
        public virtual User User { get; set; }
    }
}
