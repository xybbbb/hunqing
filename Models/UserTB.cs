//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeddingProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserTB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserTB()
        {
            this.dingdan = new HashSet<dingdan>();
            this.Images = new HashSet<Images>();
            this.Taocan = new HashSet<Taocan>();
        }
    
        public int UserID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Age { get; set; }
        public string Sex { get; set; }
        public string Nubler { get; set; }
        public string pwd { get; set; }
        public string img { get; set; }
        public string Addr { get; set; }
        public Nullable<decimal> Charge { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dingdan> dingdan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Images> Images { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Taocan> Taocan { get; set; }
    }
}
