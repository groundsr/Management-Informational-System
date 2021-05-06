using Microsoft.AspNetCore.Http;

namespace MIS.DTOs.BusinessLogic
{
    public class DocumentDTO
    {
        public string Name { get;  set; }
        public IFormFile File { get;  set; }
        public string Path { get; set; }
    }
}