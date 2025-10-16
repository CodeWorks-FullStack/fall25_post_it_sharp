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

  // NOTE this archiveAlbum works much like an update
  public Album ArchiveAlbum(int albumId, string userId)
  {
    // verify owner
    Album albumToArchive = GetOneAlbumById(albumId);
    if (userId != albumToArchive.CreatorId) throw new Exception("I know what you are. 🫵😠");

    // NOTE change archived (or any other properties, if you pass update data from controller, from body)
    albumToArchive.Archived = !albumToArchive.Archived;
    // albumToArchive.Title = updateData.Title ?? albumToArchive.Title;


    // Send to repo to save
    Album album = _repo.ArchiveAlbum(albumToArchive);
    return album;
  }

}