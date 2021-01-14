using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PacienteAsistente.Data.Service.Interface;
using PacienteAsistente.Data.Service.Service;
using PacienteAsistente.Model.Enum;
using PacienteAsistente.Model.Helper;
using PacienteAsistente.Model.Models;
using PacienteAsistente.Models;

namespace PacienteAsistente.Controllers
{
    public class LoginController : Controller
    {
        private readonly IGenericLoginService _genericLoginService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IAsistenteService _asistenteService;
        private readonly IPacienteService _pacienteService;
        private readonly IPacienteDataService _pacienteDataService;

        private ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            set => _signInManager = value;
        }

        private ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            set => _userManager = value;
        }

        public LoginController()
        {
            _genericLoginService = new GenericLoginService();
            _asistenteService = new AsistenteService();
            _pacienteService = new PacienteService();
            _pacienteDataService = new PacienteDataService();
        }

        public LoginController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _genericLoginService = new GenericLoginService();
        }


        public ActionResult InicioSesion()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InicioSesion(LoginLocalViewModel model) //Login
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "El correo no esta registrado.");
                    return View(model);
                }
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Pass, model.RememberMe, true);
                switch (result)
                {
                    case SignInStatus.Success:
                        {
                            var aspNetUserId = UserManager.FindByEmail(model.Email);
                            var roles = _genericLoginService.GetRolUserByAspNetUserId(aspNetUserId.Id);
                            if (roles.Contains(RoleEnum.Asistente.GetEnumDescription()))
                            {
                                return RedirectToRoute(new
                                {
                                    controller = "Asistente",
                                    action = "Index"
                                });
                            }
                            if (roles.Contains(RoleEnum.Paciente.GetEnumDescription()))
                            {
                                return RedirectToRoute(new
                                {
                                    controller = "Paciente",
                                    action = "Index"
                                });
                            }
                            ModelState.AddModelError("", "No tienes los permisos para acceder.");
                            return View(model);
                        }
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    default:
                        ModelState.AddModelError("", "Usuario o contraseña incorrecto.");
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrecto.");
                return View(model);
            }
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(LoginLocalViewModel model)
        {
            try
            {
                if (model.Pass == model.ConfirmPass)
                {
                    if (ModelState.IsValid)
                    {
                        var code = _pacienteDataService.GetAll().FirstOrDefault(x => x.CodPaciente == model.CodePaciente);
                        if (code != null || model.Rol == "Paciente")
                        {
                            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                            var result = await UserManager.CreateAsync(user, model.Pass);
                            if (result.Succeeded)
                            {
                                if (model.Rol == "Paciente")
                                {
                                    await UserManager.AddToRoleAsync(user.Id, "Paciente");
                                    await SignInManager.SignInAsync(user, false, false);

                                    _pacienteService.RegistroPaciente(model.Email);
                                }
                                else //Asistente
                                {
                                    await UserManager.AddToRoleAsync(user.Id, "Asistente");
                                    await SignInManager.SignInAsync(user, false, false);

                                    _asistenteService.RegistroAsistente(model.Email, model.CodePaciente);
                                }

                                return RedirectToAction("InicioSesion", "Login");
                            }

                            foreach (var item in result.Errors)
                            {
                                if (item.Contains("Email"))
                                {
                                    ModelState.AddModelError("", "El correo ya esta en uso.");
                                }
                            }
                            return View(model);
                        }
                        else
                        {
                            ModelState.AddModelError("", "El codigo del paciente no existe");
                            return View(model);
                        }

                    }
                    ModelState.AddModelError("", "Error al registrar");
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Las contraseñas no coinciden");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al registrar");
                return View(model);
            }
        }
    }
}