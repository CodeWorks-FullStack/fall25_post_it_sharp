using System.Data;

namespace post_it_sharp.Repositories;

public class AlbumsRepository(IDbConnection db)
{
  private readonly IDbConnection _db = db;

  public Album CreateAlbum(Album albumData)
  {
    string sql = @"
      INSERT INTO albums
      (title, cover_img, category, creator_id)
      VALUES
      (@Title, @CoverImg, @Category, @CreatorId);
    
      SELECT
       albums.*,
       accounts.*
      FROM albums
      JOIN accounts ON accounts.id = albums.creator_id
      WHERE albums.id = LAST_INSERT_ID()
    ;";

    Album album = _db.Query(sql,
    (Album album, Account creator) =>
    {
      // NOTE split the joined row into the appropriate models, and combine them into one, C# Model that is returned
      // Console.WriteLine(album);
      // Console.WriteLine(creator);
      album.Creator = creator;
      return album;
      // NOTE could be replaced with PopulateCreator method
    }
     , albumData).SingleOrDefault();
    return album;
  }

  public List<Album> GetAllAlbums()
  {
    string sql = @"
    SELECT
       albums.*,
       accounts.*
    FROM albums
    JOIN accounts ON accounts.id = albums.creator_id
    ;";
    // NOTE the query required type parameters if we are using our Named Map Function
    List<Album> albums = _db.Query<Album, Account, Album>(sql, PopulateCreator).ToList();
    return albums;
  }

  public Album GetOneAlbumById(int albumId)
  {
    string sql = @"
     SELECT
       albums.*,
       accounts.*
    FROM albums
    JOIN accounts ON accounts.id = albums.creator_id
    WHERE albums.id = @albumId
    ;";
    // types for explicit map is, first table type, second table type, return type
    // ......................1.......2.........3
    Album album = _db.Query<Album, Account, Album>(sql, PopulateCreator, new { albumId }).SingleOrDefault();
    return album;
  }

  // ........3...........................1.........2
  private Album PopulateCreator(Album album, Account creator)
  {
    album.Creator = creator;
    return album;
  }

  // NOTE PopulateCreator took the place of this map
  //  (Album album, Account creator) =>
  // {
  //   album.Creator = creator;
  //   return album;
  // }


  public Album ArchiveAlbum(Album archiveData)
  {
    string sql = @"
    UPDATE albums SET
    archived = @Archived
    WHERE id = @Id LIMIT 1;

    SELECT
       albums.*,
       accounts.*
    FROM albums
    JOIN accounts ON accounts.id = albums.creator_id
    WHERE albums.id = @Id
    ;";

    Album album = _db.Query<Album, Account, Album>(sql, PopulateCreator, archiveData).SingleOrDefault();
    return album;
  }
}