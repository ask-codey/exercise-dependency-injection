namespace MovieNight.Tests;

[TestClass]
public class BoxOfficeTests
{
    // A fake clock: it's always the time it was given.
    private class FixedClock : Clock
    {
        private readonly DateTime now;

        public FixedClock(int hour)
        {
            now = new DateTime(2026, 3, 14, hour, 0, 0);
        }

        public override DateTime Now => now;
    }

    // A fake price list: every ticket costs the same, whatever the time.
    private class FlatPriceList : PriceList
    {
        private readonly decimal price;

        public FlatPriceList(decimal price)
        {
            this.price = price;
        }

        public override decimal PriceAt(DateTime time) => price;
    }

    private static BoxOffice NewBoxOffice()
    {
        return new BoxOffice(new FixedClock(19), new PriceList());
    }

    [TestMethod]
    public void TicketPrice_BeforeFivePm_IsTheMatineePrice()
    {
        BoxOffice boxOffice = new BoxOffice(new FixedClock(14), new PriceList());

        Assert.AreEqual(8.00m, boxOffice.TicketPrice());
    }

    [TestMethod]
    public void TicketPrice_FromFivePm_IsTheEveningPrice()
    {
        BoxOffice boxOffice = new BoxOffice(new FixedClock(17), new PriceList());

        Assert.AreEqual(12.00m, boxOffice.TicketPrice());
    }

    [TestMethod]
    public void TicketPrice_ComesFromThePriceListItWasGiven()
    {
        BoxOffice boxOffice = new BoxOffice(new FixedClock(19), new FlatPriceList(5.00m));

        Assert.AreEqual(5.00m, boxOffice.TicketPrice(), "Use the price list passed to the constructor, not a new one.");
    }

    [TestMethod]
    public void Sell_AFreeSeat_SellsIt()
    {
        BoxOffice boxOffice = NewBoxOffice();

        bool sold = boxOffice.Sell("B7");

        Assert.IsTrue(sold);
        Assert.IsTrue(boxOffice.IsSold("B7"));
        Assert.AreEqual(1, boxOffice.SoldCount);
    }

    [TestMethod]
    public void Sell_ASeatAlreadySold_IsRefused()
    {
        BoxOffice boxOffice = NewBoxOffice();
        boxOffice.Sell("B7");

        bool sold = boxOffice.Sell("B7");

        Assert.IsFalse(sold);
        Assert.AreEqual(1, boxOffice.SoldCount);
    }

    [TestMethod]
    public void IsSold_ForASeatNobodyBought_IsFalse()
    {
        BoxOffice boxOffice = NewBoxOffice();
        boxOffice.Sell("B7");

        Assert.IsFalse(boxOffice.IsSold("B8"));
        Assert.IsTrue(boxOffice.IsSold("B7"));
    }

    [TestMethod]
    public void JoinWaitingList_CountsThePeopleWaiting()
    {
        BoxOffice boxOffice = NewBoxOffice();

        boxOffice.JoinWaitingList("Ada");
        boxOffice.JoinWaitingList("Grace");

        Assert.AreEqual(2, boxOffice.WaitingCount);
    }

    [TestMethod]
    public void ReleaseSeat_FreesTheSeat_AndOffersItToTheFirstPersonWaiting()
    {
        BoxOffice boxOffice = NewBoxOffice();
        boxOffice.Sell("B7");
        boxOffice.JoinWaitingList("Ada");
        boxOffice.JoinWaitingList("Grace");

        string? next = boxOffice.ReleaseSeat("B7");

        Assert.AreEqual("Ada", next, "The first to join the waiting list is the first offered a seat.");
        Assert.IsFalse(boxOffice.IsSold("B7"));
        Assert.AreEqual(1, boxOffice.WaitingCount);
    }

    [TestMethod]
    public void ReleaseSeat_WithNobodyWaiting_ReturnsNull()
    {
        BoxOffice boxOffice = NewBoxOffice();
        boxOffice.Sell("B7");
        boxOffice.Sell("B8");

        string? next = boxOffice.ReleaseSeat("B7");

        Assert.IsNull(next);
        Assert.AreEqual(1, boxOffice.SoldCount);
    }

    [TestMethod]
    public void ReleaseSeat_ThatWasNotSold_LeavesTheWaitingListAlone()
    {
        BoxOffice boxOffice = NewBoxOffice();
        boxOffice.JoinWaitingList("Ada");

        string? next = boxOffice.ReleaseSeat("B7");

        Assert.IsNull(next);
        Assert.AreEqual(1, boxOffice.WaitingCount);
    }

    [TestMethod]
    public void UndoLastSale_CancelsTheMostRecentSale()
    {
        BoxOffice boxOffice = NewBoxOffice();
        boxOffice.Sell("B7");
        boxOffice.Sell("B8");

        string? undone = boxOffice.UndoLastSale();

        Assert.AreEqual("B8", undone);
        Assert.IsFalse(boxOffice.IsSold("B8"));
        Assert.IsTrue(boxOffice.IsSold("B7"));
    }

    [TestMethod]
    public void UndoLastSale_Twice_CancelsSalesNewestFirst()
    {
        BoxOffice boxOffice = NewBoxOffice();
        boxOffice.Sell("A1");
        boxOffice.Sell("A2");
        boxOffice.Sell("A3");

        boxOffice.UndoLastSale();
        string? undone = boxOffice.UndoLastSale();

        Assert.AreEqual("A2", undone);
        Assert.AreEqual(1, boxOffice.SoldCount);
    }

    [TestMethod]
    public void UndoLastSale_SkipsASeatAlreadyReleased()
    {
        BoxOffice boxOffice = NewBoxOffice();
        boxOffice.Sell("B7");
        boxOffice.Sell("B8");
        boxOffice.ReleaseSeat("B8");

        string? undone = boxOffice.UndoLastSale();

        Assert.AreEqual("B7", undone, "B8 was released already, so the most recent sale still sold is B7.");
    }

    [TestMethod]
    public void UndoLastSale_WithNothingLeftToUndo_ReturnsNull()
    {
        BoxOffice boxOffice = NewBoxOffice();
        boxOffice.Sell("A1");

        string? first = boxOffice.UndoLastSale();
        string? second = boxOffice.UndoLastSale();

        Assert.AreEqual("A1", first);
        Assert.IsNull(second);
    }
}
