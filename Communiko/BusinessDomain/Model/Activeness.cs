using BusinessDomain.Commenting;

namespace BusinessDomain.Model
{
  public class Activeness
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public string City { get; set; }
    public DateOnly PointTime { get; set; }
    public string Location { get; set; }
    public bool IsActual { get; set; }
    public ICollection<AppUserActiveness> Participants { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public Activeness()
    {
      Participants = new List<AppUserActiveness>();
      Comments = new List<Comment>();
    }
  }
}