namespace WorkBoard.Domain.Shared;


public static class OrderHelper
{
    private const decimal STEP = 1000m;

    public static decimal GetNewOrder(decimal? prev, decimal? next)
    {
        if (prev == null && next == null)
            return STEP;

        if (prev == null)
            return next!.Value - STEP;

        if (next == null)
            return prev.Value + STEP;

        return (prev.Value + next.Value) / 2;
    }
}
