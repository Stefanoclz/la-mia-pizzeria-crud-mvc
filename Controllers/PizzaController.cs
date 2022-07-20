﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Data.SqlClient;
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
            return View("Index", listaPizze);
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
                    context.Entry(singola).Collection("listaIngredienti").Load();
                    return View("Details", singola);
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
        public ActionResult Create(PizzaCategory pizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", pizza);
            }

            using (PizzaContext db = new PizzaContext())
            {
                db.Pizza.Add(pizza.Pizza);
                db.SaveChanges();
            }
            

            return RedirectToAction("Index");
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
        public ActionResult Edit(int id, PizzaCategory pizza)
        {
            using (PizzaContext context = new PizzaContext())
            {
                Pizza modify = context.Pizza.Where(p => p.id == id).FirstOrDefault();
                if (modify != null)
                {
                    pizza.Pizza = modify;
                    if (!ModelState.IsValid)
                    {
                        pizza.Categories = context.Category.ToList();
                        return View(pizza);
                    }



                    Category nuova = pizza.Categories.FirstOrDefault();
                    modify.name = pizza.Pizza.name;
                    modify.description = pizza.Pizza.description;
                    modify.fotoLink = pizza.Pizza.fotoLink;
                    modify.prezzo = pizza.Pizza.prezzo;
                    modify.CategoryId = pizza.Pizza.CategoryId;
                    modify.Categoria = nuova;
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