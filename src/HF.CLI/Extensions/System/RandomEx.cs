namespace System;

public static class RandomEx
{
    public static Decimal NextDecimal(this Random rng, Decimal min, Decimal max)
    {
        var diff = max - min;
        var amt  = (Decimal) rng.NextDouble() * diff;

        return min + amt;
    }
}
