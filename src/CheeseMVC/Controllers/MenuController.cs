using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Models;
using CheeseMVC.Data;
using Microsoft.EntityFrameworkCore;
using CheeseMVC.ViewModels;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            IList<Menu> menus = context.Menus.Include(c => c.CheeseMenus).ToList();

            return View(menus);

        }

        [HttpGet]
        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name,
        
                };

                context.Menus.Add(newMenu);
                context.SaveChanges();
                //int numba = newMenu.ID;
                //return RedirectToAction("ViewMenu", "Menu", new {id = numba});
                return Redirect("/Menu");
            };

            return View(addMenuViewModel);

        }

        //[HttpGet]
        //[Route("/Menu/ViewMenu/id")]
        public IActionResult ViewMenu(int id)
        {

            Menu menuObject = context.Menus.SingleOrDefault(c => c.ID == id);

            if(menuObject == null)
            {
                return View("Index");
            }
            else
            {
                List<CheeseMenu> items = context.CheeseMenus.Include(item => item.Cheese).Where(cm => cm.MenuID == id).ToList();

                ViewMenuViewModel vMvM = new ViewMenuViewModel
                {
                    Menu = menuObject,

                    Items = items,
                };

                return View(vMvM);
            }
            

        }
        [HttpGet]
        public IActionResult AddItem(int id)
        {
           Menu menuObject = context.Menus.Single(c => c.ID == id);

           IEnumerable<Cheese> cheeses = context.Cheeses.ToList();

            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(cheeses, menuObject);

            return View(addMenuItemViewModel);

        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addItemtoMenu)
        {
            if (ModelState.IsValid)
            {
                IList<CheeseMenu> existingItems = context.CheeseMenus.Where(cm => cm.CheeseID == addItemtoMenu.cheeseID).Where(cm => cm.MenuID == addItemtoMenu.menuID).ToList();
                if (existingItems.Count == 0)
                {
                    CheeseMenu newCheeseM = new CheeseMenu 
                    {
                        MenuID = addItemtoMenu.menuID,
                        CheeseID = addItemtoMenu.cheeseID,
                    };

                    context.CheeseMenus.Add(newCheeseM);
                    context.SaveChanges();


                    //int numba = newCheeseM.MenuID;
                    //return RedirectToAction("ViewMenu", "Menu", new { id = numba });
                    //return Redirect("ViewMenu/" + newCheeseM.MenuID);
                    return Redirect(string.Format("/Menu/ViewMenu/{0}", newCheeseM.MenuID));
                }

                int numbaa = addItemtoMenu.menuID;
                return RedirectToAction("ViewMenu", "Menu", new { id = numbaa });
              //  return Redirect("ViewMenu/" + addItemtoMenu.menuID);
            }

            return View(addItemtoMenu);
        }
    }
}
