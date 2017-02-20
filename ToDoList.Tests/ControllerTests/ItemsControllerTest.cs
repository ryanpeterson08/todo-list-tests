using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Controllers;
using ToDoList.Models;
using Xunit;
using System.Linq;
using Moq;
using System;

namespace ToDoList.Tests
{
    public class ItemsControllerTest : IDisposable
    {
        Mock<IItemRepository> mock = new Mock<IItemRepository>();
        EFItemRepository db = new EFItemRepository(new TestDbContext());

        public void DbSetup()
        {
            mock.Setup(m => m.Items).Returns(new Item[]
            {
            new Item {ItemId = 1, Description = "Wash the dog" },
            new Item {ItemId = 2, Description = "Do the dishes" },
            new Item {ItemId = 3, Description = "Sweep the floor" }
            }.AsQueryable());
        }
            
        public void Dispose()
        {
            db.DeleteAllItems();
        }

        [Fact]
        public void Mock_Get_ViewResult_Index_Test()
        {
            //Arrange
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);
            
            //Act
            IActionResult result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Mock_Get_ModelList_Index_Test()
        {
            //Arrange
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);
            ViewResult indexView = new ItemsController().Index() as ViewResult;
            
            //Act
            object result = indexView.ViewData.Model;

            //Assert 
            Assert.IsType<List<Item>>(result);
        }

        [Fact]
        public void Mock_Post_MethodAddsItem_Test()
        {
            //Arrange
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);
            Item testItem = new Item();
            testItem.Description = "test item";

            //Act
            controller.Create(testItem);
            ViewResult indexView = new ItemsController().Index() as ViewResult;
            IEnumerable<Item> collection = indexView.ViewData.Model as IEnumerable<Item>;

            //Assert
            Assert.Contains(testItem, collection);
        }

        [Fact]
        public void Mock_ConfirmEntry_Test()
        {
            //Arrange
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);
            Item testItem = new Item();
            testItem.Description = "Wash the dog";
            testItem.ItemId = 1;

            //Act
            ViewResult indexView = controller.Index() as ViewResult;
            IEnumerable<Item> collection = indexView.ViewData.Model as IEnumerable<Item>;

            //Assert
            Assert.Contains(testItem, collection);
        }

        [Fact]
        public void DB_CreateNewEntry_Test()
        {
            // Arrange
            ItemsController controller = new ItemsController(db);
            Item testItem = new Item();
            testItem.Description = "TestDb Item";

            // Act
            controller.Create(testItem);
            var collection = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Item>;

            // Assert
            Assert.Contains<Item>(testItem, collection);
        }

      
    }
}
