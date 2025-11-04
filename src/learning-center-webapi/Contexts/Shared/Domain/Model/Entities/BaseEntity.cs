namespace learning_center_webapi.Contexts.Shared.Domain.Model.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    
    public int IsDeleted { get; set; } = 0;
}