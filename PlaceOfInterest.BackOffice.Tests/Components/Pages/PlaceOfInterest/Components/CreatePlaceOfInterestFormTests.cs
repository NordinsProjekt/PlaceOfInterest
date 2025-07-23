using Bunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using NSubstitute;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Application.Models;
using PlaceOfInterest.BackOffice.Components.Pages.PlaceOfInterestPage.Component;
using Radzen;

namespace PlaceOfInterest.BackOffice.Tests.Components.Pages.PlaceOfInterest.Components;

public class CreatePlaceOfInterestFormTests : TestContext
{
    public CreatePlaceOfInterestFormTests()
    {
        var mediatorMock = Substitute.For<IMediator>();
        var repository = Substitute.For<IRepository<Domain.PlaceOfInterest>>();
        var startRepository = Substitute.For<IRepository<Domain.StartLocation>>();
        var endRepository = Substitute.For<IRepository<Domain.EndLocation>>();
        var startRepository2 = Substitute.For<IStartLocationRepository>();

        startRepository2.GetVerifiedNameIdStartLocation()
            .ReturnsForAnyArgs(new List<DropDownNameIdDto>
                { new() { Id = Guid.NewGuid(), Name = "TestValue" } });

        var endRepository2 = Substitute.For<IEndLocationRepository>();

        endRepository2.GetVerifiedNameIdEndLocation().ReturnsForAnyArgs(
            new List<DropDownNameIdDto>
                { new() { Id = Guid.NewGuid(), Name = "TestValue" } });

        var jsRuntimeMock = Substitute.For<IJSRuntime>();

        Services.AddSingleton(mediatorMock);
        Services.AddScoped<DialogService>();
        Services.AddSingleton(repository);
        Services.AddSingleton(startRepository);
        Services.AddSingleton(endRepository);
        Services.AddSingleton(startRepository2);
        Services.AddSingleton(endRepository2);
        Services.AddSingleton(jsRuntimeMock);
    }

    [Fact]
    public void CreatePlaceOfInterestForm_ValidFormWithTextBoxes_ErrorsShouldBeZero()
    {
        var component = RenderComponent<CreatePlaceOfInterestForm>();

        component.Find("#name").Change("Test Place");
        component.Find("#startlocation").Change("StartLoc");
        component.Find("#endlocation").Change("EndLoc");

        component.Find("form").Submit();
        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count == 0);
    }

    [Fact]
    public void CreatePlaceOfInterestForm_ValidFormWithDropDowns_ErrorsShouldBeZero()
    {
        var component = RenderComponent<CreatePlaceOfInterestForm>();

        component.Find("#name").Change("Test Place");
        var dropdowns = component.FindAll("select");
        dropdowns[0].Change(Guid.NewGuid());
        dropdowns[1].Change(Guid.NewGuid());

        component.Find("form").Submit();
        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count == 0);
    }

    [Fact]
    public void CreatePlaceOfInterestForm_NotValidForm_ErrorsShouldBeMoreThanZero()
    {
        var component = RenderComponent<CreatePlaceOfInterestForm>();

        component.Find("form").Submit();
        component.GetChangesSinceFirstRender();

        var errors = component.FindAll(".validation-message");
        Assert.True(errors.Count > 0);
    }
}