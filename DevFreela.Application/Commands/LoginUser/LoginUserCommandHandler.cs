using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;

        }
        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Criar um hash para a senha inserida
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            // Verificar no banco de dados se há um usuário com o mesmo login e senha (hash)
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);
            // Se não houver, erro no login
            if (user == null)
            {
                return null;
            }

            // Se houver, gera o token com os dados do usuário
            var token = _authService.GenerateJwtToken(user.Email,user.Role);

            return new LoginUserViewModel(user.Email, token);
        }
    }
}
