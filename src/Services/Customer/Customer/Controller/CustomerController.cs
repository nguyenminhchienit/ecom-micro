using Customer.API.Services.Interfaces;

namespace Customer.API.Controller
{
    public static class CustomerController
    {
        public static void MapCustumerApi(this WebApplication app)
        {
            app.MapGet("/", () => "Welcome to Ecommerce Micoservices");

            app.MapGet("/api/customers", async (ICustomerService customerServices)
                => await customerServices.GetAllCustomerAsync());

            app.MapGet("/api/customers/{username}", async (string username, ICustomerService customerServices) =>  {
                    var customer = await customerServices.GetCustomerByUsernameAsync(username);
                    if(customer == null)
                    {
                        return Results.NotFound();
                    }
                    return customer;
            });
        }
    }
}
