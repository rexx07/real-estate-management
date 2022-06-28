using Core.Domain.Common;
using Core.Domain.Properties;

namespace Core.Domain.Users;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; }
    public string? Picture { get; set; }
    public PasswordFormat PasswordFormatId { get; set; }
    public string PasswordSalt { get; set; }
    public string? AdminComment { get; set; }
    public bool IsCompany { get; set; } = false;
    public bool Active { get; set; } = true;
    public bool Deleted { get; set; } = false;
    public bool IsAdmin { get; set; } = false;
    public bool IsAgent { get; set; } = false;
    public int FailedLoginAttempts { get; set; }
    public DateTime? CannotLoginUntilDateUtc { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? LastLoginDateUtc { get; set; }
    public DateTime LastActivityDateUtc { get; set; }

    public string? Facebook { get; set; }
    public string? Twitter { get; set; }
    public string? Instagram { get; set; }
    public string? Linkedin { get; set; }

    public ICollection<Address>? UserAddresses { get; set; }

    public ICollection<Property>? Properties { get; set; }
}