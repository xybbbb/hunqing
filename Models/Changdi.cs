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
    
    public partial class Changdi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Changdi()
        {
            this.Taocan = new HashSet<Taocan>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Addr { get; set; }
        public int type { get; set; }
    
        public virtual type_FengGe type_FengGe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Taocan> Taocan { get; set; }
    }
}
