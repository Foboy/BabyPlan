using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyPlan.mvcApp.Infras
{
    public enum SourceRootEnums
    {
        Css,
        Images,
        JsLib
    }

    public class RootConvertor
    {
        public static string RootConvert(SourceRootEnums rootType)
        {
            switch (rootType)
            { 
                case SourceRootEnums.Css:
                    return "~/css/";
                case SourceRootEnums.Images:
                    return "~/images/";
                case SourceRootEnums.JsLib:
                    return "~/JsLib/";
            }
            return "~/JsLib/";
        }
    }
}