using GourmetShopMVCApp.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int SupplierId { get; set; }
    public decimal? UnitPrice { get; set; }
    public string? Package { get; set; }
    public bool IsDiscontinued { get; set; }
    public int? CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Supplier? Supplier { get; set; }
}
