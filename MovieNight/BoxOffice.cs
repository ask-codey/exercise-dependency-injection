namespace MovieNight;

// Sells the seats for one showing.
//
// Requirements:
// - The box office is given its Clock and its PriceList. It never makes its own.
// - TicketPrice is the price list's price at the clock's current time.
// - Sell sells a seat, such as "B7", and returns true. A seat that's already sold isn't sold
//   again: Sell returns false.
// - IsSold says whether a seat is sold, and SoldCount how many seats are sold.
// - JoinWaitingList puts a name on the waiting list, and WaitingCount says how many are waiting.
// - ReleaseSeat makes a sold seat free again, and returns the name of the first person on the
//   waiting list, who is taken off the list to be offered the seat. With nobody waiting, or if
//   the seat wasn't sold, it returns null.
// - UndoLastSale cancels the most recent sale whose seat is still sold, and returns that seat.
//   With nothing to undo, it returns null.
public class BoxOffice
{
    private readonly Clock clock;
    private readonly PriceList priceList;

    public BoxOffice(Clock clock, PriceList priceList)
    {
        this.clock = new Clock();
        this.priceList = new PriceList();
    }

    public decimal TicketPrice()
    {
        return 0;
    }

    public int SoldCount => 0;

    public int WaitingCount => 0;

    public bool Sell(string seat)
    {
        return false;
    }

    public bool IsSold(string seat)
    {
        return false;
    }

    public void JoinWaitingList(string name)
    {
    }

    public string? ReleaseSeat(string seat)
    {
        return null;
    }

    public string? UndoLastSale()
    {
        return null;
    }
}
