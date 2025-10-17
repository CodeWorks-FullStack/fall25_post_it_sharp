
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
}