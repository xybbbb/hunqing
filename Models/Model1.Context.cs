﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WeddingDBEntities : DbContext
    {
        public WeddingDBEntities()
            : base("name=WeddingDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Changdi> Changdi { get; set; }
        public virtual DbSet<dingdan> dingdan { get; set; }
        public virtual DbSet<Hote> Hote { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Taocan> Taocan { get; set; }
        public virtual DbSet<tpye_lei> tpye_lei { get; set; }
        public virtual DbSet<type_FengGe> type_FengGe { get; set; }
        public virtual DbSet<type_Hote> type_Hote { get; set; }
        public virtual DbSet<type_y> type_y { get; set; }
        public virtual DbSet<UserTB> UserTB { get; set; }
        public virtual DbSet<Yuan> Yuan { get; set; }
        public virtual DbSet<Shouyi> Shouyi { get; set; }
    }
}