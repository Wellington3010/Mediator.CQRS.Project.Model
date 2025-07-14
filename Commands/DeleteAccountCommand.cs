using FastMiddleware;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastMiddleware.Interfaces;

namespace mediator_cqrs_project.Commands
{
    public class DeleteAccountCommand : IRequest<string>
    {
        public string DocumentNumber { get; set; }
    }
}
