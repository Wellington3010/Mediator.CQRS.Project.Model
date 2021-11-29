using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mediator_cqrs_project.Notifications
{
    public class ErrorNotification : INotification
    {
        public string ErrorMessage { get; set; }
    }
}
