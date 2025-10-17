namespace post_it_sharp.Controllers;

[ApiController]
[Route("api/albums")]
public class AlbumsController(Auth0Provider auth, AlbumsService albumsService, PicturesService picturesService, WatchersService watchersService) : ControllerBase
{
  private readonly Auth0Provider _auth = auth;
  private readonly AlbumsService _albumsService = albumsService;
  private readonly PicturesService _picturesService = picturesService;
  private readonly WatchersService _watchersService = watchersService;


  [HttpPost]
  [Authorize]
  async public Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
  {
    try
    {
      Account user = await _auth.GetUserInfoAsync<Account>(HttpContext);
      albumData.CreatorId = user.Id;
      Album album = _albumsService.CreateAlbum(albumData);
      return Ok(album);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Album>> GetAllAlbums()
  {
    try
    {
      List<Album> albums = _albumsService.GetAllAlbums();
      return Ok(albums);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{albumId}")]
  public ActionResult<Album> GetOneAlbumById(int albumId)
  {
    try
    {
      Album album = _albumsService.GetOneAlbumById(albumId);
      return Ok(album);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{albumId}")]
  [Authorize]
  async public Task<ActionResult<Album>> ArchiveAlbum(int albumId)
  {
    try
    {
      Account user = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Album archivedAlbum = _albumsService.ArchiveAlbum(albumId, user.Id);
      return Ok(archivedAlbum);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{albumId}/pictures")]
  public ActionResult<List<Picture>> GetPicturesByAlbumId(int albumId)
  {
    try
    {
      List<Picture> pictures = _picturesService.GetPicturesByAlbumId(albumId);
      return Ok(pictures);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{albumId}/watchers")]
  public ActionResult<List<WatcherProfile>> GetWatchersByAlbumId(int albumId)
  {
    try
    {
      List<WatcherProfile> watchers = _watchersService.GetWatchersByAlbumId(albumId);
      return Ok(watchers);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}