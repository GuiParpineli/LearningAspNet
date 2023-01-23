using System.ComponentModel.DataAnnotations;

namespace RestApi_Template.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }

        //one-to-many
        public List<Book>? Books { get; set; }
    }
}
