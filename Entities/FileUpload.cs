using System.ComponentModel.DataAnnotations;

namespace eTech.Entities
{
  public class FileUpload
  {
    [Key]
    public long Id { get; set; }
    public string FilePath { get; set; }
    public string FileName { get; set; }
    public string OriginalFileName { get; set; }
    public long FileSize { get; set; }
  }
}