using Bunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.BackOffice.Components.Pages.StartLocationPage.Components;
using Radzen;

namespace PlaceOfInterest.BackOffice.Tests.Components.Pages.StartLocation.Components;

public class UpdateStartLocationFormTests : TestContext
{
    public UpdateStartLocationFormTests()
    {
        var mediatorMock = Substitute.For<IMediator>();
        var repositoryMock = Substitute.For<IRepository<Domain.StartLocation>>();
        repositoryMock.GetByIdAsync(Guid.Empty).ReturnsForAnyArgs(new Domain.StartLocation { Id = Guid.NewGuid() });

        Services.AddSingleton(mediatorMock);
        Services.AddSingleton(repositoryMock);
        Services.AddScoped<DialogService>();
    }

    [Fact]
    public void UpdateStartLocationForm_ValidForm_ErrorsShouldBeZero()
    {
        var component = RenderComponent<UpdateStartLocationForm>();

        component.Find("#location").Change("Valid Location");
        component.Find("form").Submit();

        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count == 0);
    }

    [Fact]
    public void UpdateStartLocationForm_NotValidForm_ErrorsShouldBeMoreThanZero()
    {
        var component = RenderComponent<UpdateStartLocationForm>();

        component.Find("form").Submit();
        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count == 1);
    }
}
