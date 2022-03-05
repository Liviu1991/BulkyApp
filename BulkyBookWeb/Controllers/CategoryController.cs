using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
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
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        //Get
        public IActionResult Create()
        {
            return View();
  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Customer Error", "The DisplayOrder cannot exactly match the Name");
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfuly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            // Retriving Category
            var CategoryFromDb = _db.Categories.Find(id);
           /* var CategoryFromDbFirst = _db.Categories.FirstOrDefault(i => i.Id == id);
            var CategoryFromDbSingle = _db.Categories.SingleOrDefault(i => i.Id == id);*/
           if(CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);

        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Customer Error", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfuly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Retriving Category
            var CategoryFromDb = _db.Categories.Find(id);
            /* var CategoryFromDbFirst = _db.Categories.FirstOrDefault(i => i.Id == id);
             var CategoryFromDbSingle = _db.Categories.SingleOrDefault(i => i.Id == id);*/
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            /* var CategoryFromDbFirst = _db.Categories.FirstOrDefault(i => i.Id == id);
             var CategoryFromDbSingle = _db.Categories.SingleOrDefault(i => i.Id == id);*/
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Category deleted successfuly";
            return RedirectToAction("Index");
  
        }

    }
}

