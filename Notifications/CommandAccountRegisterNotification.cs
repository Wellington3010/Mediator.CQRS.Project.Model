﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastMiddleware.Interfaces;

namespace mediator_cqrs_project.Notifications
{
    public class CommandAccountRegisterNotification : INotification
    {
        public string DocumentNumber { get; private set; }

        public string AccountOwner { get; private set; }
    }
}
