using Library.Models;
using Library.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Library.Controllers
{ 
  
  public class BooksController : Controller
  {
    private readonly LibraryContext _db;
    public BooksController(LibraryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Books.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Book book)
    {
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
  }
}  