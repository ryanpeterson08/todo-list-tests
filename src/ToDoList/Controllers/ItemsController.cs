using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
        private IItemRepository itemRepo;

        public ItemsController(IItemRepository thisRepo = null)
        {
            if(thisRepo == null)
            {
                this.itemRepo = new EFItemRepository();
            }
            else
            {
                this.itemRepo = new EFItemRepository();
            }
        }
        // GET: /<controller>/
        

        public IActionResult Index()
        {
            return View(itemRepo.Items.ToList());
        }

        // Create
        public ActionResult Create()
        {
            //ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Item item)
        {
            itemRepo.Save(item);
            return RedirectToAction("Index");
        }

        // Read
        public IActionResult Details(int id)
        {
            //var thisItem = db.Items.Include(items => items.Categorizations).FirstOrDefault(items => items.ItemId == id);
            Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            return View(thisItem);
        }

        // Update
        public IActionResult Edit(int id)
        {
            //var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
            Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            //ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View(thisItem);
        }

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            //db.Entry(item).State = EntityState.Modified;
            //db.SaveChanges();
            itemRepo.Edit(item);
            return RedirectToAction("Index");
        }

        // Delete

        public IActionResult Delete(int id)
        {
            //var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
            Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            return View(thisItem);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Item thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            itemRepo.Remove(thisItem);
            return RedirectToAction("Index");
        }
    }
}
