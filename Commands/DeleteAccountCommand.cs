using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mediator_cqrs_project.Commands
{
    public class DeleteAccountCommand : IRequest<string>
    {
        public string DocumentNumber { get; set; }
    }
}
