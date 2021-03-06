﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Yahoo.Yui.Compressor;
using System.Text;
using System.Web.Configuration;

namespace BabyPlan.mvcApp.Extensions
{
    public class ResourceHelper
    {
        private static object state = new object();
        private static object cssVersion = WebConfigurationManager.AppSettings["cssVersion"] ?? "1.0";

        public static void RegisterCss(WebViewPage page,string css)
        {

            string registedCss = (HttpContext.Current.Items["RegistedCss"] ?? string.Empty).ToString();
            HttpContext.Current.Items["RegistedCss"] = (string.IsNullOrEmpty(registedCss) ? string.Empty : (registedCss + ",")) + css;
        }

        public static string CreateCssLink(WebViewPage page)
        {
            if (HttpContext.Current.Items["RegistedCss"] != null)
            {
                string css = (HttpContext.Current.Items["RegistedCss"] ?? string.Empty).ToString();
                if (!string.IsNullOrEmpty(css))
                {
                    string cacheName = string.Format("cache_{0}.css", (css + (cssVersion ?? string.Empty)).GetHashCode()).Replace("-", "0");
                    //string cacheName = cacheFileName + ".css";
                    string cssCachePath = page.Server.MapPath("~/css/cache");
                    string cacheFile = Path.Combine(cssCachePath, cacheName);
                    if (!System.IO.File.Exists(cacheFile))
                    {
                        lock (state)
                        {
                            if (!System.IO.File.Exists(cacheFile))
                            {
                                string[] cssArr = css.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (cssArr.Length > 0)
                                {
                                    if (!Directory.Exists(cssCachePath))
                                    {
                                        Directory.CreateDirectory(cssCachePath);
                                    }
                                    string cssItemPath, cssPath = page.Server.MapPath("~/css"); ;
                                    CssCompressor cc = new CssCompressor();
                                    for (int i = cssArr.Length - 1; i >= 0; i--)
                                    {
                                        cssItemPath = Path.Combine(cssPath, cssArr[i]);
                                        if (System.IO.File.Exists(cssItemPath))
                                        {
                                            string content = System.IO.File.ReadAllText(cssItemPath);
                                            System.IO.File.AppendAllText(cacheFile, cc.Compress(content), Encoding.UTF8);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    string href = page.Url.Content("~/css/cache/" + cacheName);
                    return string.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" media=\"screen\" />", href);
                }
                //string href = string.Format("/Content/CssAccessor/{0}/{1}", HttpContext.Current.Items["RegistedCss"], version);
                return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}