using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Helpers
{
    public class TokenHelper
    {
        public static SymmetricSecurityKey GetSecurityKey()
        {
            var securityKey = "abcabcdsakhakldm,asfkadasnjdkas";

            var symmetricSecurtyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            return symmetricSecurtyKey;
        }
    }
}
