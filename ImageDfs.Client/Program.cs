using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImageDfs.Client.Models;

namespace ImageDfs.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            AsyncMethod();

            Console.Read();
        }

        private static async void AsyncMethod()
        {
            Console.WriteLine("开始异步代码");

            #region input
            var tmplImg1 = new TmplImage
            {
                ExamCourseId = "3208075F3F8A4BD19032B41331A1C133",
                PageNo = 1,
                TmplPath = @"e:\10001003\1.jpg"
            };

            var tmplImg2 = new TmplImage
            {
                ExamCourseId = "3208075F3F8A4BD19032B41331A1C133",
                PageNo = 2,
                TmplPath = @"e:\10001003\2.jpg"
            };

            var lstTmplImgs = new List<TmplImage> { tmplImg1, tmplImg2 };

            var input = new UploadTmplImgInput {TmplImages = lstTmplImgs};
            #endregion

            var serverList = await HttpHelper.GetAllUseableServers();

            // 获取要保存的图片服务器索引号
            var serverIndex = HttpHelper.GetServerIndex(serverList.Count);
            Console.WriteLine("serverIndex:" + serverIndex);

            // 获取指定图片服务器的信息
            var serverUrl = serverList[serverIndex].SvrUrl;
            var serverId = serverList[serverIndex].SvrId;

            Console.WriteLine("serverUrl:" + serverUrl);
            Console.WriteLine("serverId:" + serverId);

            var baseAddress = $"http://{serverUrl}";
            const string postUrl = "/api/Dfs/UploadTmplImg";

            HttpHelper.UploadTmplImg(baseAddress, postUrl, input);

            Console.WriteLine("异步代码执行完毕");
        }



    }
}
