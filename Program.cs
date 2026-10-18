using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using ISRPO_Projects.Classes;

namespace ISRPO_Projects
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var service = new TODO_Service();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Показать задачи пользователя");
                Console.WriteLine("2. Найти задачу по ID");
                Console.WriteLine("3. Создать задачу");
                Console.WriteLine("4. Изменить статус задачи");
                Console.WriteLine("5. Удалить задачу");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Введите ID пользователя: ");
                            if (int.TryParse(Console.ReadLine(), out int userId))
                            {
                                // Вызов метода GET /todos?userId={id}
                                var todos = await service.GetTodosByUserId(userId);

                                if (todos != null && todos.Count > 0)
                                {
                                    Console.WriteLine("\n===== Список задач =====");
                                    foreach (var todo in todos)
                                    {
                                        string status = todo.Completed ? "[Выполнено!]" : "[В процессе]";
                                        Console.WriteLine($"ID: {todo.Id}\t| {status} |\t{todo.Title}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("У этого пользователя нет задач.");
                                }
                            }
                            break;

                        case "2":
                            Console.Write("Введите ID задачи: ");
                            if (int.TryParse(Console.ReadLine(), out int taskId))
                            {
                                // Вызов метода GET /todos/{id}
                                var todo = await service.GetTaskByTaskId(taskId);

                                if (todo != null)
                                {
                                    Console.WriteLine("\n===== Информация о задаче =====");
                                    Console.WriteLine($"ID: {todo.Id}");
                                    Console.WriteLine($"Название: {todo.Title}");
                                    Console.WriteLine($"Статус: {(todo.Completed ? "Выполнено" : "Не выполнено")}");
                                }
                                else
                                {
                                    Console.WriteLine("Задача не найдена.");
                                }
                            }
                            break;

                        case "3":
                            Console.Write("Введите ID пользователя: ");
                            int.TryParse(Console.ReadLine(), out int newUserId);
                            Console.Write("Введите название задачи: ");
                            string title = Console.ReadLine();

                            // Создаем объект для отправки
                            var newTask = new TODO
                            {
                                UserId = newUserId,
                                Title = title,
                                Completed = false
                            };

                            // Вызов метода POST /todos
                            var createdTask = await service.CreateTask(newTask);

                            Console.WriteLine($"\nЗадача создана! Сервер присвоил ей ID: {createdTask?.Id}");
                            break;

                        case "4":
                            Console.Write("Введите ID задачи для изменения: ");
                            int.TryParse(Console.ReadLine(), out int patchId);

                            Console.Write("Задача выполнена? (y/n): ");
                            bool isCompleted = Console.ReadLine()?.ToLower() == "y";

                            // Вызов метода PATCH /todos/{id}
                            var updatedTask = await service.UpdateTaskStatus(patchId, isCompleted);

                            Console.WriteLine($"\nСтатус задачи {updatedTask?.Id} обновлен на: {(updatedTask?.Completed == true ? "Выполнено" : "В процессе")}");
                            break;

                        case "5":
                            Console.Write("Введите ID задачи для удаления: ");
                            int.TryParse(Console.ReadLine(), out int deleteId);

                            // Вызов метода DELETE /todos/{id}
                            bool isDeleted = await service.DeleteTask(deleteId);

                            if (isDeleted)
                                Console.WriteLine($"\nЗадача {deleteId} успешно удалена (имитация).");
                            else
                                Console.WriteLine("\nНе удалось удалить задачу.");
                            break;

                        case "0":
                            return; // Выход из программы

                        default:
                            Console.WriteLine("Неверный пункт меню.");
                            break;
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"\n[Ошибка сети]: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[Ошибка]: {ex.Message}");
                }

                Console.WriteLine("\nДля продолжения нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
    }
}
