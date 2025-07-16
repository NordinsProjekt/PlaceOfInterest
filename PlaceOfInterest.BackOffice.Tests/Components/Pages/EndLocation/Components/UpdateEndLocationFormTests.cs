using Bunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.BackOffice.Components.Pages.EndLocationPage.Components;
using Radzen;

namespace PlaceOfInterest.BackOffice.Tests.Components.Pages.EndLocation.Components;

public class UpdateEndLocationFormTests : TestContext
{
    public UpdateEndLocationFormTests()
    {
        var mediatorMock = Substitute.For<IMediator>();
        var repositoryMock = Substitute.For<IRepository<Domain.EndLocation>>();
        repositoryMock.GetByIdAsync(Guid.Empty).ReturnsForAnyArgs(new Domain.EndLocation { Id = Guid.NewGuid() });

        Services.AddSingleton(mediatorMock);
        Services.AddSingleton(repositoryMock);
        Services.AddScoped<DialogService>();
    }

    [Fact]
    public void UpdateStartLocationForm_ValidForm_ErrorsShouldBeZero()
    {
        var component = RenderComponent<UpdateEndLocationForm>();

        component.Find("#location").Change("Valid Location");
        component.Find("form").Submit();

        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count == 0);
    }

    [Fact]
    public void UpdateStartLocationForm_NotValidForm_ErrorsShouldBeMoreThanZero()
    {
        var component = RenderComponent<UpdateEndLocationForm>();

        component.Find("form").Submit();
        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count == 1);
    }
}