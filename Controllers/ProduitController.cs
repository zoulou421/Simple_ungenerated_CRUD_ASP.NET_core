using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonAppCRUD.Data;
using MonAppCRUD.Models;

namespace MonAppCRUD.Controllers
{
    public class ProduitController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProduitController(ApplicationDbContext db)
        {
            _db = db;
        }
        //Si on ne veut pas utiliser le constructeur, il faut donc mettre au niveau des paramètres [FromServices]ApplicationDbContext db
        // par exemple la met. index([FromServices]ApplicationDbContext) mais c'est repetif


        //GET: /Produit/Index
        public IActionResult Index(int page = 0, int size = 1)
        {
            // IEnumerable<Produit> produits = dbContext.Produits.ToList();
            // return View("Produits", produits);
            int position = page * size;
            IEnumerable<Produit> produits = _db.Produits.Skip(position).Take(size).Include(p => p.Categorie).ToList(); //ToList()
            ViewBag.currentPage = page;
            int nbProduits = _db.Produits.ToList().Count;
            int totalPages;
            if ((nbProduits % size) == 0)
            {
                totalPages = nbProduits / size;
            }
            else
            {
                totalPages = (nbProduits / size) + 1;
            }
            ViewBag.totalPages = totalPages;
            return View("Produits", produits);

        }



        public IActionResult FormProduit()
        {
            Produit p = new Produit();
            IEnumerable<Category> cats = _db.Categories;
            ViewBag.categories = cats;
            return View("FormProduit",p);//"FormProduit",
        }

        [HttpPost]
        public IActionResult SaveProduit(Produit p)
        {

            if (ModelState.IsValid)
            {
                _db.Produits.Add(p);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            IEnumerable<Category> cats = _db.Categories;
            ViewBag.categories = cats;
            return View("FormProduit",p);//



        }
    }
}
