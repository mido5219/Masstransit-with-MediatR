using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public class ConsumerEndpointsSettings
    {
        public string ActionAssignedEmailServiceAddress { get; set; }
        public string ProductCreatedServiceAddress { get; set; }
        public string ProductDeletedServiceAddress { get; set; }

    }
}
