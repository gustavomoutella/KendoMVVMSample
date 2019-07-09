using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.User;
using Vsol.Api.AppSecurity.Domain.Entities;
using Vsol.Api.AppSecurity.Domain.Repositories;
using Vsol.Api.Shared.Domain;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Vsol.Api.AppSecurity.Domain.Commands.Handlers
{
    public class UserCommandHandler
    {
        private readonly IUserRepository _userRepository;
        private const string passwordSalt = "149A8727-19E1-4014-9E94-C5C7E82A60A9 D4443F99-2C4C-486A-BC6A-0A5614D7A92D 9EE0BE7E-995E-4ADA-8171-70B204333CC3";

        public UserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private static string GetHashedPassword(string pass)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: pass,
                        salt: System.Text.Encoding.ASCII.GetBytes(passwordSalt),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));
        }

        public async Task<NotificationResult> CheckUserAsync(string username, Guid? idUser, string password)
        {
            var result = new NotificationResult();

            // Passo 1 - Recuperando o usuário pelo username
            var user = await _userRepository.GetByUserName(username);

            // Passo 2 - Verificando a existência do usuário
            if (user != null)
            {
                // Passo 3 - Verificando se o usuário está ativo e não bloqueado
                if (user.Enabled && !user.Blocked)
                {
                    // Passo 3.1 - Verificando e-mail confirmado
                    if (user.EmailConfirmed)
                    {
                        bool isValid = false;

                        // Passo 3.2 - Identificando se a verificação ocorrerá pelo Guid ou pela senha
                        if (idUser.HasValue)
                        {
                            // Passo 3.2.1 - Verificando o identificador
                            if (user.IdUser == idUser.Value)
                                isValid = true;
                        }
                        else
                        {
                            // Passo 3.2.2 - Verificando a senha
                            string encrypted = GetHashedPassword(password);

                            if (user.Password == encrypted)
                                isValid = true;
                        }

                        // Passo 3.3 - Validando o login
                        if (isValid)
                        {
                            user.ValidLogon();
                            result.Data = user;
                        }
                        else
                        {
                            user.InvalidLogon();
                            result.AddMessage("Usuário e / ou senha inválidos.");
                        }
                    }
                    else
                        result.AddMessage("Aguardando pela confirmação de e-mail.");
                }
                else
                    result.AddMessage("Usuário inativo ou bloqueado.");

            }
            else
                result.AddMessage("Usuário e / ou senha inválidos.");

            return result;
        }
    }
}