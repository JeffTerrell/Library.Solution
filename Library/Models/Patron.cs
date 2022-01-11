using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Patron
  {

    public Patron()
    {
      this.JoinEntitiesCopy = new HashSet<Copy>();
    }

    public int PatronId {get; set;}

    [Display(Name = "Patron's Name")]
    public string Name {get; set;}

    public virtual ICollection<Copy> JoinEntitiesCopy {get;}
  }
}