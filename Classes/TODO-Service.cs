using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ISRPO_Projects.Classes
{
    internal class TODO_Service
    {
        private readonly HttpClient _client;
        public TODO_Service()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://jsonplaceholder.typicode.com/") };
        }


        public async Task<List<TODO>> GetTodosByUserId(int userId)
        {
            return await _client.GetFromJsonAsync<List<TODO>>($"$\"todos?userId={{userId}}\"");
        }

        public async Task<TODO> GetTaskByTaskId(int taskId) 
        {
            return await _client.GetFromJsonAsync<TODO>($"$\"/todos/{{id}}\"");
        }
    }
}
