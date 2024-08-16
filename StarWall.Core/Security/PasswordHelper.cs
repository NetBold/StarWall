using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StarWall.Core.Security
{
    public class PasswordHelper
    {
        public static string EncodePasswordMd5(string pass)
        {
            Byte[] originalbytes;
            Byte[] encodedBytes;
            MD5 md5;
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            md5 = new MD5CryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
            originalbytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalbytes);
            return BitConverter.ToString(encodedBytes);
        }
    }
}
