using Newtonsoft.Json;

namespace Jibble.Core.Dto;

public class ODataDto
{
    [JsonProperty("@odata.context")]
    public Uri OdataContext { get; set; }

    [JsonProperty("value")]
    public PersonDto[] People { get; set; }
}

public class PersonDto
{
    [JsonProperty("UserName")]
    public string UserName { get; set; }

    [JsonProperty("FirstName")]
    public string FirstName { get; set; }

    [JsonProperty("LastName")]
    public string LastName { get; set; }

    [JsonProperty("MiddleName")]
    public string MiddleName { get; set; }

    [JsonProperty("Gender")]
    public string Gender { get; set; }

    [JsonProperty("Age")]
    public object Age { get; set; }

    [JsonProperty("Emails")]
    public string[] Emails { get; set; }
}

public record UpdatePersonDto(string FirstName, string LastName);

