using System;

namespace PacienteAsistente.Web.Service.Service.Utils
{
    public static class DateHelper
    {
        public static DateTime DateMexico(DateTime fechaHora)
        {
            var dt = new DateTime(fechaHora.Year, fechaHora.Month, fechaHora.Day, fechaHora.Hour, fechaHora.Minute, fechaHora.Second).ToUniversalTime();
            var nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return TimeZoneInfo.ConvertTime(dt, TimeZoneInfo.Utc, nzTimeZone);
        }
    }
}
