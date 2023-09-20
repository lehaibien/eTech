namespace eTech.Entities.Requests {
  public class RatingRequestAdd {
    public int ProductId { get; set; }
    public string UserId { get; set; }
    public double Rate { get; set; }
    public string? Comment { get; set; }
  }
}
