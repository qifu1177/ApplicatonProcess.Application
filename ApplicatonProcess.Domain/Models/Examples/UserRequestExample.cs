using ApplicatonProcess.Domain.Interfaces;


namespace ApplicatonProcess.Domain.Models.Examples
{
    public class UserRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new UserRequest { Email = "th@gmail.com", Address = "kriemhild Str. 10, 93050", Age = 20, FirstName = "Hans", LastName = "Henne" };
        }
    }
}
