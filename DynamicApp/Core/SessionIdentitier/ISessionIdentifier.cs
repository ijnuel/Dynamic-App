using Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SessionIdentitier
{
    public interface ISessionIdentifier
    {
        UserDto? GetCurrentUser();
        void GetUserDetails();
    }
}
