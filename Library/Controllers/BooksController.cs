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
using System;

namespace Library.Controllers
{ 
  
  [Authorize]
  public class BooksController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public BooksController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var librarian = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(librarian);
      ViewBag.UserBooks = _db.Books.Where(entry => entry.Librarian.Id == currentUser.Id).ToList();
      return View(_db.Books.ToList());
    }

    [AllowAnonymous]
    public ActionResult PatronIndex()
    {
      List<Book> model = _db.Books
        .Include(book => book.JoinEntitiesAuthor)
        .ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Book book)
    {
      var librarian = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(librarian);
      book.Librarian = currentUser;
      _db.Books.Add(book);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      ViewData["Authors"] = _db.Authors.ToList();
      Book model = _db.Books
        .Include(book => book.JoinEntitiesAuthor)
        .ThenInclude(join => join.Author)
        .Include(book => book.JoinEntitiesCopy)
        .FirstOrDefault(book => book.BookId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Book book)
    {
      _db.Entry(book).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = book.BookId});
    }

    public ActionResult Delete(Book book)
    {
      Book target = _db.Books.FirstOrDefault(target => target.BookId == book.BookId);
      _db.Books.Remove(target);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public PartialViewResult AddAuthor(int BookId, int AuthorId)
    {
      _db.AuthorBooks.Add(new AuthorBook() {BookId = BookId, AuthorId = AuthorId});
      _db.SaveChanges();
      Book model = _db.Books.FirstOrDefault(model => model.BookId == BookId);
      ViewData["Authors"] = _db.Authors.ToList();
      return PartialView("_ManageAuthorsPartial", model);
    }

    [HttpPost]
    public PartialViewResult RemoveAuthor(int BookId, int AuthorId)
    {
      AuthorBook target = _db.AuthorBooks.FirstOrDefault(target => target.BookId == BookId && target.AuthorId == AuthorId);
      _db.AuthorBooks.Remove(target);
      _db.SaveChanges();
      Book model = _db.Books.FirstOrDefault(model => model.BookId == BookId);
      ViewData["Authors"] = _db.Authors.ToList();
      return PartialView("_ManageAuthorsPartial", model);
    }

    [HttpPost]
    public PartialViewResult AddCopy(int BookId)
    {
      _db.Copies.Add(new Copy() {BookId = BookId, PatronId = 1});
      _db.SaveChanges();
      Book model = _db.Books
        .Include(book => book.JoinEntitiesCopy)
        .FirstOrDefault(book => book.BookId == BookId);
      return PartialView("_ManageCopiesPartial", model);
    }

    [HttpPost]
    public PartialViewResult RemoveCopy(int CopyId)
    {
      Copy target = _db.Copies.FirstOrDefault(copy => copy.CopyId == CopyId);
      Book model = _db.Books
        .Include(book => book.JoinEntitiesCopy)
        .FirstOrDefault(book => book.BookId == target.BookId);
      //TODO: check for book checked in before removing?
      _db.Copies.Remove(target);
      _db.SaveChanges();
      return PartialView("_ManageCopiesPartial", model);
    }
  }
}  