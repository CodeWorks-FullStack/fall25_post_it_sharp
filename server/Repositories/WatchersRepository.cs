


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

  internal List<WatcherAlbum> GetWatchersByAccountId(string accountId)
  {
    string sql = @"
    SELECT
      albums.*,
      watchers.account_id AS account_id,
      watchers.id AS watcher_id,
      accounts.*
    FROM watchers
    INNER JOIN albums ON watchers.album_id = albums.id
    INNER JOIN accounts ON accounts.id = albums.creator_id
    WHERE watchers.account_id = @accountId;";

    List<WatcherAlbum> watchers = _db.Query(
      sql,
      (WatcherAlbum album, Profile account) =>
      {
        album.Creator = account;
        return album;
      },
      new { accountId }).ToList();

    return watchers;
  }

  internal List<WatcherProfile> GetWatchersByAlbumId(int albumId)
  {
    // string sql = @"
    // SELECT
    // watchers.*,
    // accounts.*
    // FROM watchers
    // INNER JOIN accounts ON accounts.id = watchers.account_id
    // WHERE watchers.album_id = @albumId;";

    // List<WatcherProfile> watchers = _db.Query(
    //   sql,
    //   (Watcher watcher, WatcherProfile account) =>
    //   {
    //     account.AlbumId = watcher.AlbumId;
    //     account.WatcherId = watcher.Id;
    //     return account;
    //   },
    //   new { albumId }).ToList();

    string sql = @"
    SELECT
      accounts.*,
      watchers.album_id AS album_id,
      watchers.id AS watcher_id
    FROM watchers
    INNER JOIN accounts ON accounts.id = watchers.account_id
    WHERE watchers.album_id = @albumId;";

    List<WatcherProfile> watchers = _db.Query<WatcherProfile>(sql, new { albumId }).ToList();

    return watchers;
  }
}