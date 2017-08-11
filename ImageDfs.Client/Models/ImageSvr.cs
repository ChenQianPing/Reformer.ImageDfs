using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDfs.Client.Models
{
    public class ImageSvr
    {
        public string SvrId { get; set; }
        public string SvrName { get; set; }
        public string SvrUrl { get; set; }
        public string PicRootPath { get; set; }
        public int MaxPicAmount { get; set; }
        public int CurPicAmount { get; set; }
        public int FlgUsable { get; set; }
    }
}
