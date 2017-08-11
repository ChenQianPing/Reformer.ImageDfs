using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageDfs.Domain.Entities;
using ImageDfs.Infrastructure.Mapping;

namespace ImageDfs.Infrastructure
{
    public class EfDbContext : DbContext
    {

        public Guid Guid { get; set; }

        public EfDbContext()
            : base("name=DefaultDBConnection")
        {
            Guid = Guid.NewGuid();
            Database.SetInitializer<EfDbContext>(null);
            this.Database.Log = new Action<string>(q => Debug.WriteLine(q));
        }


        public DbSet<ImageInfo> ImageInfos { get; set; }
        public DbSet<ImageSvr> ImageSvrs { get; set; }

 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 设置禁用一对多级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // 设置Schema
            modelBuilder.HasDefaultSchema("BOEREMS");

            modelBuilder.Configurations.Add(new ImageInfoMap());
            modelBuilder.Configurations.Add(new ImageSvrMap());

        }

    }
}
