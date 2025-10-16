namespace post_it_sharp.Models;

public class Profile : IRepoItem<string>
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Picture { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}

public class Account : Profile
{
  public string Email { get; set; }
  public string CreditCardNumber { get; set; }
  public string HomeAddress { get; set; }
  public bool PicksNose { get; set; }
}

// NOTE for later use
// public class WatcherProfile : Profile
// {
//   public int WatcherId { get; set; }
// }
