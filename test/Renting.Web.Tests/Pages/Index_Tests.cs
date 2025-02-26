﻿using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Renting.Pages;

public class Index_Tests : RentingWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
