using System.ComponentModel.DataAnnotations;

namespace LibraryManager.BusinessLogic.Models
{
    public class UpdateBook
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        [Range(1, 4)]
        public int Grade { get; set; }
    }
}
