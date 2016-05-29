using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Final_eshop_entities.Extension
{
    public class Utils
    {

        /// <summary>
        /// MD5 Hash
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string Md5Hash(string originalString)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(originalString);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}