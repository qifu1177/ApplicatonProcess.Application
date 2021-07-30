using ApplicatonProcess.Domain.Interfaces;


namespace ApplicatonProcess.Domain.Models.Examples
{
    public class UserResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new UserResponse { Email = "th@gmail.com", Address = "kriemhild Str. 10, 93050", Age = 20, FirstName = "Hans", LastName = "Henne" };
        }
    }
}
