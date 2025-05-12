namespace Entity.Models;

public partial class Cartitem
{
    public string Id { get; set; } = null!;

    public string Cartid { get; set; } = null!;

    public string Productid { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
