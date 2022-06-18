using Core.Domain.Common;
using Core.Domain.Users;

namespace Core.Domain.Properties;

/// <summary>
/// Property Details
/// </summary>
public class Property: BaseEntity
{
    public string PropertyName { get; set; }
    public string PropertyDescription { get; set; }
    public Decimal PropertyPrice { get; set; }
    public PropertyStatus PropertyStatus { get; set; }
    
    public int SellerId { get; set; }
    public User User { get; set; }
    public int PropertyTypeId { get; set; }
    public PropertyType PropertyType { get; set; }
    
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public ICollection<PropertyImage> PropertyImages { get; set; }
}