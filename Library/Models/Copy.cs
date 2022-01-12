namespace Library.Models
{
  public class Copy
  {
    public int CopyId {get; set;}
    public int BookId {get; set;}
    public int PatronId {get; set;}
    public virtual Book Book {get; set;}

    public virtual Patron Patron {get; set;}

    public bool CheckedOut {get; set;} = false;

    //public User Librarian who last interacted with the record?
  }
}