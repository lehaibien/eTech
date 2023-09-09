namespace eTech.Entities
{
  public class CategoryImage : FileUpload
  {
    public int CategoryId { get; set; }
    public Category Category { get; set; }
  }
}