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
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      ViewBag.Copies = _db.Copies
        .Include(copy => copy.Book)
        .ToList();
      Patron model = _db.Patrons
        .Include(patron => patron.JoinEntitiesCopy)
        .ThenInclude(join => join.Book)
        .FirstOrDefault(patron => patron.PatronId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult CheckOut(int CopyId, int PatronId)
    {
      Copy target = _db.Copies.FirstOrDefault(copy => copy.CopyId == CopyId);
      target.PatronId = PatronId;
      target.CheckedOut = true;
      _db.Entry(target).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = PatronId});
    }

    [HttpPost]
    public ActionResult CheckIn(int CopyId)
    {
      Copy target = _db.Copies.FirstOrDefault(copy => copy.CopyId == CopyId);
      int patronId = target.PatronId;
      target.PatronId = 1;
      target.CheckedOut = false;
      _db.Entry(target).State = EntityState.Modified;
      _db.SaveChanges(); 
      return RedirectToAction("Details", new {id = patronId});
    }
  }
}