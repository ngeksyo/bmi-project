using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BmiProject.Infra;
using BmiProject.Service;

namespace BmiProject.Test;

public class ApiExceptionFilterAttributeTest
{
    private ApiExceptionFilterAttribute _filter;
    private ExceptionContext _context;

    [SetUp]
    public void SetUp()
    {
        _filter = new ApiExceptionFilterAttribute();
        _context = new ExceptionContext(
            new ActionContext(
                new DefaultHttpContext(),
                new Microsoft.AspNetCore.Routing.RouteData(),
                new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor()),
            new List<IFilterMetadata>()
        );
    }

    [Test]
    public void OnException_SetsBadRequest_WhenValidationErrorIsThrown()
    {
        // Arrange
        _context.Exception = new ValidationException("Custom error message");

        // Act
        _filter.OnException(_context);

        // Assert
        var result = _context.Result as BadRequestObjectResult;
        Assert.IsNotNull(result);
        Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
    }
}
