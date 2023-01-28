using TestProject.Infrastructure.Models;

namespace TestProjectApp.CometManagement.Models;

public class GetPropertiesPageResponse
{
    public List<Recclass> Recclasses { get; set; } = new List<Recclass>();
    public List<int> Years { get; set; }= new List<int>();
}