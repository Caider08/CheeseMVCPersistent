using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CheeseMVC.ViewModels;
using CheeseMVC.Controllers;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        public int cheeseID { get; set; }

        public int menuID { get; set; }

        public Menu Menu { get; set; }

        public List<SelectListItem> Cheeses { get; set; }

        public AddMenuItemViewModel() { }

        public AddMenuItemViewModel(IEnumerable<Cheese> cheeses, Menu menu)
        {

            Cheeses = new List<SelectListItem>();

            foreach (var cheese in cheeses)
            {
                Cheeses.Add(new SelectListItem
                {
                    //Value = cheese.ID.ToString(),
                    Value = ((int)cheese.ID).ToString(),
                    Text = cheese.Name,
                });
            }

            Menu = menu;
        }

    }

}
