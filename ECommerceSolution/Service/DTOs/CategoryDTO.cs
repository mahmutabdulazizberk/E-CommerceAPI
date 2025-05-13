namespace Service.DTOs;

public class CategoryDTO
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? Isactive { get; set; }
}