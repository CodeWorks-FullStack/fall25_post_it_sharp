namespace post_it_sharp.Models;


// Because Album "implements" IRepoItem, Album *must* have the members IRepoItem enforces (Id, CreatedAt, UpdatedAt)
public class Album : IRepoItem<int>
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string Title { get; set; }
  public string CoverImg { get; set; }
  public string Category { get; set; }
  public string CreatorId { get; set; }
  public bool Archived { get; set; } = false; // create default value in C# model

  public Profile Creator { get; set; }
}