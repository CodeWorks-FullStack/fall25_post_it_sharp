

namespace post_it_sharp.Services;

public class PicturesService
{
  private readonly PicturesRepository _repository;
  private readonly AlbumsService _albumsService;

  public PicturesService(PicturesRepository repository, AlbumsService albumsService)
  {
    _repository = repository;
    _albumsService = albumsService;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    // NOTE perform business logic BEFORE adding data to the database
    Album album = _albumsService.GetOneAlbumById(pictureData.AlbumId);

    if (album.Archived)
    {
      throw new Exception($"{album.Title} is archived and no longer accepting new pictures.");
    }

    Picture picture = _repository.CreatePicture(pictureData);
    return picture;
  }

  internal void DeletePicture(int pictureId, string userId)
  {
    Picture picture = GetPictureById(pictureId);

    if (picture.CreatorId != userId)
    {
      throw new Exception("YOU CANNOT DELETE A PICTURE THAT YOU DID NOT CREATE!");
    }

    _repository.DeletePicture(pictureId);
  }

  private Picture GetPictureById(int pictureId)
  {
    Picture picture = _repository.GetPictureById(pictureId);
    if (picture == null) throw new Exception("No picture found with id: " + pictureId);
    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    List<Picture> pictures = _repository.GetPicturesByAlbumId(albumId);
    return pictures;
  }
}