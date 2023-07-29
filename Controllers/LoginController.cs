using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers{
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpGet("/Login")]
        public IActionResult Get([FromServices] AppDbContext context)
        {
            try {
                return Ok(context.Login.ToList());
            } catch {
                return BadRequest("nao foi possível listar");
            }
        }

        [HttpPost("/CreateAccount")]
        public IActionResult Post(
        [FromBody] LoginModel model,
        [FromServices] AppDbContext context)
        {
            try {
                context.Login.Add(model);
                context.SaveChanges();
                return Ok();
            } catch {
                return BadRequest("nao foi possível criar conta");
            }
        }
        
        [HttpPost("/Login")]
        public IActionResult Login(
        [FromBody] LoginModel model,
        [FromServices] AppDbContext context)
        {
            try {
                var user = context.Login.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);

                if (user == null) {
                    return BadRequest("usuario ou senha incorretos");
                }
                return Ok("logado");
            } catch {
                return BadRequest("nao foi possível logar");
            }
        }

    }

}