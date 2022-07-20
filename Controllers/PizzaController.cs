using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using la_mia_pizzeria_static.ValidationAttributes;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        // GET: HomeController1
        public ActionResult Index()
        {
            PizzaContext context = new PizzaContext();
            List<Pizza> listaPizze = context.Pizza.ToList();
            List<PizzaCategory> listaPizzeCat = new List<PizzaCategory>();
            foreach (Pizza pizza in listaPizze)
            {
                PizzaCategory pizzaCategory = new PizzaCategory();
                pizzaCategory.Pizza = pizza;
                pizzaCategory.Categories = context.Category.Where(c => c.Id == pizza.CategoryId).ToList();
                listaPizzeCat.Add(pizzaCategory);
            }
            return View("Index", listaPizzeCat);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            

            using (PizzaContext context = new PizzaContext())
            {
                Pizza singola = context.Pizza.Where(singola => singola.id == id).FirstOrDefault();
                if(singola == null)
                {
                    return NotFound($"La Pizza con id {id} non è stata trovata");

                }
                else
                {
                    PizzaCategory pizzaCat = new PizzaCategory();
                    context.Entry(singola).Collection("listaIngredienti").Load();
                    pizzaCat.Pizza = singola;
                    pizzaCat.Categories = context.Category.Where(c => c.Id == singola.CategoryId).ToList();
                    return View("Details", pizzaCat);
                }
            }
                    
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            using(PizzaContext context = new PizzaContext())
            {
                List<Category> categories = context.Category.ToList();
                PizzaCategory model = new PizzaCategory();
                model.Categories = categories;
                model.Pizza = new Pizza();
                return View(model);
            }
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MoreThanFiveWordsValidationAttribute]
        public ActionResult Create(PizzaCategory model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (PizzaContext db = new PizzaContext())
            {

                db.Pizza.Add(model.Pizza);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            using(PizzaContext context = new PizzaContext())
            {
                Pizza modify = context.Pizza.Where(p => p.id == id).FirstOrDefault();
                if(modify == null)
                {
                    return NotFound();
                }

                List<Category> categorie = context.Category.ToList();
                PizzaCategory model = new PizzaCategory();
                model.Pizza = modify;
                model.Categories = categorie;
                return View(model);
         
            }
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PizzaCategory model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (PizzaContext context = new PizzaContext())
            {
                Pizza modify = context.Pizza.Where(p => p.id == id).FirstOrDefault();
                if (modify != null)
                {
                    //pizza.Pizza = modify;
                    
                    modify.name = model.Pizza.name;
                    modify.description = model.Pizza.description;
                    modify.fotoLink = model.Pizza.fotoLink;
                    modify.prezzo = model.Pizza.prezzo;
                    modify.CategoryId = model.Pizza.CategoryId;

                    context.Update(modify);
                    context.SaveChanges();
                }
                else
                {
                    return NotFound();
                }

                return RedirectToAction("Index");
            }
        }

        // GET: HomeController1/Delete/5
        /*public ActionResult Delete(int id)
        {
            return View();
        }*/

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (PizzaContext context = new PizzaContext())
            {
                Pizza erase = context.Pizza.Where(p => p.id == id).FirstOrDefault();

                if (erase == null)
                {
                    return NotFound();
                }
               
                context.Pizza.Remove(erase);
                context.SaveChanges();
                return RedirectToAction("Index");
                
            }
        }
    }
}
