using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Data;
using User.Models;

namespace User.Controllers
{
    
    [Route("/api/companies")]
    [ApiController]
    public class CompanyController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<CompanyMoldels>>> Get([FromServices] DataContext contex)
        {
            var comapanies = await contex.Companies.ToListAsync();
            return Ok(comapanies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CompanyMoldels>> GetById([FromServices] DataContext context, int id)
        {
            var company = await context.Companies.AsNoTracking()
                .FirstAsync(x => x.Id == id);
            return company;
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<CompanyMoldels>> Put(
            [FromServices] DataContext context,
            [FromBody] CompanyMoldels model)
        {
            context.Companies.Update(model);
            await context.SaveChangesAsync();
            return Ok(model);
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<CompanyMoldels>> Post(
            [FromServices] DataContext context,
            [FromBody] CompanyMoldels model)
        {
            if (ModelState.IsValid)
            {
                context.Companies.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<CompanyMoldels>> DeleteById([FromServices] DataContext context, int id)
        {
            var comapny = await context.Companies.AsNoTracking()
                .FirstAsync(x => x.Id == id);

            context.Set<CompanyMoldels>().Remove(comapny);
            await context.SaveChangesAsync();
            return Ok("Removido com sucesso!");
        }
    }

}
