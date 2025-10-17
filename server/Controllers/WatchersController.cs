namespace post_it_sharp.Controllers;

[ApiController]
[Route("api/watchers")]
public class WatchersController : ControllerBase
{
  private readonly WatchersService _watchersService;
  private readonly Auth0Provider _auth;

  public WatchersController(WatchersService watchersService, Auth0Provider auth)
  {
    _watchersService = watchersService;
    _auth = auth;
  }


}