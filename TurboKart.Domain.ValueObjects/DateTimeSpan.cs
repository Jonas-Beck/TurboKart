﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;

namespace TurboKart.Domain.ValueObjects;

[ComplexType]
public class DateTimeSpan
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public TimeSpan Duration { get; init; }

    
    // Empty constructor for EF Core
    public DateTimeSpan()
    {
        
    }
    public DateTimeSpan(DateTime start, DateTime end)
    {
        Duration = CalculateDuration(start, end);
        Start = start;
        End = end;
    }

    public DateTimeSpan(DateTime start, TimeSpan duration)
    {
        Duration = duration;
        Start = start;
        End = CalculateEnd(start, duration);
    }

    public override string ToString()
    {
        // Define a string representation that can be parsed back to your struct
        return $"{Start:yyyy-MM-dd HH:mm:ss} - {End:yyyy-MM-dd HH:mm:ss}";
    }

    public static DateTimeSpan Parse(string value)
    {
        // Parse the string representation and create a new DateTimeSpan instance
        var parts = value.Split(" - ");
        
        if (parts.Length == 2 && DateTime.TryParse(parts[0], out var start) && DateTime.TryParse(parts[1], out var end))
        {
            return new DateTimeSpan(start, end);
        }

        throw new FormatException("Invalid DateTimeSpan string format.");
    }


    public static bool CheckOverlap(DateTimeSpan dateTimeSpan1, DateTimeSpan dateTimeSpan2)
    {
         if (dateTimeSpan1.End == dateTimeSpan2.Start ^ dateTimeSpan2.End == dateTimeSpan1.Start)
         {
             return false;
         }

         return (dateTimeSpan1.End >= dateTimeSpan2.Start) && (dateTimeSpan2.End >= dateTimeSpan1.Start);
    }

    public static TimeSpan CalculateDuration(DateTime start, DateTime end)
    {
        if (!EndIsAfterStart(start, end))
        {
            throw new ArgumentOutOfRangeException(nameof(end), end, "End is not after start.");
        }

        return end - start;
    }

    public static DateTime CalculateEnd(DateTime start, TimeSpan duration)
    {
        return start.Add(duration);
    }

    public static bool EndIsAfterStart(DateTime start, DateTime end)
        => (end > start);

    public static implicit operator TimeSpan(DateTimeSpan dateTimeSpan) => dateTimeSpan.Duration;

    public static implicit operator (DateTime start, DateTime end)(DateTimeSpan dateTimeSpan) =>
        (dateTimeSpan.Start, dateTimeSpan.End);
}