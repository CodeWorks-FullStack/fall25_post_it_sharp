using System.ComponentModel.DataAnnotations;

namespace post_it_sharp.Models;

public class Profile : IRepoItem<string>
{
  public string Id { get; set; }
  public string Name { get; set; }
  [Url] public string Picture { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}

public class Account : Profile
{
  public string Email { get; set; }
  [CreditCard] public string CreditCardNumber { get; set; }
  public string HomeAddress { get; set; }
  public bool PicksNose { get; set; }
}