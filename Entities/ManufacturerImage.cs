namespace eTech.Entities
{
  public class ManufacturerImage : FileUpload
  {
    public int ManufacturerId { get; set; }
    public Manufacturer Manufacturer { get; set; }
  }
}