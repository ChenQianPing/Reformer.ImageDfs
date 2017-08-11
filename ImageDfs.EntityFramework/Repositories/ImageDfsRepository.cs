using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageDfs.Domain.Entities;
using ImageDfs.Domain.Repositories;

namespace ImageDfs.Infrastructure.Repositories
{
    public class ImageDfsRepository : IImageDfsRepository
    {
        private readonly EfDbContext _efDbContext;

        public ImageDfsRepository()
        {
            _efDbContext = new EfDbContext();
        }

        public ImageStatusEnum CreateImageInfo(ImageInfo imageEntity)
        {
            // 首先是图片信息表
            _efDbContext.ImageInfos.Add(imageEntity);

            // 其次是图片服务器信息表
            var serverEntity = _efDbContext.ImageSvrs
                .FirstOrDefault(s => s.SvrId == imageEntity.ImageSvrId);

            if (serverEntity != null)
            {
                // 当前服务器存储数量+1
                serverEntity.CurPicAmount += 1;
            }

            // 一起提交到SQL Server数据库中
            var result = _efDbContext.SaveChanges();

            return result > 0 ? ImageStatusEnum.Successful : ImageStatusEnum.Failure;


        }

        public List<ImageSvr> GetAllUseableServers()
        {
            var serverList = _efDbContext.ImageSvrs
                .Where<ImageSvr>(s => s.FlgUsable == 1
                                      && s.CurPicAmount < s.MaxPicAmount).ToList();

            return serverList;
        }

        public ImageSvr GetServerInfoById(string serverId)
        {
            var serverEntity = _efDbContext.ImageSvrs.FirstOrDefault(s => s.SvrId == serverId);

            return serverEntity;
        }
    }
}
