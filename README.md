# Practice: Dependency Injection

Practice code for the lesson **Practice: Dependency Injection** in the C# Full-Stack Bootcamp on [ask-codey.com](https://www.ask-codey.com).

A small cinema needs a box office for its movie nights: ticket prices that change with the time of day, seats that can only be sold once, a waiting list for a full house, and an undo for the sale the cashier didn't mean to make. You'll write the `BoxOffice` class.

## What's here

- `MovieNight/Clock.cs`: tells the time. Ready to use.
- `MovieNight/PriceList.cs`: a matinee (before 5 PM) costs $8.00, an evening ticket $12.00. Ready to use.
- `MovieNight/BoxOffice.cs`: for you to finish. The requirements are in the comment at the top.
- `MovieNight.Tests`: tests for `BoxOffice`, with a fake clock and a fake price list. You don't need to change these.

## What to do

1. **Fix the constructor.** It's given a `Clock` and a `PriceList`, but it ignores them and makes its own. Keep the ones it's given.
2. **`TicketPrice`** asks the price list for the price at the clock's current time.
3. **Seats:** `Sell`, `IsSold` and `SoldCount`. A seat can only be sold once.
4. **The waiting list:** `JoinWaitingList`, `WaitingCount`, and `ReleaseSeat`, which frees a seat and returns the first person waiting.
5. **Undo:** `UndoLastSale` cancels the most recent sale whose seat is still sold.

Run the program with **Ctrl+F5**, and the tests with **Test > Run All Tests** (or `dotnet test`).

## Tips

- Pick one collection per job. Sold seats can't repeat; the waiting list is first come, first served; undo works newest first.
- `HashSet.Add` and `HashSet.Remove` return whether they changed anything. That answers half the requirements by itself.
- The tests pass a fake clock that's always 2 PM or 7 PM, and a fake price list. That only works if `BoxOffice` uses what it's given.
- `TryDequeue` and `TryPop` return `false` when there's nothing left, instead of throwing.

## Hand it in

When it works, push your solution to a public repo of your own on GitHub, then paste that repo's address into **Submit your exercise** on the lesson page at [ask-codey.com](https://www.ask-codey.com). That's what completes the lesson. The lesson page shows the Git commands, step by step.
