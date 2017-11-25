﻿using System.Threading.Tasks;
using MassTransit;
using MassTransit.Logging;
using HelpDesk.Common.EventBus.AppEvents;
using HelpDesk.Common.EventBus.AppEvents.Interface;
using HelpDesk.ConsumerEventSrvice.Consumers.Interface;
using HelpDesk.ConsumerEventSrvice.Sender;
using HelpDesk.ConsumerEventSrvice.DTO;

namespace HelpDesk.ConsumerEventSrvice.Consumers
{
    public class UserPasswordRecoveryAppEventConsumer : IConsumer<IUserPasswordRecoveryAppEvent>
    {
        private readonly ILog log;
        private readonly ISender sender;
        public UserPasswordRecoveryAppEventConsumer(IUserPasswordRecoveryAppEventConsumerLog log, ISender sender)
        {
            this.log = log;
            this.sender = sender;
        }

        public async Task Consume(ConsumeContext<IUserPasswordRecoveryAppEvent> context)
        {
            log.InfoFormat("UserPasswordRecoveryAppEventConsumer: Email = {0}", context.Message.Email);
            await Task.Run(() =>
            {
                sender.Send(new UserPasswordRecoveryAppEventSubscribeDTO
                {
                    Email = context.Message.Email,
                    Password = context.Message.Password
                }, "UserPasswordRecoveryAppEvent");
                log.InfoFormat("UserPasswordRecoveryAppEventConsumer Send OK: Email = {0}", context.Message.Email);
            });
        }
    }
}