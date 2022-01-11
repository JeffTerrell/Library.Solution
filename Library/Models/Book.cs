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
      this.JoinEntitiesCopy = new HashSet<Copy>();
    }

    public int BookId { get; set; }

    [Required]
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
    public DateTime PublishDate { get; set; }

    public virtual ApplicationUser Librarian { get; set; }

    public virtual ICollection<AuthorBook> JoinEntitiesAuthor { get; }
    public virtual ICollection<Copy> JoinEntitiesCopy {get;}
  }
}
