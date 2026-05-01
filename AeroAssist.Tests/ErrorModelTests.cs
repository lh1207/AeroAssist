using AeroAssist.Data.Models;
using Xunit;

namespace AeroAssist.Tests;

public class ErrorModelTests
{
    private readonly ErrorModel _model = new("", "");

    [Fact]
    public void OnGet_With404_SetsNotFoundMessage()
    {
        _model.OnGet("404");

        Assert.Equal("404", _model.ErrorCode);
        Assert.Equal("The page you are looking for does not exist.", _model.ErrorMessage);
    }

    [Fact]
    public void OnGet_With500_SetsServerErrorMessage()
    {
        _model.OnGet("500");

        Assert.Equal("500", _model.ErrorCode);
        Assert.Equal("An error occurred while processing your request.", _model.ErrorMessage);
    }

    [Fact]
    public void OnGet_WithUnknownCode_SetsDefaultMessage()
    {
        _model.OnGet("418");

        Assert.Equal("418", _model.ErrorCode);
        Assert.Equal("An error occurred while processing your request.", _model.ErrorMessage);
    }
}
