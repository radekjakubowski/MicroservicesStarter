using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SignIn;

public record SignInCommandResponse(string BearerToken, string RefreshToken, DateTime RefreshTokenValidTo);
