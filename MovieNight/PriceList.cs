namespace MovieNight;

// The cinema's ticket prices: a matinee (before 5 PM) costs $8.00, an evening ticket $12.00.
public class PriceList
{
    public const decimal MatineePrice = 8.00m;
    public const decimal EveningPrice = 12.00m;

    public virtual decimal PriceAt(DateTime time)
    {
        return time.Hour < 17 ? MatineePrice : EveningPrice;
    }
}
