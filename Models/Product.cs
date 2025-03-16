using GourmetShopMVCApp.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int SupplierId { get; set; }
    public decimal? UnitPrice { get; set; }
    public string? Package { get; set; }
    public bool IsDiscontinued { get; set; }

    //public string CategoryName { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Supplier Supplier { get; set; }
}
