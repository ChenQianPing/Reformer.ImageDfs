using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageDfs.Domain.Entities;

namespace ImageDfs.Infrastructure.Mapping
{
    public class ImageSvrMap : EntityTypeConfiguration<ImageSvr>
    {
        public ImageSvrMap()
        {
            // Primary Key
            this.HasKey(t => t.SvrId);

            // Table & Column Mappings
            this.ToTable("T_SYS_IMAGE_SVR");

            this.Property(t => t.SvrId).HasColumnName("SVR_ID");
            this.Property(t => t.SvrName).HasColumnName("SVR_NAME");
            this.Property(t => t.SvrUrl).HasColumnName("SVR_URL");
            this.Property(t => t.PicRootPath).HasColumnName("PIC_ROOT_PATH");
            this.Property(t => t.MaxPicAmount).HasColumnName("MAX_PIC_AMOUNT");
            this.Property(t => t.CurPicAmount).HasColumnName("CUR_PIC_AMOUNT");
            this.Property(t => t.FlgUsable).HasColumnName("FLG_USABLE");
        }
    }
}
