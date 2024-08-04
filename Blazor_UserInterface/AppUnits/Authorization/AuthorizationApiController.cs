

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
 


[ApiController]    
[Route("api/auth/[action]")]
[Route("api/[controller]/[action]")]
public class AuthorizationApiController : ControllerBase
{
    private APIAuthorization _authorization;
    private ILogger<AuthorizationApiController> _logger;

    public AuthorizationApiController(
        ILogger<AuthorizationApiController> logger,
        APIAuthorization authorization)
    {
        _authorization = authorization;
        _logger = logger;
    }

    [HttpGet()]
    public object IsSignin()
    {
        return new
        {
            IsSignin = _authorization.IsSignin()
        };
    }

    [HttpGet()]
    public object Signin(string Email, string Password)
    {
        try
        {
            _authorization.Signin(Email, Password, true);
        }
        catch (Exception ex)
        {
            return new
            {
                status = "failed",
                message = ex.Message
            };
        }
        return new
        {
            status = "success"
        };
    }

    [HttpGet()]
    public object Signout()
    {
        try
        {
            _authorization.Signout(true);
        }
        catch (Exception ex)
        {
            return new
            {
                status = "failed",
                message = ex.Message
            };
        }
        return new
        {
            status = "success"
        };
    }


    [HttpGet()]
    public object Signup(string Email, string Password, string Confirmation, string SurName, string FirstName, string LastName, string Birthday, string Tel)
    {
        try
        {
            _authorization.Signup(Email, Password, Confirmation, SurName, FirstName, LastName, DateTime.Parse(Birthday), Tel);
        }
        catch (Exception ex)
        {
            string message = "";
            do
            {
                message += ex.Message + ". ";
                ex = ex.InnerException;
            }
            while (ex.InnerException != null);
            return new
            {
                status = "failed",
                message = message
            };
        }
        return new
        {
            status = "success"
        };
    }

    [HttpGet()]
    public UserContext Verify()
    {
        return _authorization.Verify();
    }

    // 
    [HttpGet()]
    public object HasUserWithEmail(string Email)
    {
        bool has = _authorization.HasUserWithEmail(Email);
        if (has)
        {
            return new
            {
                uniq = "User with this email already registered"
            };
        }
        else
        {
            return new { };
        }
        
    }

    [HttpGet()]
    public object HasUserWithTel(string Tel)
    {
        bool has = _authorization.HasUserWithTel(Tel);
        if (has)
        {
            return new
            {
                uniq = "Use with this tel number already registered"
            };
        }
        else
        {
            return new { };
        } 
    }
}



