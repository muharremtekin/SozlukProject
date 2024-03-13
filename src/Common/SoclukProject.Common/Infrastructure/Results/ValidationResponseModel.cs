using System.Text.Json.Serialization;

namespace SoclukProject.Common.Infrastructure.Results;
public sealed class ValidationResponseModel
{
    public ValidationResponseModel(IEnumerable<string> errors)
    {
        Errors = errors;
    }
    public ValidationResponseModel(string message) : this(new List<string>() { message })
    {

    }

    public IEnumerable<string> Errors { get; set; }

    [JsonIgnore]
    public string FlattenError => Errors != null
        ? string.Join(Environment.NewLine, Errors)
        : string.Empty;
}

