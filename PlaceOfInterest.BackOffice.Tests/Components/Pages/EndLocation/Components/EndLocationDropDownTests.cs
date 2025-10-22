using Bunit;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PlaceOfInterest.Application.Interfaces;
using PlaceOfInterest.Application.Models;
using PlaceOfInterest.BackOffice.Components.Pages.EndLocationPage.Components;

namespace PlaceOfInterest.BackOffice.Tests.Components.Pages.EndLocation.Components;

public class StartLocationDropDownTests : TestContext
{
    public StartLocationDropDownTests()
    {
        var repository = Substitute.For<IEndLocationRepository>();
        repository.GetVerifiedNameIdEndLocation().ReturnsForAnyArgs(new List<DropDownNameIdDto>
            { new() { Id = Guid.NewGuid(), Name = "TestName" } });

        Services.AddSingleton(repository);
    }

    [Fact]
    public void RenderComponent_DataListHasObjects_ShouldBeRendered()
    {
        var component = RenderComponent<EndLocationDropDown>();

        var dataListItems = component.FindAll("option");

        Assert.NotEmpty(dataListItems);
        Assert.Contains(dataListItems, item => item.TextContent == "TestName");
    }

    [Fact]
    public void RenderComponent_DataListIsEmpty_ShouldNotBeRendered()
    {
        var repository = Substitute.For<IEndLocationRepository>();
        repository.GetVerifiedNameIdEndLocation().Returns(new List<DropDownNameIdDto>());
        Services.AddSingleton(repository);

        var component = RenderComponent<EndLocationDropDown>();

        Assert.Throws<ElementNotFoundException>(() => component.Find("select"));
    }
}