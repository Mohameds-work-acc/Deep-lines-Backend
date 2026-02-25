using Deep_lines_Backend.BLL.DTOs.AuthServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface IAuthService
    {
        public bool Authenticate(LoginDTO loginDTO);
    }
}
