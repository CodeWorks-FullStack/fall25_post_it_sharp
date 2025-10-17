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
  public async Task<ActionResult<Picture>> CreatePicture([FromBody] Picture pictureData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      pictureData.CreatorId = userInfo.Id;
      Picture picture = _picturesService.CreatePicture(pictureData);
      return Ok(picture);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpDelete("{pictureId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeletePicture(int pictureId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      _picturesService.DeletePicture(pictureId, userInfo.Id);
      return Ok("Picture was successfully deleted!");
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}