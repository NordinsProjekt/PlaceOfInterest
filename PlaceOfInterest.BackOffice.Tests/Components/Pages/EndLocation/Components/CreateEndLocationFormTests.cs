using Bunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PlaceOfInterest.BackOffice.Components.Pages.EndLocationPage.Components;
using Radzen;

namespace PlaceOfInterest.BackOffice.Tests.Components.Pages.EndLocation.Components;

public class CreateEndLocationFormTests : TestContext
{
    public CreateEndLocationFormTests()
    {
        var mediatorMock = Substitute.For<IMediator>();

        Services.AddSingleton(mediatorMock);
        Services.AddScoped<DialogService>();
    }

    [Fact]
    public void CreateEndLocationForm_ValidForm_ErrorsShouldBeZero()
    {
        var component = RenderComponent<CreateEndLocationForm>();

        component.Find("#location").Change("Valid Location");
        component.Find("form").Submit();

        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count == 0);
    }

    [Fact]
    public void CreateEndLocationForm_NotValidForm_ErrorsShouldBeMoreThanZero()
    {
        var component = RenderComponent<CreateEndLocationForm>();

        component.Find("form").Submit();
        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count > 0);
    }
}
