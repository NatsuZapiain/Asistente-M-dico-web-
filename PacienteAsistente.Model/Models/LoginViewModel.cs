namespace PacienteAsistente.Model.Models
{
    public class LoginLocalViewModel
    {
        public string Email { get; set; }
        public string Pass { get; set; }
        public string ConfirmPass { get; set; }
        public string Rol { get; set; }
        public string CodePaciente { get; set; }
        public bool RememberMe { get; set; }
    }
}
