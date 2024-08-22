using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using order_management.common;
using order_management.database.dto;
using order_management.services.interfaces;
using Order_Management.src.database.dto.fileUpload;
using Order_Management.src.services.interfaces;

namespace Order_Management.src.api.File
{
    public class FileUploadController
    {

        public async Task<IResult> Create(IFormFile file, IFileUploadService fileUploadService)
        {
            if (file == null || file.Length == 0)
            {
                return Results.BadRequest("No file uploaded.");
            }

            var filePath = await fileUploadService.UploadFileAsync(file);
            var response = new FileUploadDTO { FilePath = filePath };

            return Results.Ok(response);
        }

           
        }
}
