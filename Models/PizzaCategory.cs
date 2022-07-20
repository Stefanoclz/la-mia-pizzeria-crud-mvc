using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class PizzaCategory
{
    public Pizza Pizza { get; set; }
    public List<Category>? Categories { get; set; }

    public PizzaCategory()
    {

    }
}

