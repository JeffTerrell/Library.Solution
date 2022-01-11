using System.Collections.Generic;

namespace Library.Models
{
  public class Patron
  {

    public Patron()
    {
      this.JoinEntitiesCopy = new HashSet<Copy>();
    }

    public int PatronId {get; set;}
    public string Name {get; set;}

    public virtual ICollection<Copy> JoinEntitiesCopy {get;}
  }
}