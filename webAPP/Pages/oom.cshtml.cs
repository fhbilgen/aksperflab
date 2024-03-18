using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace webAPP.Pages
{
    public class oomModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string url = Environment.GetEnvironmentVariable("WEBAPIURL") + "oom";

        public IList<string>? Strings { get; set; }

        public void OnGet()
        {
            gsWrapper();
        }

        private static async Task<List<string>> GetStrings()
        {
            List<string> strs = new List<string>();
            var streamTask = await client.GetStreamAsync(url);

            await foreach (var str in JsonSerializer.DeserializeAsyncEnumerable<string>(streamTask))
            {
                strs.Add(str);
            }
            return strs;
        }

        public bool gsWrapper()
        {
            Strings = GetStrings().Result;
            
            if (Strings!= null)
                return true;
            else
                return false;
        }

    }
}
