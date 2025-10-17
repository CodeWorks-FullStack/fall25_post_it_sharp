
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
}