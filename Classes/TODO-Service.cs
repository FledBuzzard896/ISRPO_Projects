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


        // 1. GET /todos?userId={id}
        public async Task<List<TODO>> GetTodosByUserId(int userId)
        {
            return await _client.GetFromJsonAsync<List<TODO>>($"todos?userId={userId}");
        }

        // 2. GET /todos/{id}
        public async Task<TODO> GetTaskByTaskId(int taskId)
        {
            return await _client.GetFromJsonAsync<TODO>($"todos/{taskId}");
        }

        // 3. POST /todos
        public async Task<TODO> CreateTask(TODO task)
        {
            var response = await _client.PostAsJsonAsync("todos", task);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TODO>();
        }

        // 4. PATCH /todos/{id}
        public async Task<TODO> UpdateTaskStatus(int taskId, bool completed)
        {
            var patchData = new { Completed = completed };

            var response = await _client.PatchAsJsonAsync($"todos/{taskId}", patchData);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TODO>();
        }

        // 5. DELETE /todos/{id}
        public async Task<bool> DeleteTask(int taskId)
        {
            var response = await _client.DeleteAsync($"todos/{taskId}");

            return response.IsSuccessStatusCode;
        }
    }
}
