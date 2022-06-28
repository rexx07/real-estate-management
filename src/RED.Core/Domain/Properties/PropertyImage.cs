namespace Core.Domain.Properties;

/// <summary>
///     Images for a property
/// </summary>
public class PropertyImage : BaseEntity
{
    public string ImageName { get; set; }
    public string ImageDescription { get; set; }

    public int PropertyId { get; set; }
    public Property Property { get; set; }
}