namespace post_it_sharp.Models;

// NOTE backing class for watchers table
public class Watcher
{
  public int Id { get; set; }
  public int AlbumId { get; set; }
  public string AccountId { get; set; }
}

// REVIEW DTO (data transfer objects)
public class WatcherProfile : Profile
{
  // public string Id { get; set; }
  // public string Name { get; set; }
  // public string Picture { get; set; }
  // public DateTime CreatedAt { get; set; }
  // public DateTime UpdatedAt { get; set; }
  public int WatcherId { get; set; }
  public int AlbumId { get; set; }
}

public class WatcherAlbum : Album
{
  public string AccountId { get; set; }
  public int WatcherId { get; set; }
}