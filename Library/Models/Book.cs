using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{

  public class Book
  {

    public Book()
    {
      this.JoinEntitiesAuthor = new HashSet<AuthorBook>();
    }

    public int BookId { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
    public DateTime PublishDate { get; set; }

    public bool CheckedOut { get; set; } = false;

    public virtual ICollection<AuthorBook> JoinEntitiesAuthor { get; }
  }
}
