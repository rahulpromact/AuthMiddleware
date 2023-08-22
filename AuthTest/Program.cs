
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AuthTest;

var url = "http://localhost:5108";

Input input = new Input();
var getCredentials = input.TakeInput();

var client = new HttpClient();
client.BaseAddress = new Uri(url);
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
    "Basic", Convert.ToBase64String(
        Encoding.ASCII.GetBytes($"{getCredentials.Username}:{getCredentials.Password}")));
        
var response = await client.GetAsync("/WeatherForecast");

if(response.IsSuccessStatusCode)
{
    var result = await response.Content.ReadAsStringAsync();
    var deserializedResult = JsonSerializer.Deserialize<object>(result);

    var formattedJson = JsonSerializer.Serialize(deserializedResult, new JsonSerializerOptions
    {
        WriteIndented = true // This option enables formatting
    });

    Console.WriteLine(formattedJson);
}
else
{
    Console.WriteLine(response.StatusCode.ToString());
}