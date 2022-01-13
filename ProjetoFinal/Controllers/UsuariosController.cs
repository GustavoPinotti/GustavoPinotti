using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ProjetoFinal.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult LoginPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize]
        public IActionResult UserPage()
        {
            return View();
        }

        [HttpPost]

        


        public async Task<IActionResult> LoginPage(Usuario usuario)
        {
            ProjetoFinalContext c = new ProjetoFinalContext();
            Usuario user = new Usuario();
            user =  await c.Usuarios
            .FirstOrDefaultAsync(ob => ob.Login == usuario.Login);
            var hash = new Hash(SHA512.Create());


            try
            {
                if (ModelState.IsValid)
                {
                    if (usuario.Login == user.Login && hash.VerificarSenha(usuario.Password, user.Password))
                    {
                       
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, usuario.Login)
                            

                        };

                        var identidade = new ClaimsIdentity(claims, "Login:");

                        ClaimsPrincipal principal = new ClaimsPrincipal(identidade);
                        var regrasAutenticacao = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            ExpiresUtc = DateTime.Now.ToLocalTime().AddMinutes(59),
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            principal, regrasAutenticacao
                            );


                        return RedirectToAction("UserPage");
                    }
                    else
                    {
                        ViewBag.Erro = "Usuário ou senha invalido";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Ocorreu um problema ao autenticar" + ex.Message;
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Usuario usuario)
        {
            var hash = new Hash(SHA512.Create());
            string SenhaCripto = hash.CriptografarSenha(usuario.Password);
            usuario.Password = SenhaCripto;
            ProjetoFinalContext context = new ProjetoFinalContext();
            context.Add(usuario);
            await  context.SaveChangesAsync();

            return View();

        }
    }
}
