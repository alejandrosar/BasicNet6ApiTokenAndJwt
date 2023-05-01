

namespace BasicNet6Template.Services.User{

    public interface IUserService
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);
    }

    public class UserService : IUserService
    {
        // Aquí puedes agregar la lógica de validación de usuarios y contraseñas según tus necesidades.
        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            // Simulamos una validación básica de las credenciales
            return await Task.FromResult(username == "test" && password == "1234");
        }
    }
}