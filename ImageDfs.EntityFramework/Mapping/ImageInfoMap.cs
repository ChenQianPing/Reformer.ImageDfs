using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageDfs.Domain.Entities;

namespace ImageDfs.Infrastructure.Mapping
{
    public class ImageInfoMap : EntityTypeConfiguration<ImageInfo>
    {
        public ImageInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.ImageId);

            // Table & Column Mappings
            this.ToTable("T_SYS_IMAGE_INFO");

            this.Property(t => t.ImageId).HasColumnName("IMAGE_ID");
            this.Property(t => t.ImageName).HasColumnName("IMAGE_NAME");
            this.Property(t => t.ImageSvrId).HasColumnName("SVR_ID");
        }
    }
}
