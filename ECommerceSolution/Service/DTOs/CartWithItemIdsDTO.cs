using Entity.Models;

namespace Service.DTOs;

public sealed class CartWithItemIdsDTO
{
    public Cart Cart { get; init; }           // Sepet
    public IList<string> ItemIds { get; init; } = new List<string>(); // Id listesi
}