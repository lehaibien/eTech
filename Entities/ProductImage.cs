namespace eTech.Entities
{
  public class ProductImage : FileUpload
  {
    public int productId { get; set; }
    public Product Product { get; set; }
  }
}