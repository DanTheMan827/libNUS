using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace libNUS
{
    internal static class HelperFunctions
    {
        public static string ByteArrayToHexString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                _ = hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            int NumberChars = hex.Length;

            if (NumberChars % 2 == 1)
            {
                throw new FormatException("String length is not an even number.");
            }

            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }
        public static async Task<long> FileSizeAtURL(string url)
        {
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";
            request.AllowAutoRedirect = true;
            request.MaximumAutomaticRedirections = 10;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                return response.ContentLength;
            }
            catch (WebException)
            {
                /* A WebException will be thrown if the status of the response is not `200 OK` */
            }
            finally
            {
                // Don't forget to close your response.
                if (response != null)
                {
                    response.Close();
                }
            }
            return 0;
        }
        public static async Task<bool> FileExistsAtURL(string url)
        {
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";
            request.AllowAutoRedirect = true;
            request.MaximumAutomaticRedirections = 10;

            bool exists = false;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                exists = true;
            }
            catch (WebException)
            {
                /* A WebException will be thrown if the status of the response is not `200 OK` */
            }
            finally
            {
                // Don't forget to close your response.
                if (response != null)
                {
                    response.Close();
                }
            }
            return exists;
        }
    }
}
