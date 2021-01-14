using System;
using PacienteAsistente.Data.Service.Interface;
using PacienteAsistente.Model.Models;

namespace PacienteAsistente.Data.Service.Service
{
    public class PacienteService : IPacienteService
    {
        private readonly IAspNetUserDataService _aspNetUserDataService;
        private readonly IPacienteDataService _pacienteDataService;

        public PacienteService()
        {
            _aspNetUserDataService = new AspNetUserDataService();
            _pacienteDataService = new PacienteDataService();
        }

        public bool RegistroPaciente(string userName)
        {
            try
            {
                var result = new Paciente()
                {
                    Id = Guid.NewGuid(),
                    AspNetUserId = _aspNetUserDataService.GetByEmail(userName).Id,
                    CodPaciente = GenerateCode(5)
                };
                _pacienteDataService.Create(result);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public PacienteViewModel GetModelPerfil(string userName)
        {
            try
            {
                var asp = _aspNetUserDataService.GetByEmail(userName);
                var perfil = _pacienteDataService.GetByAspNet(asp.Id);
                var model = new PacienteViewModel()
                {
                    NombreUsuario = perfil.NombreUsuario,
                    Genero = perfil.Genero,
                    Edad = perfil.Edad,
                    TipoSangre = perfil.TipoSangre,
                    Alergias = perfil.Alergias,
                    Padecimiento = perfil.Padecimiento,
                    NumEmergencia = perfil.NumEmergencia
                };
                return model;
            }
            catch (Exception e)
            {
                return new PacienteViewModel();
            }
        }

        public bool UpdateDataPerfil(string userName, PacienteViewModel model)
        {
            try
            {
                var asp = _aspNetUserDataService.GetByEmail(userName);
                var perfil = _pacienteDataService.GetByAspNet(asp.Id);

                perfil.NombreUsuario = model.NombreUsuario;
                perfil.Genero = model.Genero;
                perfil.Edad = model.Edad;
                perfil.TipoSangre = model.TipoSangre;
                perfil.Alergias = model.Alergias;
                perfil.Padecimiento = model.Padecimiento;
                perfil.NumEmergencia = model.NumEmergencia;

                _pacienteDataService.Update(perfil);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private string GenerateCode(int number)
        {
            var result = "";
            var ran = new Random();
            var mayus = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var minus = "abcdefghijklmnopqrstuvwxyz";
            var nums = "0123456789";
            for (var i = 0; i < number; i++)
            {
                switch (ran.Next(0, 3))
                {
                    case 0:
                        result += mayus.Substring(ran.Next(0, mayus.ToCharArray().Length - 1), 1);
                        break;
                    case 1:
                        result += minus.Substring(ran.Next(0, minus.ToCharArray().Length - 1), 1);
                        break;
                    case 2:
                        result += nums.Substring(ran.Next(0, nums.ToCharArray().Length - 1), 1);
                        break;
                }
            }

            return result;
        }
    }
}
