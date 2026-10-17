using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;

namespace ISRPO_Projects
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
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
                            break;
                        case "2":
                            break;
                        case "3":
                            break;
                        case "4":
                            break;
                        case "5":
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                            Console.ReadKey();
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
