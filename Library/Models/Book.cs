using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{

    public enum Language
    {
        Hebrew,
        English
    }
 
    public class Book
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Title")]
        [Required]
        public string title { get; set; }
        [DisplayName("Author")]
        [Required]
        public string author { get; set; }
        [DisplayName("Publisher")]
        [Required]
        public string publisher { get; set; }
        [DisplayName("Publication Year")]
        [Required]
        public int publicationYear { get; set; }
        [DisplayName("Language")]
        [Required]
        public Language language { get; set; }
        [DisplayName("Series")]
        public string series { get; set; }
        [DisplayName("Summery")]
        public string summery { get; set; }
        [DisplayName("Category")]
        public string category { get; set; }
        [DisplayName("Copies")]
        [Required]
        public int copies{ get; set; }
        [DisplayName("Trailer URL")]
        public string trailerURL { get; set; }
        [DisplayName("Picture")]
        public byte[] picture { get; set; }
        
    }
}