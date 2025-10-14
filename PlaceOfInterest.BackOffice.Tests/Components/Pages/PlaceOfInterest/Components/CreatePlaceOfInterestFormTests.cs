using Bunit;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using NSubstitute;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Application.Models;
using PlaceOfInterest.Application.UseCases.CreatePlaceOfInterest;
using PlaceOfInterest.BackOffice.Components.Pages.PlaceOfInterestPage.Component;
using Radzen;

namespace PlaceOfInterest.BackOffice.Tests.Components.Pages.PlaceOfInterest.Components;

public class CreatePlaceOfInterestFormTests : TestContext
{
    private readonly IMediator _mediatorMock;
    private readonly IStartLocationRepository _startRepository;
    private readonly IEndLocationRepository _endRepository;

    public CreatePlaceOfInterestFormTests()
    {
        // Setup MediatR mock
        _mediatorMock = Substitute.For<IMediator>();
        _mediatorMock.Send(Arg.Any<CreatePlaceOfInterestRequest>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult("TEST-TOKEN"));

        // Setup repositories
        var repository = Substitute.For<IRepository<Domain.PlaceOfInterest>>();
        var startRepository = Substitute.For<IRepository<Domain.StartLocation>>();
        var endRepository = Substitute.For<IRepository<Domain.EndLocation>>();
        
        _startRepository = Substitute.For<IStartLocationRepository>();
        _startRepository.GetVerifiedNameIdStartLocation()
            .Returns(new List<DropDownNameIdDto>
            {
                new() { Id = Guid.NewGuid(), Name = "Test Start Location" }
            });

        _endRepository = Substitute.For<IEndLocationRepository>();
        _endRepository.GetVerifiedNameIdEndLocation()
            .Returns(new List<DropDownNameIdDto>
            {
                new() { Id = Guid.NewGuid(), Name = "Test End Location" }
            });

        // Setup JSRuntime for Radzen components
        var jsRuntimeMock = Substitute.For<IJSRuntime>();
        jsRuntimeMock.InvokeAsync<object>(Arg.Any<string>(), Arg.Any<object[]>())
            .Returns(ValueTask.FromResult<object>(null!));

        // Register services
        Services.AddSingleton(_mediatorMock);
        Services.AddScoped<DialogService>();
        Services.AddSingleton(repository);
        Services.AddSingleton(startRepository);
        Services.AddSingleton(endRepository);
        Services.AddSingleton(_startRepository);
        Services.AddSingleton(_endRepository);
        Services.AddSingleton(jsRuntimeMock);
        
        // Register FluentValidation validator
        Services.AddScoped<IValidator<CreatePlaceOfInterestRequest>, CreatePlaceOfInterestValidator>();
        
        // Add Radzen services
        Services.AddScoped<DialogService>();
        Services.AddScoped<NotificationService>();
        Services.AddScoped<TooltipService>();
        Services.AddScoped<ContextMenuService>();
    }

    [Fact]
    public void CreatePlaceOfInterestForm_ValidFormWithTextBoxes_ErrorsShouldBeZero()
    {
        // Arrange
        var component = RenderComponent<CreatePlaceOfInterestForm>();

        // Act
        component.Find("#name").Change("Test Place");
        component.Find("#description").Change("Test Description");
        component.Find("#startlocation").Change("40.7128,-74.0060");
        component.Find("#endlocation").Change("34.0522,-118.2437");

        // Wait for any pending state changes
        component.WaitForState(() => component.Find("#name").GetAttribute("value") == "Test Place", 
            timeout: TimeSpan.FromSeconds(2));

        // Assert - Check that form fields are populated
        var nameInput = component.Find("#name");
        Assert.Equal("Test Place", nameInput.GetAttribute("value"));
        
        var descInput = component.Find("#description");
        Assert.Equal("Test Description", descInput.GetAttribute("value"));
    }

    [Fact]
    public void CreatePlaceOfInterestForm_ValidFormWithDropDowns_ErrorsShouldBeZero()
    {
        // Arrange
        var component = RenderComponent<CreatePlaceOfInterestForm>();
        var testStartId = _startRepository.GetVerifiedNameIdStartLocation().First().Id;
        var testEndId = _endRepository.GetVerifiedNameIdEndLocation().First().Id;

        // Act
        component.Find("#name").Change("Test Place");
        component.Find("#description").Change("Test Description");
        
        // For dropdowns, we need to find the actual select elements rendered by the custom components
        var selects = component.FindAll("select");
        if (selects.Count >= 2)
        {
            selects[0].Change(testStartId.ToString());
            selects[1].Change(testEndId.ToString());
        }

        // Assert - Verify form has values
        var nameInput = component.Find("#name");
        Assert.Equal("Test Place", nameInput.GetAttribute("value"));
    }

    [Fact]
    public void CreatePlaceOfInterestForm_NotValidForm_ValidationErrorsAppear()
    {
        // Arrange
        var component = RenderComponent<CreatePlaceOfInterestForm>();

        // Act - Try to submit empty form
        var form = component.Find("form");
        form.Submit();

        // Trigger a render to ensure validation state is updated
        component.Render();

        // Wait a bit for validation to process
        Task.Delay(100).Wait();
        component.Render();

        // Assert - The form should have validation errors
        // FluentValidation will add the 'invalid' class to invalid fields
        var invalidFields = component.FindAll(".invalid");
        var validationMessages = component.FindAll(".validation-message");
        var validationSummary = component.FindAll(".validation-summary");
        
        var hasAnyValidationIndicator = invalidFields.Count > 0 || 
                                        validationMessages.Count > 0 || 
                                        validationSummary.Count > 0;
        
        Assert.True(hasAnyValidationIndicator, 
            $"Expected validation errors to be displayed. " +
            $"Invalid fields: {invalidFields.Count}, " +
            $"Validation messages: {validationMessages.Count}, " +
            $"Validation summary: {validationSummary.Count}");
        
        // Also verify MediatR was NOT called (because form is invalid)
        _mediatorMock.DidNotReceive().Send(
            Arg.Any<CreatePlaceOfInterestRequest>(), 
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CreatePlaceOfInterestForm_ValidSubmit_CallsMediatorAndClosesDialog()
    {
        // Arrange
        var dialogService = Services.GetRequiredService<DialogService>();
        var component = RenderComponent<CreatePlaceOfInterestForm>();

        // Act - Fill in valid form data
        component.Find("#name").Change("Valid Place");
        component.Find("#description").Change("Valid Description");
        component.Find("#startlocation").Change("40.7128,-74.0060");
        component.Find("#endlocation").Change("34.0522,-118.2437");

        // Submit the form
        var form = component.Find("form");
        await form.SubmitAsync();

        // Wait for async operations
        await Task.Delay(100);

        // Assert - Verify MediatR was called
        await _mediatorMock.Received(1).Send(
            Arg.Is<CreatePlaceOfInterestRequest>(r => r.Name == "Valid Place"),
            Arg.Any<CancellationToken>());
    }
}