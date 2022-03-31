using System;
using System.Collections.Generic;
using Therania.Utilities;
using Xunit;

namespace Therania.Tests;

public class MinimumAgeAttributeTest
{
    private int _minAge = 18;
    [Fact]
    public void MinimumAgeAttribute_FailsIfLessThan18()
    {
        //Arrange
        var enteredDate = new DateTime(DateTime.Now.Year - 15, DateTime.Now.Month, DateTime.Now.Day);
        MinimumAgeAttribute minimumAgeAttribute = new(_minAge);
        
        //Act
        var result = minimumAgeAttribute.IsValid(enteredDate);
        
        //Assert
        Assert.False(result);
    }
    
   [Fact] 
    public void MinimumAgeAttribute_SucceedsIfMoreThan18()
    {
        //Arrange
        var enteredDate = new DateTime(DateTime.Now.Year - 20, DateTime.Now.Month, DateTime.Now.Day);
        MinimumAgeAttribute minimumAgeAttribute = new(_minAge);
        
        //Act
        var result = minimumAgeAttribute.IsValid(enteredDate);
        
        //Assert
        Assert.True(result);
    }
    
   [Fact] 
    public void MinimumAgeAttribute_SucceedsIfEqual18()
    {
        //Arrange
        var enteredDate = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day);
        MinimumAgeAttribute minimumAgeAttribute = new(_minAge);
        
        //Act
        var result = minimumAgeAttribute.IsValid(enteredDate);
        
        //Assert
        Assert.True(result);
    }
    
}