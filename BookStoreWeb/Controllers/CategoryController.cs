using BookStore.DataAccess;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;
        public CategoryController(ICategoryRepository db)
        {
            _db = db;  
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList= _db.GetAll();
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
            _db.Add(obj);
            _db.Save();
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
            //var categoryFromDb=_db.Categories.Find(id);
            var CategoryFromDbFirst = _db.GetFirstOrDefault(x => x.Id == id);
            //var CategoryFromDbSingle = _db.Categories.SingleOrDefault(x => x.Id == id);
            if(CategoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CategoryFromDbFirst);
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
                _db.Update(obj);
                _db.Save();
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
            //var categoryFromDb = _db.Categories.Find(id);
            var CategoryFromDbFirst = _db.GetFirstOrDefault(x => x.Id == id);
            //var CategoryFromDbSingle = _db.Categories.SingleOrDefault(x => x.Id == id);
            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CategoryFromDbFirst);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            { 
                return NotFound();
            }
                _db.Remove(obj);
                _db.Save();
            TempData["success"] = "Category successfully deleted!";
            return RedirectToAction("Index");
        }
    }
}
