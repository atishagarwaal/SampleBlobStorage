using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBlobStorage
{
    internal class FileUtility
    {
        public static IFormFile CreateSampleFormFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist.", filePath);
            }

            string fileName = Path.GetFileName(filePath);
            var stream = File.OpenRead(filePath);

            var file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };

            return file;
        }
    }
}
