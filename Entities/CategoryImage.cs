namespace eTech.Entities
{
  public class CategoryImage : Image
  {
    public int CategoryId { get; set; }
    public Category Category { get; set; }
  }
}