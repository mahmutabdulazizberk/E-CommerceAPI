using System.Text.Json.Serialization;

namespace Entity.Models;

public partial class Cart
{
    public string Id { get; set; } = null!;

    public string Userid { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    [JsonIgnore]
    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();

    public virtual User User { get; set; } = null!;
}
