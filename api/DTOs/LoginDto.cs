using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs;

public record LoginDto
(
    string Email,
    string Password
);
