using Microsoft.AspNetCore.Http;

namespace LibraryManager.BusinessLogic.Models
{
    public class CreateBook
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public int Grade { get; set; }

        public IFormFile File { get; set; }
    }
}
