using System;
using System.Linq;
using PacienteAsistente.Data.Service.Interface;
using PacienteAsistente.Model.Models;
using PacienteAsistente.Web.Service.Service.Utils;

namespace PacienteAsistente.Data.Service.Service
{
    public class PacienteService : IPacienteService
    {
        private readonly IAspNetUserDataService _aspNetUserDataService;
        private readonly IPacienteDataService _pacienteDataService;
        private readonly ITratamientoDataService _tratamientoDataService;
        private readonly IAsistenteDataService _asistenteDataService;

        public PacienteService()
        {
            _aspNetUserDataService = new AspNetUserDataService();
            _pacienteDataService = new PacienteDataService();
            _tratamientoDataService = new TratamientoDataSevice();
            _asistenteDataService = new AsistenteDataService();
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

        public IndexViewModel IndexPaciente(string userName)
        {
            try
            {
                var asp = _aspNetUserDataService.GetByEmail(userName);
                var user = _pacienteDataService.GetByAspNet(asp.Id);
                var model = new IndexViewModel
                {
                    Codigo = user.CodPaciente,
                    NombrePaciente = user.NombreUsuario
                };
                return model;
            }
            catch (Exception e)
            {
                return new IndexViewModel();
            }
        }

        public listaAplicacionViewModel GetListaAplicacionViewModel(string userName)
        {
            try
            {
                var asp = _aspNetUserDataService.GetByEmail(userName);
                var asis = _pacienteDataService.GetByAspNet(asp.Id);
                var hactual = DateHelper.DateMexico(DateTime.Now);
                var result = new listaAplicacionViewModel
                {
                    ListaAplicacion = _tratamientoDataService.GetAll().Where(x => x.IdUsuario == asis.Id && x.FechaAplicacion == hactual.Date).Select(x => new TratamientoViewModel
                    {
                        Id = x.Id.ToString(),
                        NomMedicamento = x.NomMedicamento,
                        TipoMedicamento = x.TipoMedicamento,
                        FechaAplicacion = x.FechaAplicacion,
                        Aplicador = x.Asistente.NomAsistente,
                        Contador = x.Aplicador.ToString(),
                        HoraAplicacion = x.HoraAplicacion,
                    }).ToList()
                };
                return result;
            }
            catch (Exception e)
            {
                return new listaAplicacionViewModel();
            }
        }

        public ListaAsistentesViewModel getListaAsistentes(string userName)
        {
            try
            {
                var asp = _aspNetUserDataService.GetByEmail(userName);
                var asis = _pacienteDataService.GetByAspNet(asp.Id);
                var model = new ListaAsistentesViewModel
                {
                    ListaAsistentes = _asistenteDataService.GetAll().Where(x => x.PacienteId == asis.Id).Select(
                        x => new AsistenteViewModel()
                        {
                            Nombre = x.NomAsistente,
                            Parentesco = x.Parentesco,
                            Telefono = x.Telefono
                        }).ToList()
                };

                return model;
            }
            catch (Exception e)
            {
                return new ListaAsistentesViewModel();
            }
        }

        public listaAplicacionViewModel GetHistorialAplicacion(string userName)
        {
            try
            {
                var asp = _aspNetUserDataService.GetByEmail(userName);
                var paciente = _pacienteDataService.GetByAspNet(asp.Id);
                var model = new listaAplicacionViewModel()
                {
                    ListaAplicacion = _tratamientoDataService.GetAll().OrderBy(x => x.FechaAplicacion).Where(x => x.IdUsuario == paciente.Id).Select(
                        x =>
                            new TratamientoViewModel
                            {
                                NomMedicamento = x.NomMedicamento,
                                FechaAplicacion = x.FechaAplicacion,
                                TipoMedicamento = x.TipoMedicamento,
                                Contador = x.Aplicador.ToString(),
                                Aplicador = x.Asistente.NomAsistente,
                                HoraAplicacion = x.HoraAplicacion,
                                
                            }).ToList()
                };
                return model;
            }
            catch (Exception e)
            {
                return new listaAplicacionViewModel();
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
