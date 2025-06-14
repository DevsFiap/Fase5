using Newtonsoft.Json;

namespace Fase5.Api.Middlewares.Models;

public class ErrorResultModel
{
    public int? StatusCode { get; set; }
    public string? Message { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}