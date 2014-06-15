using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using BabyPlan.Cache;
using System.IO;

namespace BabyPlan.Common
{
    public static class SecurityHelper
    {
        private const string KEY = "B0FD4893";
        private const string VECTOR = "D4C14EC1";

       public static string GetToken(string userid)
       {
           string token = string.Empty;
           token=MD5(userid+System.DateTime.Now.Ticks.ToString());
           CacheManagerFactory.GetMemoryManager().Set(token, userid,new TimeSpan(1,0,0,0));
           return token;
       }
       /// <summary>
       /// MD5加密
       /// </summary>
       /// <param name="text">原文.</param>
       /// <returns>返回加密结果的小写形式</returns>
       /// <remarks></remarks>
       public static string MD5(string text)
       {
           return MD5(text, Encoding.Default);
       }

       /// <summary>
       /// MD5加密
       /// </summary>
       /// <param name="text">原文.</param>
       /// <param name="encode">字符编码格式.</param>
       /// <returns>返回加密结果的小写形式</returns>
       /// <remarks></remarks>
       public static string MD5(string text, Encoding encode)
       {
           MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
           return BitConverter.ToString(hashmd5.ComputeHash(encode.GetBytes(text))).Replace("-", "").ToLower();
       }

       public static string Encrypt(string value)
       {
           try
           {
               return new SymmetricEncryptor(KEY, VECTOR).Encrypt(value);
           }
           catch
           {
               return null;
           }
       }

       public static string Decrypt(string value)
       {
           try
           {
               return new SymmetricEncryptor(KEY, VECTOR).Decrypt(value);
           }
           catch
           {
               return null;
           }
       }

       public static string EncryptObject(object value)
       {
           return Encrypt(JsonHelper.Serialize(value));
       }

       public static T DecryptObject<T>(string value)
       {
           return JsonHelper.Deserialize<T>(Decrypt(value));
       }

       public static T DecryptObject<T>(string value,T defvalue)
       {
           try
           {
               return JsonHelper.Deserialize<T>(Decrypt(value));
           }
           catch
           {
               return defvalue;
           }
       }

       public class SymmetricEncryptor
       {
           private byte[] _key;
           private byte[] _vector;

           public SymmetricEncryptor(string key, string vector)
           {
               InitBuffer(ref _key, key);
               InitBuffer(ref _vector, vector);
           }

           private void InitBuffer(ref byte[] buffer, string value)
           {
               if (!string.IsNullOrEmpty(value))
               {
                   buffer = Encoding.Default.GetBytes(value);
               }
               else
               {
                   buffer = new byte[8];
                   new Random().NextBytes(buffer);
               }
           }

           private bool IsEncrypted(string value)
           {
               try
               {
                   Convert.FromBase64String(value);
                   return true;
               }
               catch
               {
                   return false;
               }
           }

           public string Encrypt(string value)
           {
               if (IsEncrypted(value))
                   return value;

               SymmetricAlgorithm sa = new DESCryptoServiceProvider();
               ICryptoTransform ct = sa.CreateEncryptor(_key, _vector);

               using (MemoryStream ms = new MemoryStream())
               {
                   using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                   {
                       byte[] buffer = Encoding.UTF8.GetBytes(value);
                       cs.Write(buffer, 0, buffer.Length);
                       cs.FlushFinalBlock();
                   }

                   return Convert.ToBase64String(ms.ToArray());
               }
           }

           public string Decrypt(string value)
           {
               if (!IsEncrypted(value))
                   return value;

               try
               {
                   SymmetricAlgorithm sa = new DESCryptoServiceProvider();
                   ICryptoTransform ct = sa.CreateDecryptor(_key, _vector);

                   using (MemoryStream ms = new MemoryStream())
                   {
                       using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                       {
                           byte[] buffer = Convert.FromBase64String(value);
                           cs.Write(buffer, 0, buffer.Length);
                           cs.FlushFinalBlock();
                       }

                       return Encoding.UTF8.GetString(ms.ToArray());
                   }
               }
               catch (Exception e)
               {
                   e.ToString(); // disable CS0168
                   return value;
               }
           }
       }

    }
}
