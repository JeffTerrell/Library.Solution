using Library.Models;
using Library.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace Library.Controllers
{ 
  
  [Authorize]
  public class PatronsController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public PatronsController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Patrons.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patron patron)
    {
      _db.Patrons.Add(patron);
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Patron model = _db.Patrons.FirstOrDefault(patron => patron.PatronId == id);
      return View(model);
    }
  }
}