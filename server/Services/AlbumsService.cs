namespace post_it_sharp.Services;


public class AlbumsService(AlbumsRepository repo)
{
  private readonly AlbumsRepository _repo = repo;

  public Album CreateAlbum(Album albumData)
  {
    Album album = _repo.CreateAlbum(albumData);
    return album;
  }

  public List<Album> GetAllAlbums()
  {
    List<Album> albums = _repo.GetAllAlbums();
    return albums;
  }

  public Album GetOneAlbumById(int albumId)
  {
    Album album = _repo.GetOneAlbumById(albumId);
    return album;
  }

}