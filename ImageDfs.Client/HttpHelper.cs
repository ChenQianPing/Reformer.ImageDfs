using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ImageDfs.Client.Models;
using Newtonsoft.Json;

namespace ImageDfs.Client
{
    public static class HttpHelper
    {

        public static async Task<List<ImageSvr>> GetAllUseableServers()
        {
            const string url = "http://192.168.2.100:9005/api/Dfs/GetAllUseableServers";

            using (var http = new HttpClient())
            {
                // await异步等待回应
                var response = await http.GetAsync(url);

                // 确保HTTP成功状态值
                response.EnsureSuccessStatusCode();

                // await异步读取最后的JSON
                var jsonStr = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<ImageSvr>>(jsonStr);

                return result;


            }


        }

        /// <summary>
        /// 获取服务器索引号
        /// </summary>
        /// <param name="serverCount">服务器数量</param>
        /// <returns>索引号</returns>
        public static int GetServerIndex(int serverCount)
        {
            /*
             * 图片服务器实现负载均衡；             
             * Tips：可以设计一个更加高效的，类似于一致性哈希算法的哈希函数；
             */
            var rand = new Random();
            var randomNumber = rand.Next();
            var serverIndex = randomNumber % serverCount;

            return serverIndex;
        }

        #region GetFormDataByteArrayContent
        /// <summary>
        /// 获取键值集合对应的ByteArrayContent集合
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private static List<ByteArrayContent> GetFormDataByteArrayContent(NameValueCollection collection)
        {
            var list = new List<ByteArrayContent>();
            foreach (var key in collection.AllKeys)
            {
                var dataContent = new ByteArrayContent(Encoding.UTF8.GetBytes(collection[key]));
                dataContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    Name = key
                };
                list.Add(dataContent);
            }
            return list;
        }
        #endregion


        #region UploadTmplImg
        public static void UploadTmplImg(string baseAddress, string postUrl, UploadTmplImgInput inputs)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

                using (var content = new MultipartFormDataContent())
                {
                    // Make sure to change API address  
                    client.BaseAddress = new Uri(baseAddress);

                    var count = 1;

                    foreach (var input in inputs.TmplImages)
                    {
                        var imageName = input.TmplPath;

                        // Add file content   
                        var filePath = imageName;
                        var fileContent = new ByteArrayContent(File.ReadAllBytes(@filePath));
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            // 注意，这里修改一下，只获取文件名；
                            FileName = System.IO.Path.GetFileName(imageName),   // 文件名
                            FileNameStar = input.ExamCourseId,                  // 考试科目Id
                            Name = input.PageNo.ToString()                      // 页码

                        };
                        content.Add(fileContent);

                        Console.WriteLine((count++) + "/" + inputs.TmplImages.Count);
                    }

                    var d = new NameValueCollection { { "inputs", JsonConvert.SerializeObject(inputs) } };
                    var fromData = GetFormDataByteArrayContent(d);
                    foreach (var item in fromData)
                    {
                        content.Add(item);
                    }

                    // Make a call to Web API  
                    var result = client.PostAsync(postUrl, content).Result;

                    var str = result.Content.ReadAsStringAsync().Result;

                    Console.WriteLine(result.StatusCode);
                    Console.WriteLine(str);
                    Console.ReadLine();
                }
            }
        }
        #endregion

    }


}
