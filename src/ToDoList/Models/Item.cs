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
        //public ICollection<Categorization> Categorizations { get; set; }


        public override bool Equals(System.Object otherItem)
        {
            if(!(otherItem is Item))
            {
                return false;
            }
            else
            {
                Item newItem = (Item)otherItem;
                return ItemId.Equals(newItem.ItemId);
            }
        }

        public override int GetHashCode()
        {
            return ItemId.GetHashCode();
        }
    }
}