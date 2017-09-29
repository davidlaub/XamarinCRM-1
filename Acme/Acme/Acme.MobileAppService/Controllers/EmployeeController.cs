using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Acme.MobileAppService.DataObjects;
using Acme.MobileAppService.Models;
using Microsoft.Azure.Mobile.Server;

namespace Acme.MobileAppService.Controllers
{
    // [Authorize]
    public class EmployeeController : TableController<Employee>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MasterDetailContext context = new MasterDetailContext();
            DomainManager = new EntityDomainManager<Employee>(context, Request);
        }

        // GET tables/Employee
        public IQueryable<Employee> GetAllEmployees()
        {
            return Query();
        }

        // GET tables/Employee/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Employee> GetEmployee(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Employee/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Employee> PatchEmployee(string id, Delta<Employee> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Employee
        public async Task<IHttpActionResult> PostEmployee(Employee employee)
        {
            Employee current = await InsertAsync(employee);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Employee/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEmployee(string id)
        {
            return DeleteAsync(id);
        }
    }
}
