
using System.ComponentModel.DataAnnotations;

namespace post_it_sharp.Models;

public class Picture : IRepoItem<int>
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string CreatorId { get; set; }
  public int AlbumId { get; set; }
  [Url] public string ImgUrl { get; set; }
  public Profile Creator { get; set; }
}