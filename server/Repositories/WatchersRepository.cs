

namespace post_it_sharp.Repositories;

public class WatchersRepository
{
  private readonly IDbConnection _db;

  public WatchersRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Watcher CreateWatcher(Watcher watcherData)
  {
    string sql = @"
    INSERT INTO
    watchers(account_id, album_id)
    VALUES(@AccountId, @AlbumId);
    
    SELECT * FROM watchers WHERE id = LAST_INSERT_ID();";

    Watcher watcher = _db.Query<Watcher>(sql, watcherData).SingleOrDefault();

    return watcher;
  }

  internal List<WatcherProfile> GetWatchersByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    watchers.*,
    accounts.*
    FROM watchers
    JOIN accounts ON accounts.id = watchers.account_id
    WHERE watchers.album_id = @albumId;";

    List<WatcherProfile> watchers = _db.Query(
      sql,
      (Watcher watcher, WatcherProfile account) =>
      {
        account.AlbumId = watcher.AlbumId;
        account.WatcherId = watcher.Id;
        return account;
      },
      new { albumId }).ToList();

    return watchers;
  }
}