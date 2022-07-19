using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Data.SqlClient;
using la_mia_pizzeria_static.ValidationAttributes;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzeController : Controller
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
                    return NotFound($"La pizza con id {id} non è stata trovata");

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
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MoreThanFiveWordsValidationAttribute]
        public ActionResult Create(Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", pizza);
            }

            using (PizzaContext db = new PizzaContext())
            {
                db.Pizza.Add(pizza);
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
                return View(modify);
            }
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
