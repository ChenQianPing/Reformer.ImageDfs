using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageDfs.Domain.Entities;

namespace ImageDfs.Domain.Repositories
{
    public interface IImageDfsRepository
    {
        List<ImageSvr> GetAllUseableServers();
        ImageSvr GetServerInfoById(string serverId);

        ImageStatusEnum CreateImageInfo(ImageInfo imageEntity);
    }
}
