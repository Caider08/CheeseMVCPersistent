using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CheeseMVC.Models;
using CheeseMVC.Data;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.ViewModels;

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<CheeseCategory> categories = context.Categories.ToList();
            return View(categories);

        }

        public IActionResult Add()
        {
            AddCategoryViewModel adds = new AddCategoryViewModel();

            return View(adds);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addcategory)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = new CheeseCategory
                {
                    Name = addcategory.Name,
       
                };

                context.Categories.Add(newCategory);
                context.SaveChanges();
                return Redirect("/");

            }
            return View(addcategory);
        }



    }
}
