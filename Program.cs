
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Countries
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            List<CountryInfo> countries = await GetCountries();

            if (countries != null) { 
                GenerateCountryDataFiles(countries);
            } else {
                Console.WriteLine("No Countries Data Found");
                return;
            }
        }

        // Uses list of CountryInfo Objects and creates text files for each country.
        static void GenerateCountryDataFiles(List<CountryInfo> countries)
        {
            // Directory path to save files
            string directoryPath = @"C:\Users\Administrator\source\repos\Countries\Country List";

            // Create the directory if it doesn't exist.
            Directory.CreateDirectory(directoryPath);

            foreach (var country in countries)
            {
                // Creating the Path for files
                string filePath = Path.Combine(directoryPath, $"{country.name.common}.txt");

                // Create or overwrite the file
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    // Write the properties to the text file.
                    writer.WriteLine($"Name: {country.name.common}");
                    writer.WriteLine($"Region: {country.region}");
                    writer.WriteLine($"Subregion: {country.subregion}");
                    writer.WriteLine($"Latlng: {string.Join(", ", country.latlng)}");
                    writer.WriteLine($"Area: {country.area}");
                    writer.WriteLine($"Population: {country.population}");
                }
            }

            Console.WriteLine("Country data files generated successfully.");
        }


        // Returns list of CountryInfo objects 
        static async Task<List<CountryInfo>> GetCountries()
        {
            string baseUrl = "https://restcountries.com/";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("v3.1/all");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(content))
                        {
                            // Deserialize the JSON response into a list of CountryInfo objects
                            return JsonConvert.DeserializeObject<List<CountryInfo>>(content);
                        } 
                        else
                        {
                            Console.WriteLine("Response context is Empty or null!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Record found");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }
    }
}
