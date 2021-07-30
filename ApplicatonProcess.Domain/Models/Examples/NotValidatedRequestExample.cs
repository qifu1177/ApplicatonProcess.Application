using ApplicatonProcess.Domain.Interfaces;

namespace ApplicatonProcess.Domain.Models.Examples
{
   
    public class NotValidatedRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ErrorResponse { ErrorCode = 400, Message = "Could not be validated." };
        }
    }
}
