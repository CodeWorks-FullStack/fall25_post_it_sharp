namespace post_it_sharp.Controllers;

[ApiController]
[Route("api/pictures")]
public class PicturesController : ControllerBase
{

  private readonly PicturesService _picturesService;
  private readonly Auth0Provider _auth;

  public PicturesController(PicturesService picturesService, Auth0Provider auth)
  {
    _picturesService = picturesService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Picture>> CreatePicture()
  {
    try
    {
      Picture picture = _picturesService.CreatePicture();
      return Ok(picture);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}