using Bunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PlaceOfInterest.BackOffice.Components.Pages.StartLocation.Components;
using Radzen;

namespace PlaceOfInterest.BackOffice.Tests.Components.Pages.StartLocation.Components;

public class UpdateStartLocationFormTests : TestContext
{
    private readonly IMediator _mediatorMock;

    public UpdateStartLocationFormTests()
    {
        _mediatorMock = Substitute.For<IMediator>();

        Services.AddSingleton(_mediatorMock);
        Services.AddScoped<DialogService>();
    }

    [Fact]
    public void UpdateStartLocationForm_ValidForm_ErrorsShouldBeZero()
    {
        var component = RenderComponent<CreateStartLocationForm>();

        component.Find("#location").Change("Valid Location");
        component.Find("form").Submit();

        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count == 0);
    }

    [Fact]
    public void UpdateStartLocationForm_NotValidForm_ErrorsShouldBeMoreThanZero()
    {
        var component = RenderComponent<CreateStartLocationForm>();

        component.Find("form").Submit();
        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count > 0);
    }
}
