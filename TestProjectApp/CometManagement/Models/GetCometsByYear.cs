using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectApp.CometManagement.Models;

public class GetCometsByYearRequest : IValidatableObject
{
    public int YearFrom { get; set; }
    public int YearTo { get; set; }
    public int? RecclassID { get; set; }
    public string Pattern { get; set; } = string.Empty;
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (YearFrom < 0)
        {
            yield return new ValidationResult(
                $"Year From must be more then 0.",
                new[] { nameof(YearFrom) });
        }
        if (YearTo < 0)
        {
            yield return new ValidationResult(
                $"Year To must be more then 0.",
                new[] { nameof(YearTo) });
        }
        if (YearFrom > YearTo)
        {
            yield return new ValidationResult(
                $"Year From must be less then Year To.",
                new[] { nameof(YearFrom) });
        }
    }
}
public class GetCometsByYearResponse
{
    public List<CometByYear> List { get; set; } = new List<CometByYear>();
}
public class CometByYear
{
    public int Year { get; set; }
    public int Count { get; set; }
    public double TotalMass { get; set; }
}
