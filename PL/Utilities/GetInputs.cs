using BLL.DTOs;

namespace Lab3_3.Utilities;

public class GetInputs
{
    public static SpectacleDto GetSpectacleDto()
    {
        Console.Write("Введіть назву вистави: ");
        var name = Console.ReadLine()!;

        Console.Write("Введіть опис вистави: ");
        var description = Console.ReadLine()!;

        Console.Write("Введіть автора вистави: ");
        var author = Console.ReadLine()!;

        Console.Write("Введіть жанр вистави: ");
        var genre = Console.ReadLine()!;

        Console.Write("Введіть дату вистави: ");
        var date = DateTime.Parse(Console.ReadLine()!);

        return new SpectacleDto
        {
            Name = name,
            Description = description,
            Author = author,
            Genre = genre,
            Date = date
        };
    }

    public static OrderDto GetOrderDto(bool isPaid)
    {
        Console.Write("Введіть кількість квитків: ");
        var quantity = int.Parse(Console.ReadLine()!);

        Console.Write("Введіть id квитка: ");
        var ticketId = int.Parse(Console.ReadLine()!);

        return new OrderDto
        {
            Quantity = quantity,
            TicketId = ticketId,
            IsPaid = isPaid
        };
    }

    public static TicketDto GetTicketDto()
    {
        Console.Write("Введіть назву квитка: ");
        var name = Console.ReadLine()!;

        Console.Write("Введіть ціну квитка: ");
        var price = int.Parse(Console.ReadLine()!);

        Console.Write("Введіть кількість квитків: ");
        var amount = int.Parse(Console.ReadLine()!);

        Console.Write("Введіть id вистави: ");
        var spectacleId = int.Parse(Console.ReadLine()!);

        return new TicketDto
        {
            Name = name,
            Price = price,
            Amount = amount,
            SpectacleId = spectacleId
        };
    }
}
