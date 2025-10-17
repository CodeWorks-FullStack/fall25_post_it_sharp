namespace post_it_sharp.Services;

public class WatchersService
{
  private readonly WatchersRepository _repository;

  public WatchersService(WatchersRepository repository)
  {
    _repository = repository;
  }
}