using Microsoft.AspNetCore.Mvc;
using MonAppCRUD.Data;
using MonAppCRUD.Models;

namespace MonAppCRUD.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db=db;
        }
        public IActionResult Index(int page = 0, int size = 3, string motCle = "")
        {
            //IEnumerable<Category> Cats = _db.Categories.ToList();
            //var Cats = _db.Categories.ToList();
            // return View(Cats);
            int position = page * size;
            //IEnumerable<Category> Cats = _db.Categories.Skip(position).Take(size).ToList();
            IEnumerable<Category> Cats = _db.Categories.Where(c => c.Name.Contains(motCle))
             .Skip(position).Take(size).ToList();
            ViewBag.currentPage = page;
            //int nbCategories = _db.Categories.ToList().Count;
            int nbCategories =_db.Categories.Where(c => c.Name.Contains(motCle)).ToList().Count;
            int totalPages;
            if ((nbCategories % size) == 0)
            {
                totalPages = nbCategories / size;
            }
            else
            {
                totalPages = 1+ (nbCategories / size);
            }
            ViewBag.motCle = motCle;
            ViewBag.totalPages = totalPages;
            return View(Cats);


        }

        //GET
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //partie de Edit
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id==id) ;
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //partie de delete
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id==id) ;
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted successfully";
            return RedirectToAction("Index");
        }
    }

}

