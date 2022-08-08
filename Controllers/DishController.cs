using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class DishController : Controller
{

    private DishContext _context;

    public DishController(DishContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        // Can also do this:
        // ViewBag.allDishes = _context.Dishes.OrderByDescending(dish => dish.UpdatedAt).ToList();
        
        List<Dish> allDishes = _context.Dishes.OrderByDescending(dish => dish.UpdatedAt).ToList();
        // Then pass allDishes: return View("Index", allDishes)

        return View("Index", allDishes);
    }

    [HttpGet("/new")]
    public IActionResult NewDish()
    {
        return View("NewDish");
    }

    [HttpPost("/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return NewDish();
    }

    [HttpGet("/{dishId}")]
    public IActionResult ShowDish(int dishId)
    {
        Dish? singleDish = _context.Dishes.SingleOrDefault(dish => dish.DishId == dishId);

        return View("ShowDish", singleDish);
    }

    [HttpGet("/edit/{dishId}")]
    public IActionResult ShowEditForm(int dishId)
    {
        Dish? singleDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

        return View("ShowEditForm", singleDish);
    }


    [HttpPost("/edit/{dishId}")]
    public IActionResult EditDish(int dishId, Dish editedDish)
    {
        Dish? singleDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        
        if(ModelState.IsValid)
        {
            singleDish.Name = editedDish.Name;
            singleDish.Chef = editedDish.Chef;
            singleDish.Calories = editedDish.Calories;
            singleDish.Tastiness = editedDish.Tastiness;
            singleDish.Description = editedDish.Description;
            singleDish.UpdatedAt = DateTime.Now;
            
            _context.SaveChanges();
            return RedirectToAction("ShowDish", editedDish);
        }
        return View("ShowEditForm", editedDish);
    }

    [HttpGet("/delete/{dishId}")]
    public IActionResult DeleteDish(int dishId)
    {
        Dish? singleDish = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

        _context.Dishes.Remove(singleDish);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
