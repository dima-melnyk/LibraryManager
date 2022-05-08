using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManager.DataAccess.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public int Grade { get; set; }

        public int ImageId { get; set; }
        public virtual Image Image { get; set; }
    }
}
