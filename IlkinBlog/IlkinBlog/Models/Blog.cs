using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IlkinBlog.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? TitleEn { get; set; }

        public string? Content { get; set; }


        public string? ContentEn { get; set; }

        public string? Description { get; set; }

        public string? DescriptionEn { get; set; }

        public string? Author { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public int CategoryId{ get; set; }

        public virtual Category? Category { get; set; }
        

    }
}
