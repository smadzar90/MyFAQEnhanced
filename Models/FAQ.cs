using System.ComponentModel.DataAnnotations;

namespace MyFAQEnhanced.Models
{
    public class FAQ
    {
        public int FAQId { get; set; }

        [Required(ErrorMessage = "The Question field is required.")]
        public string Question { get; set; }

        [Required(ErrorMessage = "The Answer field is required.")]
        public string Answer { get; set; }

        [Required(ErrorMessage = "Please select a topic.")]
        public string TopicId { get; set; }

        public Topic? Topic { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public string CategoryId { get; set; } 

        public Category? Category { get; set; } 
    }
}
