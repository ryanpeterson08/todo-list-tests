using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ToDoList.Models
{
    [Table("Items")]

    public class Item
    {
        //public Item()
        //{
        //    this.Categories = new HashSet<Category>();
        //}
        [Key]
        public int ItemId { get; set; }
        public string Description { get; set; }
        public ICollection<Categorization> Categorizations { get; set; }
    }
}