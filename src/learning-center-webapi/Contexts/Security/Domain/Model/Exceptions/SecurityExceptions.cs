namespace learning_center_webapi.Contexts.Security.Domain.Model.Exceptions;

public class SecurityExceptions
{
    
    public class LoginException : Exception
    {
        public LoginException() : base("Invalid username or password.")
        {
        }
        
    }
}