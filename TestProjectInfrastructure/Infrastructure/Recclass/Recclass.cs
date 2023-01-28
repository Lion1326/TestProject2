namespace TestProject.Infrastructure.Models;

public class Recclass
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        Recclass other = (Recclass)obj;
        return Name == other.Name;
    }
    public override int GetHashCode() { return Name.GetHashCode(); }

}
