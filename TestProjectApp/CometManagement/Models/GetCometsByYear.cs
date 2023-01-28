using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TestProjectApp.CometManagement.Models;

public class GetCometsByYearRequest : IValidatableObject
{
    [Required(ErrorMessage = "is required")]
    public int? YearFrom { get; set; }
    [Required(ErrorMessage = "is required")]
    public int? YearTo { get; set; }
    public int? RecclassID { get; set; }
    public string Pattern { get; set; } = string.Empty;
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (!YearFrom.HasValue || YearFrom < 0)
        {
            yield return new ValidationResult(
                $"Year From must be more then 0.",
                new[] { nameof(YearFrom) });
        }
        if (!YearTo.HasValue || YearTo < 0)
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
    public int TotalCount { get; set; } = 0;
    public double TotalMass { get; set; } = 0;
}
public class CometByYear
{
    public int Year { get; set; }
    public int Count { get; set; }
    public double Mass { get; set; }
}
