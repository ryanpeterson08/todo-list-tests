
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    [Table("Categories_Items")]
    public class Categorization
    {
        [Key]
        public int Id { get; set; } 
        public int CategoryId { get; set; }
        public int ItemId { get; set; }
        public Item item { get; set; }
        public Category category { get; set; }
        
    }
    
}