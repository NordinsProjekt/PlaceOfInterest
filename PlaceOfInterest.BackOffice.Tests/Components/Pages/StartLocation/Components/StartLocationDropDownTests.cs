using Bunit;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Application.Models;
using PlaceOfInterest.BackOffice.Components.Pages.StartLocationPage.Components;

namespace PlaceOfInterest.BackOffice.Tests.Components.Pages.StartLocation.Components;

public class StartLocationDropDownTests : TestContext
{
    public StartLocationDropDownTests()
    {
        var repository = Substitute.For<IStartLocationRepository>();
        repository.GetVerifiedNameIdStartLocation().ReturnsForAnyArgs(new List<DropDownNameIdDto>
            { new() { Id = Guid.NewGuid(), Name = "TestName" } });

        Services.AddSingleton(repository);
    }

    [Fact]
    public void RenderComponent_DataListHasObjects_ShouldBeRendered()
    {
        var component = RenderComponent<StartLocationDropDown>();

        var dataListItems = component.FindAll("option");

        Assert.NotEmpty(dataListItems);
        Assert.Contains(dataListItems, item => item.TextContent == "TestName");
    }

    [Fact]
    public void RenderComponent_DataListIsEmpty_ShouldNotBeRendered()
    {
        var repository = Substitute.For<IStartLocationRepository>();
        repository.GetVerifiedNameIdStartLocation().Returns(new List<DropDownNameIdDto>());
        Services.AddSingleton(repository);

        var component = RenderComponent<StartLocationDropDown>();

        Assert.Throws<ElementNotFoundException>(() => component.Find("select"));
    }
}