using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
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
            IEnumerable<Category> objCategoryList = _db.GetAll();
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
                _db.Add(obj);
                _db.Save();
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
            /*var CategoryFromDb = _db.Categories.Find(id);*/
            var CategoryFromDbFirst = _db.GetFirstOrDefault(i => i.Id == id);
            /*var CategoryFromDbSingle = _db.Categories.SingleOrDefault(i => i.Id == id);*/
            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CategoryFromDbFirst);

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
                _db.Update(obj);
                _db.Save();
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
            /*var CategoryFromDb = _db.Categories.Find(id);*/
            var CategoryFromDb = _db.GetFirstOrDefault(i => i.Id == id);
            /*var CategoryFromDbSingle = _db.Categories.SingleOrDefault(i => i.Id == id);*/
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
            /*var obj = _db.Categories.Find(id);*/
            var obj = _db.GetFirstOrDefault(i => i.Id == id);
            /*var CategoryFromDbSingle = _db.Categories.SingleOrDefault(i => i.Id == id);*/
            if (obj == null)
            {
                return NotFound();
            }
            _db.Remove(obj);
            _db.Save();
            TempData["success"] = "Category deleted successfuly";
            return RedirectToAction("Index");
  
        }

    }
}

