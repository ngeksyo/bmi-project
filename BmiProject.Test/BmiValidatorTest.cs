using BmiProject.Service;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace BmiProject.Test;

public class BmiValidatorTest
{

    [Theory]
    [TestCase(-1, 2)]
    [TestCase(-2, -7)]
    public void Validate_ShouldThrowException_WhenHeightIsNegative(double height, double weight)
    {
        // Arrange
        var validator = new BmiValidator();

        // Act
        var ex = Assert.Throws<ValidationException>(() => validator.Validate(height, weight));

        // Assert
        Assert.That(ex.Message, Is.EqualTo("Invalid height or weight"));
    }

    [Theory]
    [TestCase(1, -2)]
    [TestCase(-1, -7)]
    public void Validate_ShouldThrowException_WhenWeightIsNegative(double height, double weight)
    {
        // Arrange
        var validator = new BmiValidator();

        // Act
        var ex = Assert.Throws<ValidationException>(() => validator.Validate(height, weight));

        // Assert
        Assert.That(ex.Message, Is.EqualTo("Invalid height or weight"));
    }

    [Test]
    public void Validation_ShouldRenturnTrue_WhenHeightAndWeightGTEZero()
    {
        // Arrange
        var validator = new BmiValidator();
        var height = 1.5;
        var weight = 70;

        // Act
        var isValid = validator.Validate(height, weight);

        // Assert
        Assert.That(isValid, Is.True);
    }
}
