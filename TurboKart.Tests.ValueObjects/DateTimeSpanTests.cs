using TurboKart.Domain.ValueObjects;

namespace TurboKart.Tests.ValueObjects;

public class DateTimeSpanTests
{
    
    public static IEnumerable<object[]> CheckOverlapTrueData()
    {
        yield return new object[]
        {
            new DateTimeSpan(DateTime.Now, TimeSpan.FromMinutes(20)),
            new DateTimeSpan(DateTime.Now, TimeSpan.FromMinutes(20)),
        };
        
        yield return new object[]
        {
            new DateTimeSpan(DateTime.Now.AddMinutes(19), TimeSpan.FromMinutes(20)),
            new DateTimeSpan(DateTime.Now, TimeSpan.FromMinutes(20)),
        };
        
        yield return new object[]
        {
            new DateTimeSpan(DateTime.Now.AddMinutes(5), TimeSpan.FromMinutes(5)),
            new DateTimeSpan(DateTime.Now, TimeSpan.FromMinutes(20)),
        };
    }
    
    public static IEnumerable<object[]> CheckOverlapFalseData()
    {
        yield return new object[]
        {
            new DateTimeSpan(DateTime.Now, TimeSpan.FromMinutes(20)),
            new DateTimeSpan(DateTime.Now.AddMinutes(20), TimeSpan.FromMinutes(20)),
        };
        
        
        yield return new object[]
        {
            new DateTimeSpan(DateTime.Now.AddMinutes(20), TimeSpan.FromMinutes(20)),
            new DateTimeSpan(DateTime.Now, TimeSpan.FromMinutes(20)),
        };
        
        yield return new object[]
        {
            new DateTimeSpan(DateTime.Now, TimeSpan.FromMinutes(20)),
            new DateTimeSpan(DateTime.Now.AddMinutes(30), TimeSpan.FromMinutes(20)),
        };
        
        yield return new object[]
        {
            new DateTimeSpan(DateTime.Now.AddMinutes(30), TimeSpan.FromMinutes(20)),
            new DateTimeSpan(DateTime.Now, TimeSpan.FromMinutes(20)),
        };
        
    }

    /* TODO Convert to Inlinedata with expected result 
    [Theory]
    public void CheckOverlap_Should_ReturnExpectedResult()
    {
    }
    */
    
    [Theory]
    [MemberData(nameof(CheckOverlapTrueData))]
    public void CheckOverlap_Should_ReturnTrue(DateTimeSpan dateTimeSpan1, DateTimeSpan dateTimeSpan2)
    {
        // Act
        var result = DateTimeSpan.CheckOverlap(dateTimeSpan1, dateTimeSpan2);

        // Assert
        Assert.True(result);
    }
    
    [Theory]
    [MemberData(nameof(CheckOverlapFalseData))]
    public void CheckOverlap_Should_ReturnFalse(DateTimeSpan dateTimeSpan1, DateTimeSpan dateTimeSpan2)
    {
        // Act
        var result = DateTimeSpan.CheckOverlap(dateTimeSpan1, dateTimeSpan2);

        // Assert
        Assert.False(result);
    }
}