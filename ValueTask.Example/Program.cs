
using System.Diagnostics.Metrics;

Dictionary<int, string> _countryCache = new Dictionary<int, string>
{
    { 1, "Australia" },
    { 2, "Canada" },
    { 3, "France" }
};


async ValueTask<Dictionary<int,string>> GetCountries()
{
        if (_countryCache.Any())
        {
            // Return synchronously from cache
            return _countryCache;
        }
        else
        {
            return await FetchCountryDataFromDatabase();
        }
}



// Simulated method to fetch country data from a database
async Task<Dictionary<int, string>> FetchCountryDataFromDatabase()
{

   await Task.Delay(10000); 
    Dictionary<int, string> _dbCountries = new Dictionary<int, string>
    {
        { 1, "Australia" },
        { 2, "Canada" },
        { 3, "France" }
    };
    // In a real scenario, this method would fetch country data from a database
    // For simplicity, using hardcoded values here
    return _dbCountries;
}

Console.WriteLine("Enter x to clear cache or any other key to continue...");
var key = Console.ReadKey();
if(key.KeyChar == 'x')
    _countryCache.Clear();
Console.WriteLine(Environment.NewLine);
Console.WriteLine("Fetching country data...");



var taskToFetchCountries = GetCountries();
if(taskToFetchCountries.IsCompleted)
{
    Console.WriteLine($"Completing synchronously");
    foreach (var country in taskToFetchCountries.Result)
    {
        Console.WriteLine(country);
    }
}
else
{
    Console.WriteLine($"Completing asynchronously");
    var countries = await taskToFetchCountries;
    foreach (var country in countries)
    {
        Console.WriteLine(country);
    }
}

