using System.Web.Mvc;
using PacienteAsistente.Data;
using PacienteAsistente.Data.Service.Interface;
using PacienteAsistente.Data.Service.Service;
using PacienteAsistente.Model.Enum;
using PacienteAsistente.Model.Models;
using PacienteAsistente.Utils;

namespace PacienteAsistente.Controllers
{
    public class AsistenteController : Controller
    {
        private readonly IAsistenteService _asistenteService;
        
        public AsistenteController()
        {
            _asistenteService = new AsistenteService();
        }


        [AuthorizedRole(RoleEnum.Asistente)]
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Perfil", "Asistente");
        }


        [AuthorizedRole(RoleEnum.Asistente)]
        public ActionResult Perfil()
        {
            return View(_asistenteService.GetModelPerfil(User.Identity.Name));
        }

        [AuthorizedRole(RoleEnum.Asistente)]
        [HttpPost]
        public ActionResult Perfil(AsistenteViewModel model)
        {
            _asistenteService.UpdateDataPerfil(User.Identity.Name, model);
            return RedirectToAction("Perfil", "Asistente");
        }
    }
}