

namespace TestProject.Services.Models;

public class NasaComet
{
    public int ID { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Nametype { get; set; } = String.Empty;
    public string Recclass { get; set; } = String.Empty;
    public double Mass { get; set; }
    public string Fall { get; set; } = String.Empty;
    public DateTime? Year { get; set; }
    public double Reclat { get; set; }
    public double Reclong { get; set; }
    public Geolocation Geolocation { get; set; }
}

public class Geolocation
{
    public string Type { get; set; } = String.Empty;
    public double[] Coordinates { get; set; }
    public Geolocation()
    {
        Coordinates = new double[0];
    }
}
