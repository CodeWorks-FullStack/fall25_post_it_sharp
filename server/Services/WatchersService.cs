


namespace post_it_sharp.Services;

public class WatchersService
{
  private readonly WatchersRepository _repository;

  public WatchersService(WatchersRepository repository)
  {
    _repository = repository;
  }

  internal Watcher CreateWatcher(Watcher watcherData)
  {
    Watcher watcher = _repository.CreateWatcher(watcherData);
    return watcher;
  }

  internal List<WatcherAlbum> GetWatchersByAccountId(string accountId)
  {
    List<WatcherAlbum> watchers = _repository.GetWatchersByAccountId(accountId);
    return watchers;
  }

  internal List<WatcherProfile> GetWatchersByAlbumId(int albumId)
  {
    List<WatcherProfile> watchers = _repository.GetWatchersByAlbumId(albumId);
    return watchers;
  }
}