using Bunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PlaceOfInterest.BackOffice.Components.Pages.StartLocation.Components;
using Radzen;

namespace PlaceOfInterest.BackOffice.Tests.Components.Pages.StartLocation.Components;

public class CreateStartLocationFormTests : TestContext
{
    public CreateStartLocationFormTests()
    {
        var mediatorMock = Substitute.For<IMediator>();

        Services.AddSingleton(mediatorMock);
        Services.AddScoped<DialogService>();
    }

    [Fact]
    public void CreateStartLocationForm_ValidForm_ErrorsShouldBeZero()
    {
        var component = RenderComponent<CreateStartLocationForm>();

        component.Find("#location").Change("Valid Location");
        component.Find("form").Submit();

        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count == 0);
    }

    [Fact]
    public void CreateStartLocationForm_NotValidForm_ErrorsShouldBeMoreThanZero()
    {
        var component = RenderComponent<CreateStartLocationForm>();

        component.Find("form").Submit();
        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count > 0);
    }
}