using System;
using PacienteAsistente.Data.Service.Interface;
using PacienteAsistente.Model.Models;

namespace PacienteAsistente.Data.Service.Service
{
    public class AsistenteService : IAsistenteService
    {
        private readonly IAspNetUserDataService _aspNetUserDataService;
        private readonly IAsistenteDataService _asistenteDataService;
        private readonly IPacienteDataService _pacienteDataService;

        public AsistenteService()
        {
            _aspNetUserDataService = new AspNetUserDataService();
            _asistenteDataService = new AsistenteDataService();
            _pacienteDataService = new PacienteDataService();
        }

        public bool RegistroAsistente(string userName, string code)
        {
            try
            {
                var paciente = _pacienteDataService.GetByCode(code);

                var result = new Asistente()
                {
                    Id = Guid.NewGuid(),
                    AspNetUserId = _aspNetUserDataService.GetByEmail(userName).Id,
                    PacienteId = paciente.Id
                };
                _asistenteDataService.Create(result);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public AsistenteViewModel GetModelPerfil(string userName)
        {
            try
            {
                var asp = _aspNetUserDataService.GetByEmail(userName);
                var perfil = _asistenteDataService.GetByAspNet(asp.Id);
                var model = new AsistenteViewModel()
                {
                    Nombre = perfil.NomAsistente,
                    Parentesco = perfil.Parentesco,
                    Telefono = perfil.Telefono,
                };
                return model;
            }
            catch (Exception e)
            {
                return new AsistenteViewModel();
            }
        }

        public bool UpdateDataPerfil(string userName, AsistenteViewModel model)
        {
            try
            {
                var asp = _aspNetUserDataService.GetByEmail(userName);
                var perfil = _asistenteDataService.GetByAspNet(asp.Id);

                perfil.NomAsistente = model.Nombre;
                perfil.Telefono = model.Telefono;
                perfil.Parentesco = model.Parentesco;

                _asistenteDataService.Update(perfil);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
