using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.BusinessLogic.Models.Book
{
    public class CreateBook
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [Range(1, 4)]
        public int Grade { get; set; }

        public IFormFile File { get; set; }
    }
}
