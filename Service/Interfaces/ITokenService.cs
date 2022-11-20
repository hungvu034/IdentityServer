using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.DTO;

namespace IsApi.Service.Interfaces
{
    public interface ITokenService
    {
         TokenResponse GetToken(TokenRequest request);

    }
}