using Order_Management.src.services.interfaces;
using static Order_Management.src.services.implementetions.FileUploadService;

namespace Order_Management.src.services.implementetions
{
    public class FileUploadService : IFileUploadService
    {
       
            private readonly string _uploadPath;

            public FileUploadService()
            {
                _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                Directory.CreateDirectory(_uploadPath);
            }

            public async Task<string> UploadFileAsync(IFormFile file)
            {
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("File is null or empty");
                }

                var filePath = Path.Combine(_uploadPath, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return filePath;
            }
        }
    }