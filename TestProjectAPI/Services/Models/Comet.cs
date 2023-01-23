namespace TestProjectAPI.Services.Models
{
    public class Comet
    {
        public string Name { get; set; } = String.Empty;
        public int ID { get; set; }
        public string Nametype { get; set; } = String.Empty;
        public string Recclass { get; set; } = String.Empty;
        public double Mass { get; set; }
        public string Fall { get; set; } = String.Empty;
        public DateTime Year { get; set; }
        public double Reclat { get; set; }
        public double Reclong { get; set; }
        public Geolocation Geolocation { get; set; }
    }

    public class Geolocation
    {

    }
}