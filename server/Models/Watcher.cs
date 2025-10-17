namespace post_it_sharp.Models;

// NOTE backing class for watchers table
public class Watcher
{
  public int Id { get; set; }
  public int AlbumId { get; set; }
  public string AccountId { get; set; }
}