using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeacherProblem.Model;

namespace TeacherProblemApp
{
    public class DataProxy
    {
        public async Task<Result> GetData(string apiUrl, Data data)
        {
            Result result = null;

            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<Result>(responseBody);
            }

            return result;
        }
    }
}
