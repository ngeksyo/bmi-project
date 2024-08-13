using BmiProject.Controllers;
using BmiProject.Service;
using BmiProject.Service.Models;
using BmiProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmiProject.Test;

public class BmiControllerTest
{
    private Mock<IBmiComputationService> _bmiComputationService;

    [SetUp]
    public void SetUp()
    {
        _bmiComputationService = new Mock<IBmiComputationService>();
    }

    [Test]
    public void Compute_ShouldAlwaysReturnOkResult_WhenNoExceptionIsThrown()
    {
        // Arrange
        _bmiComputationService.Setup(x => x.Compute(It.IsAny<double>(), It.IsAny<double>())).Returns(new BmiResult { Category = "Normal weight", Bmi = 24.89795918 });
        var controller = new BmiController(_bmiComputationService.Object);

        // Act
        var result = controller.Compute(new BmiInputModel {});

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public void Compute_ShouldAlwaysThrow_WhenValidationExceptionIsThrown()
    {
        // Arrange
        _bmiComputationService.Setup(x => x.Compute(It.IsAny<double>(), It.IsAny<double>())).Throws(new ValidationException("Validaion exception"));
        var controller = new BmiController(_bmiComputationService.Object);

        // Act & Assert
        Assert.Throws<ValidationException>(() => controller.Compute(new BmiInputModel { }));
    }

    [Test]
    public void Compute_CallsBmiComputationServiceCompute_WithCorrectParameters()
    {
        // Arrange
        var height = 140;
        var weight = 48.8;
        _bmiComputationService.Setup(x => x.Compute(It.IsAny<double>(), It.IsAny<double>())).Returns(new BmiResult { Category = "Normal weight", Bmi = 24.89795918 });
        var controller = new BmiController(_bmiComputationService.Object);

        // Act
        var result = controller.Compute(new BmiInputModel { Height = height, Weight = weight });

        // Assert
        _bmiComputationService.Verify(x => x.Compute(It.Is<double>(d => d == height), It.Is<double>(d => d == weight)), Times.Once);
    }
}
