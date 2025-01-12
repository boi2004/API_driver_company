using Driver_Company_5._0.Infrastructure.Data;
using Driver_Company_5._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Driver_Company_5._0.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
       
        [HttpGet]
        public IEnumerable<Role> Get()
        {
            using (var context = new DriverManagementContext())
            {
                //get all Roles
                return context.Roles.ToList();

                

            }
        }
    }
}