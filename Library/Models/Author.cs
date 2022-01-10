using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Author
  {

    public Author()
    {
      this.JoinEntitiesBook = new HashSet<AuthorBook>();
    }

    public int AuthorId { get; set; }

    [Required]
    public string Name { get; set; }

    public virtual ICollection<AuthorBook> JoinEntitiesBook { get; set; }
  }
}