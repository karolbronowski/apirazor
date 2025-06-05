using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using FinalLabProject.Domain.Common;
using FinalLabProject.Domain.Exceptions.Common;

namespace FinalLabProject.Domain.ValueObjects;

public sealed class Username : ValueObject
{
    public string Value { get; }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidUsernameException(value, "Username cannot be empty.");

        if (!Regex.IsMatch(value, @"^[a-z0-9_]+$"))
            throw new InvalidUsernameException(
                value,
                "It must contain only lowercase letters, numbers, and underscores."
            );

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(Username user) => user.Value;
}