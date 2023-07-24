using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogger.Models.ViewModel
{
    public class BlogViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username Field can not be blank.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Title Field can not be blank.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Tag Field can not be blank.")]
        public string Tag { get; set; }

        [Required(ErrorMessage = "Content Field can not be blank."),MinLength(150)]
        public string Content { get; set; }

        //[DisplayName("Upload File")]
       // [Required]
       // public IFormFile ImagePath { get; set; }

       // public string BlogImagePath { get; set; }
    }
}
