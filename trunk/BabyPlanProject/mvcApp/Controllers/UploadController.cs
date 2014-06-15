using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using BabyPlan.Common;
using BabyPlan.WcfService;
using BabyPlan.Meta;
using BabyPlan.mvcApp.ViewModel;

namespace BabyPlan.mvcApp.Controllers
{
    public class UploadController : BaseController
    {
        //
        // GET: /Upload/

        public ActionResult Index()
        {
            return View();
        }

        public void Uploader(string token)
        {
            QueryDataCollection queryData = new QueryDataCollection(Request["queryData"]);

            HttpPostedFileBase file=null;

            if (Request.Files.Count > 0)
            {
                file = Request.Files[0];
            }
            else if (Request.InputStream.Length > 0)
            {
                string fileName= Server.UrlDecode(Request.Headers["fileName"]);
                file = new HttpPostedFileFromRequest(fileName, Request.ContentType, Request.InputStream);
            }

            UploadFileItem uploadItem = new UploadFileItem();
            //UserServiceClient userclient = new UserServiceClient();
            token = token ?? CurrentToken;
            //var logined = userclient.CheckLogin(token);
            //userclient.Close();
            //userclient = null;
            //if (!logined.Data)
            //{
            //    uploadItem.Saved = false;
            //    uploadItem.Msg = "登录后方可上传文件!";
            //}
             if (file != null)
            {

                string extention = file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower();

                uploadItem.Saved = true;

                if (file.ContentLength > 1024 * 2 * 1024)
                {
                    uploadItem.Saved = false;
                    uploadItem.Msg = "请上传2M以下大小图片!";
                }
                if (".jpg;.bmp;.gif;.png;.jpeg".IndexOf(extention) < 0)
                {
                    uploadItem.Saved = false;
                    uploadItem.Msg = "上传图片格式必须为.jpg;.bmp;.gif;.png;.jpeg!";
                }

                System.Drawing.Image upImage = null;
                try
                {
                    upImage = System.Drawing.Image.FromStream(file.InputStream);
                }
                catch (Exception ex)
                {
                    uploadItem.Saved = false;
                    uploadItem.Msg = "图片识别失败!" + ex.Message;
                }

                if (uploadItem.Saved)
                {

                    try
                    {
                        MemoryStream ms = new MemoryStream();
                        byte[] fileByteArray = null;
                        upImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        fileByteArray = ms.GetBuffer();

                        FileServiceClient client = new FileServiceClient();
                        AdvancedResult<ResPic> response = client.UploadPic(fileByteArray, upImage.Height, upImage.Width, token);
                        //string type = Request["type"] ?? queryData["type"];
                        //switch (type)
                        //{ 
                        //    case "g":
                        //        response = client.UploadBBPic(fileByteArray, upImage.Height, upImage.Width, CurrentToken);
                        //        break;
                        //    case "h":
                        //        response = client.UploadUserImage(fileByteArray, upImage.Height, upImage.Width, CurrentToken);
                        //        break;
                        //    case "b":
                        //        response = client.UploadPic(fileByteArray, upImage.Height, upImage.Width, CurrentToken);
                        //        break;
                        //    default:
                        //        response = new AdvancedResult<ResPic>();
                        //        response.Error = DataStructure.AppError.ERROR_FAILED;
                        //        response.ExMessage = "上传失败,不明确的图片使用类型！";
                        //        break;
                        //}



                        if (response.Error == DataStructure.AppError.ERROR_SUCCESS)
                        {
                            uploadItem.PicId = response.Data.PicId;
                            uploadItem.Pic = new PicModel().Bind(response.Data);
                            int maxWidth = 0;
                            int maxHeight = 0;
                            string width = Request["width"] ?? queryData["width"] ?? "";
                            string height = Request["height"] ?? queryData["height"] ?? "";
                            if (int.TryParse(width, out maxWidth) && int.TryParse(height, out maxHeight))
                            {
                                uploadItem.Pic.DisplaySeting(maxWidth, maxHeight);
                            }
                        }
                        else
                        {
                            uploadItem.Saved = false;
                            uploadItem.Msg = response.ExMessage ?? "保存图片失败!请稍后再试！";
                        }
                    }
                    catch (Exception ex)
                    {
                        uploadItem.Saved = false;
                        uploadItem.Msg = "保存图片失败!" + ex.Message;
                    }
                }
            }
            else
            {
                uploadItem.Saved = false;
                uploadItem.Msg = "上传文件为空!";
            }
            Response.ContentType = "text/html; charset=utf-8";
            Response.AppendHeader("Cache-Control", "no-cache");
            Response.Write(JsonHelper.Serialize(uploadItem));
        }

        public class UploadFileItem
        {
            public int PicId { get; set; }
            public PicModel Pic { get; set; }
            public bool Saved { get; set; }
            public string Msg { get; set; }

        }

    }

    public class HttpPostedFileFromRequest : HttpPostedFileBase
    {
        private Stream inputStream;
        private string fileName;
        private string contentType;

        public HttpPostedFileFromRequest(string fileName,string contentType, Stream inputStream)
        {
            this.inputStream = inputStream;
            this.fileName = fileName;
            this.contentType = contentType;
        }

        public override int ContentLength
        {
            get
            {
                return inputStream != null ? (int)inputStream.Length : 0;
            }
        }

        public override string ContentType
        {
            get
            {
                return contentType;
            }
        }

        public override string FileName
        {
            get
            {
                return fileName;
            }
        }

        public override Stream InputStream
        {
            get
            {
                return inputStream;
            }
        }

        public override void SaveAs(string filename)
        {
            if (inputStream != null)
            {
                base.SaveAs(filename);
            }
        }
    }

    public class QueryDataCollection
    {
        private Dictionary<string, string> keyValues;

        public string this[string key]
        {
            get
            {
                if (keyValues == null)
                    return null;
                string value = null;
                keyValues.TryGetValue(key, out value);
                return value;
            }
        }

        public QueryDataCollection(string queryData)
        {
            if (string.IsNullOrEmpty(queryData))
                return;
            string[] paramArray = queryData.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            keyValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (string param in paramArray)
            {
                var keyValueArray = param.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValueArray.Length > 1)
                {
                    if (keyValues.ContainsKey(keyValueArray[0]))
                    {
                        keyValues[keyValueArray[0]] = keyValueArray[1];
                    }
                    else
                    {
                        keyValues.Add(keyValueArray[0], keyValueArray[1]);
                    }
                }
            }
        }
    }
}
