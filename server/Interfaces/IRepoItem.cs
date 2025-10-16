namespace post_it_sharp.Interfaces;

public interface IRepoItem<Tid>
{
  public Tid Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}