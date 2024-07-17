using Microsoft.AspNetCore.Builder;
using Renting;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<RentingWebTestModule>();

public partial class Program
{
}
