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
    
    public partial class Yuan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Yuan()
        {
            this.Taocan = new HashSet<Taocan>();
            this.Taocan1 = new HashSet<Taocan>();
            this.Taocan2 = new HashSet<Taocan>();
            this.Taocan3 = new HashSet<Taocan>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public Nullable<int> Age { get; set; }
        public int typeid { get; set; }
        public Nullable<decimal> price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Taocan> Taocan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Taocan> Taocan1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Taocan> Taocan2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Taocan> Taocan3 { get; set; }
        public virtual type_y type_y { get; set; }
    }
}
