using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;  
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList= _db.Categories;
            return View(objCategoryList);
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
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name","The DisplayOrder cannot match the Name.");
            }
            if (ModelState.IsValid)
            { 
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Category successfully created!";
            return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //3 ways to retrieve a category from DB
            var categoryFromDb=_db.Categories.Find(id);
            //var CategoryFromDbFirst = _db.Categories.FirstOrDefault(x => x.Id == id);
            //var CategoryFromDbSingle = _db.Categories.SingleOrDefault(x => x.Id == id);
            if(categoryFromDb == null)
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
                ModelState.AddModelError("name", "The DisplayOrder cannot match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category successfully edited!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //3 ways to retrieve a category from DB
            var categoryFromDb = _db.Categories.Find(id);
            //var CategoryFromDbFirst = _db.Categories.FirstOrDefault(x => x.Id == id);
            //var CategoryFromDbSingle = _db.Categories.SingleOrDefault(x => x.Id == id);
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
            if(obj == null)
            { 
                return NotFound();
            }
                _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Category successfully deleted!";
            return RedirectToAction("Index");
        }
    }
}
