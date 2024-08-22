using Microsoft.AspNetCore.Builder;
using order_management.api;

namespace Order_Management.src.api.File
{
    public class FileUploadRoutes
    {
        public void MapFileUploadRoutes(WebApplication app)
        {
            var fileUploadController = new FileUploadController();
            var router = app.MapGroup("/api");

            
            router.MapPost("/upload", fileUploadController.Create);
           
        }
    }
}
