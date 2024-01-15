using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Business.Helpers
{
    public static class FIleManager
    {
        public static string UploadFile(this IFormFile file, string envPath, string folderName)
        {
            string fileName = file.FileName.Length > 64 ? file.FileName.Substring(file.FileName.Length - 64, 64) : file.FileName;
            fileName = Guid.NewGuid().ToString() + fileName;

            string path = envPath + folderName + fileName;
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return fileName;
        }
        public static void DeleteFile(this string imgUrl, string envPath, string folderName)
        {
            string path = envPath + folderName + imgUrl;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        public static bool CheckContent(this IFormFile file, string content)
        {
            return file.ContentType.Contains(content);
        }
        public static bool CheckLength(this IFormFile file, int length)
        {
            return file.Length <= length;
        }
    }
}
