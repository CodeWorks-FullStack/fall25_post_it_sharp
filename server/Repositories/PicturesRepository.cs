



namespace post_it_sharp.Repositories;

public class PicturesRepository
{
  private readonly IDbConnection _db;

  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    string sql = @"
    INSERT INTO
    pictures(creator_id, album_id, img_url)
    VALUES(@CreatorId, @AlbumId, @ImgUrl);
    
    SELECT
    pictures.*,
    accounts.*
    FROM pictures
    INNER JOIN accounts ON accounts.id = pictures.creator_id
    WHERE pictures.id = LAST_INSERT_ID();";

    Picture createdPicture = _db.Query(
      sql,
      (Picture picture, Profile account) =>
      {
        picture.Creator = account;
        return picture;
      },
      pictureData).SingleOrDefault();
    return createdPicture;
  }

  internal void DeletePicture(int pictureId)
  {
    string sql = "DELETE FROM pictures WHERE id = @pictureId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { pictureId });

    if (rowsAffected != 1)
    {
      throw new Exception(rowsAffected + " rows have been deleted, and that is a problem!");
    }
  }

  internal Picture GetPictureById(int pictureId)
  {
    string sql = "SELECT * FROM pictures WHERE id = @pictureId;";
    //                                            { pictureId: 7 }
    Picture picture = _db.Query<Picture>(sql, new { pictureId }).SingleOrDefault();

    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    pictures.*,
    accounts.*
    FROM pictures
    INNER JOIN accounts ON accounts.id = pictures.creator_id
    WHERE pictures.album_id = @albumId;";

    List<Picture> pictures = _db.Query(
      sql,
      (Picture picture, Profile account) =>
      {
        picture.Creator = account;
        return picture;
      },
      new { albumId }).ToList();

    return pictures;
  }
}