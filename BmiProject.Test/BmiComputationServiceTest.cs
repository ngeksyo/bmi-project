using BmiProject.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmiProject.Test;

public class BmiComputationServiceTest
{
    private Mock<IBmiValidator> _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new Mock<IBmiValidator>();
    }

    [Test]
    public void Compute_ShouldCall_ValidateOnce()
    {
        // Arrange
        _validator.Setup(x => x.Validate(It.IsAny<double>(), It.IsAny<double>())).Returns(true);
        var bmiService = new BmiComputationService(_validator.Object);

        // Act
        var result = bmiService.Compute(1.5, 70);

        // Assert
        _validator.Verify(x => x.Validate(It.IsAny<double>(), It.IsAny<double>()), Times.Once);
    }

    [Test]
    public void Compute_ShouldThrow_ValidationException()
    {
        // Arrange
        _validator.Setup(x => x.Validate(It.IsAny<double>(), It.IsAny<double>())).Throws(new ValidationException("Exception"));
        var bmiService = new BmiComputationService(_validator.Object);

        // Act
        var ex = Assert.Throws<ValidationException>(() => bmiService.Compute(1.5, 70));

        // Assert
        Assert.That(ex.Message, Is.EqualTo("Exception"));
    }

    [Theory]
    [TestCase(140, 36.25, "Underweight", 18.49)]
    [TestCase(140, 48.8, "Normal weight", 24.9)]
    [TestCase(140, 58.6, "Overweight", 29.9)]
    [TestCase(140, 60, "Obesity", 30.61)]
    public void Compute_ShouldReturnExpectedCategory_WhenGivenHeightAndWeight(double height, double weight, string expectedCategory, double bmi)
    {
        // Arrange
        _validator.Setup(x => x.Validate(It.IsAny<double>(), It.IsAny<double>())).Returns(true);
        var bmiService = new BmiComputationService(_validator.Object);

        // Act
        var result = bmiService.Compute(height, weight);

        // Assert
        Assert.That(result.Category, Is.EqualTo(expectedCategory));
        Assert.That(result.Bmi, Is.EqualTo(bmi));
    }
}
