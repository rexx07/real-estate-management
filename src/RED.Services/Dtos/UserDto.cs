using Core.Domain.Common;
using Core.Domain.Properties;

namespace RED.Services.Dtos;

public class UserDto
{
    public UserDto()
    {
        UserGuid = new Guid();
    }
    
    public Guid UserGuid { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; }
    public string? Picture { get; set; }
    public bool IsCompany { get; set; } 
    public bool IsAdmin { get; set; } 
    public bool IsAgent { get; set; } 
    public DateTime CreatedOnUtc { get; set; }
    public string? Facebook { get; set; }
    public string? Twitter { get; set; }
    public string? Instagram { get; set; }
    public string? Linkedin { get; set; }

    public ICollection<Address>? UserAddresses { get; set; }

    public ICollection<Property>? Properties { get; set; }
}