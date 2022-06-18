using Core.Domain.Users;

namespace Core.Domain.Common;

public class Notification:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public int AdminId { get; set; }
    public User Admin { get; set; }
}