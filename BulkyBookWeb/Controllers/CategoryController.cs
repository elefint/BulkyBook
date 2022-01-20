using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _db.Categories;
        return View(objCategoryList);
    }
    // Get
    public IActionResult Create()
    {
        return View();
    }
    
    // Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "The DisplayOrder cannot be the same as Name.");
        }
        if (ModelState.IsValid)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully!"; 
            return RedirectToAction("Index");
        }
        
        return View(obj);
    }
    
    // Get
    public IActionResult Edit()
    {
        return View();
    }
    
    // Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "The DisplayOrder cannot be the same as Name.");
        }
        if (ModelState.IsValid)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Category updated successfully!";
            return RedirectToAction("Index");
        }
        
        return View(obj);
    }
    
    // Get
    public IActionResult Delete(int? id)
    {
        return View();
    }
    
    // Post
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj = _db.Categories.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Categories.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Category deleted successfully!";
        return RedirectToAction("Index");
    }
}