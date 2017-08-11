using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ImageDfs.Domain.Entities;
using ImageDfs.Infrastructure.Repositories;

namespace ImageDfs.WebApi.Controllers
{
    public class FileUploadController : ApiController
    {
        private readonly ImageDfsRepository _imageDfsRepository;

        public FileUploadController()
        {
            // 这里可以借助IoC实现依赖注入
            this._imageDfsRepository = new ImageDfsRepository();
        }

        [Route("api/Dfs/UploadTmplImg")]
        [HttpPost]
        public void UploadTmplImg(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            // 接收文件的扩展名
            var fileExt = context.Request["ext"];

            if (string.IsNullOrEmpty(fileExt) || string.IsNullOrEmpty(context.Request["serverId"]))
            {
                return;
            }

            // 图片所在的服务器的编号
            var serverId = Convert.ToInt32(context.Request["serverId"]);

            // 图片要存放的物理路径
            var imageDir = "/UploadFiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
            var serverPath = Path.GetDirectoryName(context.Request.MapPath(imageDir));
            if (!Directory.Exists(serverPath))
            {
                // 如果目录不存在则新建目录
                if (serverPath != null) Directory.CreateDirectory(serverPath);
            }

            // 取得GUID值作为图片名
            var newFileName = Guid.NewGuid().ToString();

            // 取得完整的存储路径
            var fullSaveDir = imageDir + newFileName + fileExt;

            using (var fileStream = File.OpenWrite(context.Request.MapPath(fullSaveDir)))
            {
                // 将文件数据写到磁盘上
                context.Request.InputStream.CopyTo(fileStream);

                // 将文件信息存入数据库
                var imageInfo = new ImageInfo
                {
                    ImageId = Guid.NewGuid().ToString("N").ToUpper(),
                    ImageName = fullSaveDir,
                    ImageSvrId = serverId.ToString()
                };

                this._imageDfsRepository.CreateImageInfo(imageInfo);
            }
        }


        [Route("api/Dfs/GetAllUseableServers")]
        [AcceptVerbs("Get", "Post")]
        public List<ImageSvr> GetAllUseableServers()
        {
            return _imageDfsRepository.GetAllUseableServers();
        }


    }
}
