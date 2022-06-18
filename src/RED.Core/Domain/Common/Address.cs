using Core.Domain.Properties;
using Core.Domain.Users;

namespace Core.Domain.Common;

/// <summary>
/// The address of the property to be sold and the contact of owner
/// </summary>
public class Address: BaseEntity
{
    public int? CountryId { get; set; }
    public int? StateProvinceId { get; set; }
    public string LocalGovernment { get; set; }
    public string City { get; set; }
    public string ZipPostalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string FaxNumber { get; set; }
    public string CustomAttributes { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    
    public int PropertyId { get; set; }
    public Property Property { get; set; }
    public int? OfficeId { get; set; }
    public User User { get; set; }
}