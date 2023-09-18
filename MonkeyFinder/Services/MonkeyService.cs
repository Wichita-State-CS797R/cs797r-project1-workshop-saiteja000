namespace MonkeyFinder.Services;
using System.Net.Http.Json;
public class MonkeyService
{
    List<Monkey> monkeyList = new();

    HttpClient httpClient;


    

    
    

    public MonkeyService()
    {
        this.httpClient = new HttpClient();
    }
    public async Task<List<Monkey>> GetMonkeys()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        monkeyList = JsonSerializer.Deserialize(contents, MonkeyContext.Default.ListMonkey);

        if (monkeyList?.Count > 0)
            return monkeyList;

        var response = await httpClient.GetAsync("https://www.montemagno.com/monkeys.json");
        if (response.IsSuccessStatusCode)
        {
            monkeyList = await response.Content.ReadFromJsonAsync(MonkeyContext.Default.ListMonkey);
        }
        return monkeyList;

        
    }


    
}
