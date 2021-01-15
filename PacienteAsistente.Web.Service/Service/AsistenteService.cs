using System;
using System.Collections.Generic;
using System.Linq;
using PacienteAsistente.Data.Service.Interface;
using PacienteAsistente.Model.Models;
using PacienteAsistente.Web.Service.Service.Utils;

namespace PacienteAsistente.Data.Service.Service
{
    public class AsistenteService : IAsistenteService
    {
        private readonly IAspNetUserDataService _aspNetUserDataService;
        private readonly IAsistenteDataService _asistenteDataService;
        private readonly IPacienteDataService _pacienteDataService;
        private readonly ITratamientoDataService _tratamientoDataService;

        public AsistenteService()
        {
            _aspNetUserDataService = new AspNetUserDataService();
            _asistenteDataService = new AsistenteDataService();
            _pacienteDataService = new PacienteDataService();
            _tratamientoDataService = new TratamientoDataSevice();
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

        public ListaAsistentesViewModel getListaAsistentes(string userName)
        {
            try
            {
                var asp = _aspNetUserDataService.GetByEmail(userName);
                var asis = _asistenteDataService.GetByAspNet(asp.Id);
                var model = new ListaAsistentesViewModel
                {
                    ListaAsistentes = _asistenteDataService.GetAll().Where(x => x.PacienteId == asis.PacienteId).Select(
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

        public bool RegistroMedicamentoPaciente(string userName, TratamientoViewModel model)
        {
            try
            {
                var asp = _aspNetUserDataService.GetByEmail(userName);
                var user = _asistenteDataService.GetByAspNet(asp.Id);
                var listaRegistros = new List<Tratamiento>();
                var fecha = model.FechaInicio;
                var hora = model.HoraAplicacion;
                var fechaActual = DateHelper.DateMexico(DateTime.Now);

                for (int i = 0; i < Int32.Parse(model.Contador); i++)
                {
                    for (int j = 0; j < Int32.Parse(model.DosisDia); j++)
                    {
                        listaRegistros.Add(new Tratamiento
                        {
                            Id = Guid.NewGuid(),
                            IdRegAsistente = user.Id,
                            IdUsuario = user.PacienteId,
                            NomMedicamento = model.NomMedicamento,
                            TipoMedicamento = model.TipoMedicamento,
                            DosisDia = "1",
                            FechaAplicacion = fecha,
                            HoraAplicacion = fechaActual.TimeOfDay
                        });
                    }

                    fecha = fecha.AddDays(Int32.Parse(model.DosisVariable));
                }

                _tratamientoDataService.CreateAll(listaRegistros);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public PacienteViewModel GetModelPerfilPaciente(string userName)
        {
            try
            {
                var asp = _aspNetUserDataService.GetByEmail(userName);
                var asis = _asistenteDataService.GetByAspNet(asp.Id);

                var perfil = _pacienteDataService.GetById(asis.PacienteId);
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
    }
}
