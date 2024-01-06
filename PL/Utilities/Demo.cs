using BLL.Services;
using Newtonsoft.Json;

namespace Lab3_3.Utilities;

public class Demo
{
    public static async Task RunAsync(ISpectacleService spectacleService, ITicketService ticketService)
    {
        while (true)
        {
            Console.WriteLine("Вітаємо у системі керування афішами!\n" +
                              "1. Переглянути усі афіши\n" +
                              "2. Пошук\n" +
                              "3. Додати афішу\n" +
                              "4. Додати квитки\n" +
                              "5. Забронювати квитки\n" +
                              "6. Купити квитки\n" +
                              "7. Переглянути замовлення\n" +
                              "8. Оновити замовлення\n" +
                              "9. Вихід\n");

            Console.Write("Ваш вибір: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                var spectacles = await spectacleService.GetSpectaclesAsync();

                Console.WriteLine("Афіши: " + GetJson(spectacles));
            }
            else if (choice == "2")
            {
                Console.Write("Пошук:\n" +
                              "1. За назвою\n" +
                              "2. За датою\n" +
                              "3. За автором\n" +
                              "4. За жанром\n");

                Console.Write("Ваш вибір: ");
                var searchChoice = Console.ReadLine()!;

                Console.Write("Введіть значення для пошуку: ");

                var searchValue = Console.ReadLine()!;

                var res = await spectacleService.GetSpectaclesAsync(searchChoice, searchValue);

                Console.WriteLine("Результати пошуку: " + GetJson(res));
            }
            else if (choice == "3")
            {
                var dto = GetInputs.GetSpectacleDto();

                await spectacleService.CreateSpectacleAsync(dto);
                Console.WriteLine("Афіша успішно додана!");
            }
            else if (choice == "4")
            {
                var dto = GetInputs.GetTicketDto();

                await ticketService.AddTicketAsync(dto);
                Console.WriteLine("Квиток успішно додано!");
            }
            else if (choice == "5")
            {
                var dto = GetInputs.GetOrderDto(false);

                await ticketService.AddOrderAsync(dto);
                Console.WriteLine("Квиток успішно заброньовано!");
            }
            else if (choice == "6")
            {
                var dto = GetInputs.GetOrderDto(true);

                await ticketService.AddOrderAsync(dto);
                Console.WriteLine("Квиток успішно куплено!");
            }
            else if (choice == "7")
            {
                var orders = await ticketService.GetOrdersAsync();

                Console.WriteLine("Замовлення: " + GetJson(orders));
            }
            else if (choice == "8")
            {
                Console.Write("Введіть id замовлення: ");

                var id = int.Parse(Console.ReadLine()!);

                await ticketService.UpdateOrderAsync(id);

                Console.WriteLine("Замовлення успішно оновлено!");
            }
            else if (choice == "9")
            {
                Console.WriteLine("До побачення!");
                break;
            }
            else
                Console.WriteLine("Невірний вибір!");
        }
    }

    private static string GetJson(object data)
    {
        return JsonConvert.SerializeObject(data, Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
    }
}
