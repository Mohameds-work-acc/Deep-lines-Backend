using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.EmailDTOs
{
    public class EmailSettings
    {

            public string Email { get; set; }
            public string Password { get; set; }
            public string Host { get; set; }
            public int Port { get; set; }
        
    }
}
