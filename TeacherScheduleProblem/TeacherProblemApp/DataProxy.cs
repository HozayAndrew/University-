using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TeacherProblemApp
{
    public class DataProxy
    {
        public async Task<Result> GetData(string apiUrl, Data data)
        {
            Result result = null;

            using (var httpClient = new HttpClient() { Timeout = TimeSpan.FromMinutes(10) })
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

        public async Task<List<ExperimentResult>> GetExperimentResult(string apiUrl, List<Data> data)
        {
            List<ExperimentResult> result = null;

            using (var httpClient = new HttpClient() { Timeout = TimeSpan.FromMinutes(10) })
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<List<ExperimentResult>>(responseBody);
            }

            return result;
        }

        public async Task<List<Data>> GetProblems(string apiUrl, ExperimentsSettings settings)
        {
            List<Data> result = null;

            using (var httpClient = new HttpClient() { Timeout = TimeSpan.FromMinutes(10) })
            {
                var json = JsonConvert.SerializeObject(settings);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<List<Data>>(responseBody);
            }

            return result;
        }
    }
}
