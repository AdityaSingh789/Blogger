using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Blogger.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username Field can not be blank.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Title Field can not be blank.")]
        public string Title { get; set; }

        [Required (ErrorMessage = "Tag Field can not be blank.")]
        public string Tag { get; set; }

        [Required(ErrorMessage = "Content Field can not be blank."),MinLength(150)]
        public string Content { get; set; }

        public string ImagePath { get; set; }

        //public IFormFile ImageFile { get; set; }

    }
}
