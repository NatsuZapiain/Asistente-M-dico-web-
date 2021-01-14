using System.Web.Mvc;
using PacienteAsistente.Data.Service.Interface;
using PacienteAsistente.Data.Service.Service;
using PacienteAsistente.Model.Enum;
using PacienteAsistente.Model.Models;
using PacienteAsistente.Utils;

namespace PacienteAsistente.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController()
        {
            _pacienteService = new PacienteService();
        }

        [AuthorizedRole(RoleEnum.Paciente)]
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Perfil", "Paciente");
        }


        [AuthorizedRole(RoleEnum.Paciente)]
        public ActionResult Perfil()
        {
            return View(_pacienteService.GetModelPerfil(User.Identity.Name));
        }

        [AuthorizedRole(RoleEnum.Paciente)]
        [HttpPost]
        public ActionResult Perfil(PacienteViewModel model)
        {
            _pacienteService.UpdateDataPerfil(User.Identity.Name, model);
            return RedirectToAction("Perfil", "Paciente");
        }
    }
}