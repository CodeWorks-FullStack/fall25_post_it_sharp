namespace post_it_sharp.Models;

public class Album : IRepoItem<int>
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string Title { get; set; }
  public string CoverImg { get; set; }
  public string Category { get; set; }
  public string CreatorId { get; set; }

  public Profile Creator { get; set; }
}