namespace post_it_sharp.Controllers;

[ApiController]
[Route("api/albums")]
public class AlbumsController(Auth0Provider auth, AlbumsService albumsService) : ControllerBase
{
  private readonly Auth0Provider _auth = auth;
  private readonly AlbumsService _albumsService = albumsService;


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
}