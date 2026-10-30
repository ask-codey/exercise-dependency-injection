namespace MovieNight;

// Tells the time. The tests use a fake clock instead, which always says the same time.
public class Clock
{
    public virtual DateTime Now => DateTime.Now;
}
