
using System.ComponentModel.DataAnnotations;

namespace IlkinBlog.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string? UserEmail { get; set; }

        public string? UserPhone { get; set; }

        [Required]
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
