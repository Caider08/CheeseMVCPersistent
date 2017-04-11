using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;


namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required]
        [Display(Name = "Cheese Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your cheese a description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public List<SelectListItem> Categories { get; set; }

        //public CheeseType Type { get; set; }

        //public List<SelectListItem> CheeseTypes { get; set; }
        public AddCheeseViewModel()
        {
        }

        public AddCheeseViewModel(IEnumerable<CheeseCategory> categories) {

            


            Categories = new List<SelectListItem>();
            foreach (CheeseCategory cheese in categories)
            {
                // <option value="0"></option>
                Categories.Add(new SelectListItem
                {
                    Value = ((int)cheese.ID).ToString(),
                    Text = cheese.Name
                });
            }
          //  CheeseTypes.Add(new SelectListItem
           // {
             //   Value = ((int)CheeseType.Soft).ToString(),
             //   Text = CheeseType.Soft.ToString()
           // });

           // CheeseTypes.Add(new SelectListItem
            //{
              //  Value = ((int)CheeseType.Fake).ToString(),
              //  Text = CheeseType.Fake.ToString()
            }

        }
    }

