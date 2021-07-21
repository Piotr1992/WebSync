using BmpWebSyncLogic.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace BmpWebSyncLogic.helpers
{
    public class FtpHelper
    {
        private static string ftpAdres = "rurex.ml";
        private static string ftpUser = "erp@rurex.ml";
        private static string ftpPassword = "B0d9VNeHMt";

        public static void uploadFiles(List<Files> files)
        {
            foreach (Files f in files)
            {
                FtpWebRequest request =
                (FtpWebRequest)WebRequest.Create("ftp://"+ftpAdres+"/"+f.Path);
                request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                using (Stream fileStream = new MemoryStream(f.GetFile()))
                using (Stream ftpStream = request.GetRequestStream())
                {
                    fileStream.CopyTo(ftpStream);
                }

                f.Uploaded = true;
            }
        }

    }
}
