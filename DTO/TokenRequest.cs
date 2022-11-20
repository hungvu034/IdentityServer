using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsApi.DTO
{
    public record TokenRequest([RequiredAttribute]string UserName , [Required]string Password);
}