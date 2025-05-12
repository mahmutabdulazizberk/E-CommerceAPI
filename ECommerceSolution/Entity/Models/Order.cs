namespace Entity.Models;

public partial class Order
{
    public string Id { get; set; } = null!;

    public string Userid { get; set; } = null!;

    public decimal Totalamount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual User User { get; set; } = null!;
}
