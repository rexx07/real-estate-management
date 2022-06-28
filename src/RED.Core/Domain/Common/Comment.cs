using Core.Domain.Properties;
using Core.Domain.Users;

namespace Core.Domain.Common;

public class Comment : BaseEntity
{
    public string Content { get; set; }
    public DateTime CommentDate { get; set; }
    public CommentStatus CommentStatus { get; set; }

    public int PropertyId { get; set; }
    public Property Property { get; set; }
    public int BuyerId { get; set; }
    public User Buyer { get; set; }
    public int SellerId { get; set; }
    public User Seller { get; set; }
    public int AdminId { get; set; }
    public User Admin { get; set; }
}