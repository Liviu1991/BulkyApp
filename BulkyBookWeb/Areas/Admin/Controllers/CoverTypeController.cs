using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers;
[Area("Admin")]

public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);
    }

    //Get
    public IActionResult Create()
    {
        return View();

    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
        if(ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType created successfuly";
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
        // Retriving CoverType
        /*var CoverTypeFromDb = _db.Categories.Find(id);*/
        var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(i => i.Id == id);
        /*var CoverTypeFromDbSingle = _db.Categories.SingleOrDefault(i => i.Id == id);*/
        if (CoverTypeFromDbFirst == null)
        {
            return NotFound();
        }
        return View(CoverTypeFromDbFirst);

    }

    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType updated successfuly";
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
        // Retriving CoverType
        /*var CoverTypeFromDb = _db.Categories.Find(id);*/
        var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(i => i.Id == id);
        /*var CoverTypeFromDbSingle = _db.Categories.SingleOrDefault(i => i.Id == id);*/
        if (CoverTypeFromDb == null)
        {
            return NotFound();
        }
        return View(CoverTypeFromDb);

    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        /*var obj = _db.Categories.Find(id);*/
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(i => i.Id == id);
        /*var CoverTypeFromDbSingle = _db.Categories.SingleOrDefault(i => i.Id == id);*/
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.CoverType.Remove(obj);
        _unitOfWork.Save();

        TempData["success"] = "CoverType deleted successfuly";
        return RedirectToAction("Index");

    }

}

