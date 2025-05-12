namespace Entity.Models;

public partial class Orderitem
{
    public string Id { get; set; } = null!;

    public string Orderid { get; set; } = null!;

    public string Productid { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Unitprice { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
