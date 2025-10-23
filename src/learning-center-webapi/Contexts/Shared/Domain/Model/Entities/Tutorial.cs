using System.ComponentModel.DataAnnotations;

namespace learning_center_webapi.Contexts.Shared.Domain.Model.Entities;

public class Tutorial
{
    public int Id { get; set; }
    public string Title { get; set; }    
    public Byte MyBinary { get; set; }

    public string Description { get; set; }
    public DateTime PublishedDate { get; set; }
}