using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class PizzaCategory
{
    public Pizza Pizza { get; set; }
    public List<Category>? Categories { get; set; }
    public List<SelectListItem>? Ingredienti { get; set; }
    public List<string>? IngredientiSelezionati { get; set; }

    public PizzaCategory()
    {

    }
}

