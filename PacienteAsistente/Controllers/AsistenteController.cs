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
            return View();
        }

        #region perfil

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

        #endregion

        [AuthorizedRole(RoleEnum.Asistente)]
        public ActionResult RegistroMedicmento()
        {
            return View();
        }

        [AuthorizedRole(RoleEnum.Asistente)]
        [HttpPost]
        public ActionResult RegistroMedicmento(TratamientoViewModel model)
        {
            _asistenteService.RegistroMedicamentoPaciente(User.Identity.Name, model);
            return RedirectToAction("Index", "Asistente");
        }

        #region lista asistentes

        [AuthorizedRole(RoleEnum.Asistente)]
        public ActionResult ListaAsistentes()
        {
            return View(_asistenteService.getListaAsistentes(User.Identity.Name));
        }

        #endregion

        [AuthorizedRole(RoleEnum.Asistente)]
        public ActionResult RegistroAplicacion()
        {
            return View(_asistenteService.GetListaAplicacionViewModel(User.Identity.Name));
        }

        [AuthorizedRole(RoleEnum.Asistente)]
        [HttpPost]
        public ActionResult RegistroAplicacion(listaAplicacionViewModel model)
        {
            _asistenteService.RegistroAplicacion(User.Identity.Name, model);
            return RedirectToAction("Index", "Asistente");
        }

        #region Datos paciente
        [AuthorizedRole(RoleEnum.Asistente)]
        public ActionResult DatosPaciente()
        {
            return View(_asistenteService.GetModelPerfilPaciente(User.Identity.Name));
        }

        #endregion
    }
}