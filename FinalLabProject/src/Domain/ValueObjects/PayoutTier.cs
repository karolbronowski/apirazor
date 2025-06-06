using System;
using System.Collections.Generic;
using FinalLabProject.Domain.Common;

namespace FinalLabProject.Domain.ValueObjects;

public sealed class PayoutTier : ValueObject
{
    public int Tier { get; }
    public int ClickThreshold { get; }

    public static readonly PayoutTier Bronze = new(1, 0);
    public static readonly PayoutTier Silver = new(2, 10_000);
    public static readonly PayoutTier Gold = new(3, 100_000);

    public string Name => ToString();

    // New public constructor for string input
    public PayoutTier(string tier)
    {
        var instance = FromString(tier);
        Tier = instance.Tier;
        ClickThreshold = instance.ClickThreshold;
    }

    public static PayoutTier FromClicks(int clicks)
    {
        if (clicks >= Gold.ClickThreshold)
            return Gold;
        if (clicks >= Silver.ClickThreshold)
            return Silver;
        return Bronze;
    }

    public static PayoutTier FromString(string tier)
    {
        return tier switch
        {
            "Bronze" => Bronze,
            "Silver" => Silver,
            "Gold" => Gold,
            _ => throw new ArgumentException($"Invalid payout tier: {tier}")
        };
    }

    private PayoutTier(int tier, int threshold)
    {
        Tier = tier;
        ClickThreshold = threshold;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Tier;
        yield return ClickThreshold;
    }

    public override string ToString() => Tier switch
    {
        1 => "Bronze",
        2 => "Silver",
        3 => "Gold",
        _ => $"Custom({Tier})"
    };
}