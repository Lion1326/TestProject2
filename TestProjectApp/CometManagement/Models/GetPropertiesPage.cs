using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Infrastructure.Models;

namespace TestProjectApp.CometManagement.Models;

public class GetPropertiesPageResponse
{
    public List<Recclass> Recclasses { get; set; } = new List<Recclass>();
}