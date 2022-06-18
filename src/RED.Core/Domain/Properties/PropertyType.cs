namespace Core.Domain.Properties;
/// <summary>
/// Property Type which acts as a category
/// </summary>
public class PropertyType: BaseEntity
{
    public string PropertyTypeName { get; set; }
    public string PropertyTypeDescription { get; set; }
    
    public int AgentId { get; set; }
    public ICollection<Property> Properties { get; set; }
}