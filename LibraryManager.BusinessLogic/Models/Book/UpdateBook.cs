using System.ComponentModel.DataAnnotations;

namespace LibraryManager.BusinessLogic.Models.Book
{
    public class UpdateBook
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Назва книги обов\'язкова")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Предмет книги обов\'язковий")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Курс обов\'язковий")]
        [Range(1, 6, ErrorMessage = "Курс має бути в районі від 1 до 6")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Курс має мати числове значення")]
        public int? Grade { get; set; }
    }
}
