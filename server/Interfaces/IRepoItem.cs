namespace post_it_sharp.Interfaces;

// Note an interface is instructions for a class. This can be implemented for all sorts of classes like our repo or services, but here it's for items we put in the DB, we are establishing a rule, that says, they must include these members.
public interface IRepoItem<Tid>
{
  public Tid Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}