using MovieNight;

BoxOffice boxOffice = new BoxOffice(new Clock(), new PriceList());

Console.WriteLine($"Tickets right now cost ${boxOffice.TicketPrice()}");

boxOffice.Sell("B7");
boxOffice.Sell("B8");
boxOffice.JoinWaitingList("Ada");

string? next = boxOffice.ReleaseSeat("B8");
Console.WriteLine($"B8 is free again; offer it to {next ?? "nobody"}");
Console.WriteLine($"{boxOffice.SoldCount} seat(s) sold");
