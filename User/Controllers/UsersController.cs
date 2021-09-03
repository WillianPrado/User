using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Data;
using User.Models;
using User.Services;

namespace User.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] UserModels model)
        {
            var user = await context.Users.AsNoTracking()
                .FirstAsync(x => x.Name == model.Name && x.Password == model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<UserModels>>> Get([FromServices] DataContext context)
        {
            var users = await context.Users.ToListAsync();
            return users;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<UserModels>> GetById([FromServices] DataContext context, int id)
        {
            var user = await context.Users.AsNoTracking()
                .FirstAsync(x => x.Id == id);
            return user;
        }

        [HttpPut]
        [Route("")]
        [Authorize(Roles = "adimin")]
        


        public async Task<ActionResult<UserModels>> Put(
            [FromServices] DataContext context,
            [FromBody] UserModels model)        {
           
            context.Users.Update(model);
            await context.SaveChangesAsync();
            return model;
        }
        


        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModels>> Post(
            [FromServices] DataContext context,
            [FromBody] UserModels model)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModels>> DeleteById([FromServices] DataContext context, int id)
        {
            var user = await context.Users.AsNoTracking()
                .FirstAsync(x => x.Id == id);
            context.Set<UserModels>().Remove(user);
            await context.SaveChangesAsync();
            return Ok("Removido com sucesso!");
        }

    }
}
