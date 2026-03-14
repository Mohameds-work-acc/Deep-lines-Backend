using Deep_lines_Backend.BLL.DTOs.EmailDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface IEmailService
    {
        Task sendEmail(sendEmailDTO sendEmailDTO);
    }
}
