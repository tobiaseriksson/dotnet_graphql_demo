using HotChocolate;

namespace com.nkt.npt.GraphQL.ErrorHandling {

    public class GraphQLErrorHandler : IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.WithMessage(error.Exception.Message);
        }
    }

}