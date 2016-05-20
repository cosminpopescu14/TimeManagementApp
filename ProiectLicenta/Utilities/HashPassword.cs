using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;


namespace ProiectLicenta.Utilities
{
    public class HashPassword
    {
        public static string ComputeSHA1(string password)
        {
            byte[] rawData = Encoding.ASCII.GetBytes(password);//converim parola intr-un sir de bytes
            byte[] hashedPassword = null;//aici stocam rezultatul functiei SHA1

            SHA1 sha1 = new SHA1CryptoServiceProvider();
            hashedPassword = sha1.ComputeHash(rawData);// aici calculam sha1 pt parola utilizatorului

            StringBuilder sb = new StringBuilder();
            foreach (var item in hashedPassword)
                sb.Append(item.ToString("x2"));

            return sb.ToString();
            
        }
    }
}