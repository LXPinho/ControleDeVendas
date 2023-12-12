using System.Net.Http.Headers;

namespace ControleDeVendasWebApplication.Tools
{
    public class Tools
    {
        static public HttpClient? _HttpClient = null; // new HttpClient();
        public static HttpClient getInstanceHttpClient()
        {
            try
            {
                if (_HttpClient == null)
                {

                    IConfigurationRoot configurationBuilder = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .AddEnvironmentVariables()
                        .Build();

                    //string connonnectionString = configurationBuilder.GetConnectionString("DefaultConnection") ?? string.Empty;
                    //string url = (configurationBuilder["ASPNETCORE_URLS"] ?? "http://localhost:5206").Split(";").Last();

                    string uri =
                        configurationBuilder
                            .GetSection("WebApplicationSettings")
                            .GetSection("Parameters")
                            .GetValue<string>("Uri") ?? string.Empty;

                    // Update port # in the following line.
                    _HttpClient = new HttpClient();
                    _HttpClient.BaseAddress = new Uri(uri.ToLower());
                    _HttpClient.DefaultRequestHeaders.Accept.Clear();
                    _HttpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                }
            }
            catch
            {
                _HttpClient = null;
            }
            return _HttpClient ?? new HttpClient(); ;
        }
    }
}
