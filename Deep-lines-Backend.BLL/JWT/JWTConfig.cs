using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.JWT
{
    public class JWTConfig
    {

        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInMinutes { get; set; }

    }
}
