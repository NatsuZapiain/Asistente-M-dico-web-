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
            return View(_pacienteService.IndexPaciente(User.Identity.Name));
        }

        [AuthorizedRole(RoleEnum.Paciente)]
        public ActionResult ListaAsistentes()
        {
            return View(_pacienteService.getListaAsistentes(User.Identity.Name));
        }

        [AuthorizedRole(RoleEnum.Paciente)]
        public ActionResult ListaAplicacion()
        {
            return View(_pacienteService.GetListaAplicacionViewModel(User.Identity.Name));
        }

        [AuthorizedRole(RoleEnum.Paciente)]
        public ActionResult HistorialAplicacion()
        {
            return View(_pacienteService.GetHistorialAplicacion(User.Identity.Name));
        }

        #region perfil

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

        #endregion
    }
}