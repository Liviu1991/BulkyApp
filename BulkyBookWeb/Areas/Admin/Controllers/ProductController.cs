using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Controllers;
[Area("Admin")]

public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll();
        return View(objProductList);
    }



    //Get
    public IActionResult Upsert(int? id)
    {
        Product product = new();
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
            u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

        IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
            u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
        if (id == null || id == 0)
        {
            //Create
            ViewBag.CategoryList = CategoryList;
            ViewData["CoverTypeList"] = CoverTypeList;
            return View(product);
        } 
        else
        {

        }
        return View(product);

    }

    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Uosert(Product obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product updated successfuly";
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
        // Retriving Product
        /*var ProductFromDb = _db.Categories.Find(id);*/
        var ProductFromDb = _unitOfWork.Product.GetFirstOrDefault(i => i.Id == id);
        /*var ProductFromDbSingle = _db.Categories.SingleOrDefault(i => i.Id == id);*/
        if (ProductFromDb == null)
        {
            return NotFound();
        }
        return View(ProductFromDb);

    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        /*var obj = _db.Categories.Find(id);*/
        var obj = _unitOfWork.Product.GetFirstOrDefault(i => i.Id == id);
        /*var ProductFromDbSingle = _db.Categories.SingleOrDefault(i => i.Id == id);*/
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save();

        TempData["success"] = "Product deleted successfuly";
        return RedirectToAction("Index");

    }

}

