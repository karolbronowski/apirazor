using System;

namespace FinalLabProject.Domain.Exceptions.Common;
// well it's not really a common exception, but it is not also defined in the Artist or Listener namespace, but in its own enum, hence i think this is good place for that.
public class InvalidPayoutTierException : Exception
{
    public InvalidPayoutTierException(int payoutValue)
        : base($"Invalid payout tier value: {payoutValue}")
    {
    }
}