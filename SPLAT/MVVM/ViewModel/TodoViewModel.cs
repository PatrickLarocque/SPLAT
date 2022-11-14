using Caliburn.Micro;
using Newtonsoft.Json;
using SPLAT.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using SPLAT.MVVM.Models;
using System.Threading.Tasks;

namespace SPLAT.MVVM.ViewModel
{
    internal class TodoViewModel : ObservableObject
    {
        public BindableCollection<Todo> Todo { get; set; }

        public TodoViewModel()
        {
            Todo = new BindableCollection<Todo>();
            _ = GetTodos();

        }
        private async Task GetTodos()
        {
            using (var httpClient = new HttpClient())
            {

                var todosJson = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/todos");
                var todoItems = JsonConvert.DeserializeObject<Todo[]>(todosJson);
                foreach (var item in todoItems)
                {
                    Todo.Add(item);
                }
            }
        }

    }
}
