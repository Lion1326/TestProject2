namespace TestProject.Infrastructure.Models;

public class Comet
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Nametype { get; set; } = string.Empty;
    public int RecclassID { get; set; }
    public Recclass Recclass { get; set; }
    public double Mass { get; set; }
    public bool Fall { get; set; }
    public DateTime? Year { get; set; }
    public double Reclat { get; set; }
    public double Reclong { get; set; }
    public string GeolocationType { get; set; } = string.Empty;
}