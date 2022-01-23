using Jibble.Core.Dto;
using Newtonsoft.Json;
using RestSharp;

namespace Jibble.Core.Services;

public class DataService
{
    public static ODataDto FetchPeople(RestClient client)
    {
        try
        {
            var request = new RestRequest("People");
            var response = client.GetAsync(request).Result;
            var data = JsonConvert.DeserializeObject<ODataDto>(response.Content);

            return data;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static ODataDto SearchPeople(RestClient client, string field, string keyword)
    {
        try
        {
            var request = new RestRequest("People").AddQueryParameter("$filter", string.Format("{0} eq '{1}'", field, keyword));
            var response = client.GetAsync(request).Result;
            var data = JsonConvert.DeserializeObject<ODataDto>(response.Content);

            return data;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static PersonDto GetPerson(RestClient client, string username)
    {
        try
        {
            var request = new RestRequest($"People('{username}')");
            var response = client.GetAsync(request).Result;
            var data = JsonConvert.DeserializeObject<PersonDto>(response.Content);

            return data;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static PersonDto UpdatePerson(RestClient client, string username, UpdatePersonDto payload)
    {
        try
        {
            var request = new RestRequest($"People('{username}')")
                    .AddJsonBody(payload);
            var data = client.PatchAsync<PersonDto>(request).Result;

            return data;
        }
        catch (Exception)
        {
            return null;
        }
    }
}

