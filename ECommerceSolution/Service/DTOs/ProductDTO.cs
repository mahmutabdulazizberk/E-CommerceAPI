namespace Service.DTOs;

public class ProductDTO
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stockquantity { get; set; }

    public string Categoryid { get; set; } = null!;

    public bool? Isactive { get; set; }
}