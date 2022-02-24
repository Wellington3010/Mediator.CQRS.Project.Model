using mediator_cqrs_project.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator_cqrs_project.EventHandlers
{
    public class ResponsesEventHandler : INotificationHandler<CommandAccountRegisterNotification>, 
                                         INotificationHandler<ErrorNotification>,
                                         INotificationHandler<CommandAccountUpdateNotification>,
                                         INotificationHandler<CommandAccountDeleteNotification>,
                                         INotificationHandler<QueryAccountNotification>,
                                         INotificationHandler<QueryAccountListNotification>

    {
        public async Task Handle(CommandAccountRegisterNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Run(() =>
                {
                    Console.WriteLine($"Account register with successful: {notification.AccountOwner} - {notification.DocumentNumber}");
                });
            }
            catch (Exception ex)
            {
                cancellationToken.Register(() => {
                    Console.WriteLine($"Request canceled: {ex.Message}");
                });
            }
        }

        public async Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Run(() =>
                {
                    Console.WriteLine($"Error: {notification.ErrorMessage}");
                });
            }
            catch(Exception ex)
            {
                cancellationToken.Register(() => {
                    Console.WriteLine($"Request canceled: {ex.Message}");
                });
            }
        }

        public async Task Handle(CommandAccountUpdateNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Run(() =>
                {
                    Console.WriteLine($"Account updated with successful: {notification.AccountOwner}-{notification.DocumentNumber}-{notification.AccountBalance}");
                });
            }
            catch(Exception ex)
            {
                cancellationToken.Register(() => {
                    Console.WriteLine($"Request canceled: {ex.Message}");
                });
            }
        }

        public async Task Handle(CommandAccountDeleteNotification notification, CancellationToken cancellationToken)
        {

            try
            {
                await Task.Run(() =>
                {
                    Console.WriteLine($"Account deleted with successful: {notification.AccountOwner}-{notification.DocumentNumber}");
                });

            }
            catch (Exception ex)
            {
                cancellationToken.Register(() => {
                    Console.WriteLine($"Request canceled: {ex.Message}");
                });
            }
        }

        public async Task Handle(QueryAccountNotification notification, CancellationToken cancellationToken)
        {

            try
            {
                await Task.Run(() =>
                {
                    Console.WriteLine($"Account found by document number: {notification.AccountOwner}-{notification.DocumentNumber}");
                });
            }
            catch(Exception ex)
            {
                cancellationToken.Register(() => {
                    Console.WriteLine($"Request canceled: {ex.Message}");
                });
            }
           
        }

        public async Task Handle(QueryAccountListNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Run(() =>
                {
                    Console.WriteLine($"Accounts list founded by document type: {notification}");
                });
            }
            catch (Exception ex)
            {
                cancellationToken.Register(() => {
                    Console.WriteLine($"Request canceled: {ex.Message}");
                });
            }
        }
    }
}
