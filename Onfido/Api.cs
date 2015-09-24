using Onfido.Http;
using Onfido.Resources;

namespace Onfido
{
    public class Api
    {
        public Applicants Applicants { get; set; }

        public Documents Documents { get; set; }

        public Checks Checks { get; set; }

        public Reports Reports { get; set; }

        public Api() :this(new Requestor())
        {
        }

        public Api(IRequestor requestor)
        {
            Applicants = new Applicants(requestor);

            Documents = new Documents(requestor);

            Checks = new Checks(requestor);

            Reports = new Reports(requestor);
        }
    }
}
