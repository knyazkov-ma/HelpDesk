﻿using System.Threading.Tasks;
using MassTransit;
using MassTransit.Logging;
using HelpDesk.Data.Query;
using HelpDesk.ConsumerEventService.Sender;
using HelpDesk.ConsumerEventService.DTO;
using System.Collections.Generic;
using System.Linq;
using HelpDesk.ConsumerEventService.Query;
using HelpDesk.ConsumerEventService.Resources;
using HelpDesk.EventBus.Common.AppEvents.Interface;

namespace HelpDesk.ConsumerEventService.Consumers
{
    /// <summary>
    /// Получатель события "Истекает срок по заявке"
    /// </summary>
    public class RequestDeedlineAppEventConsumer : IConsumer<IRequestDeedlineAppEvent>
    {
        private readonly ILog log;
        private readonly IQueryRunner queryRunner;
        private readonly ISender sender;
        public RequestDeedlineAppEventConsumer(IQueryRunner queryRunner, ILog log, 
            ISender sender)
        {
            this.queryRunner = queryRunner;
            this.log = log;
            this.sender = sender;
        }

        static object lockObj = new object();
        public async Task Consume(ConsumeContext<IRequestDeedlineAppEvent> context)
        {
            log.InfoFormat("RequestDeedlineAppEventConsumer: RequestIds.Count = {0}", context.Message.RequestIds.Count());

            IEnumerable<UserDeedlineAppEventSubscribeDTO> list = null;
            lock (lockObj)
            {
                list = queryRunner.Run(new UserRequestDeedlineAppEventSubscribeQuery(context.Message.RequestIds)); 
            }
            if (list == null || !list.Any())
                return;
            await Task.Run(() =>
            {
                foreach (var evnt in list)
                {
                    sender.Send(evnt, Resource.Subject_RequestDeedlineAppEventConsumer, "RequestDeedlineAppEvent");
                    log.InfoFormat("RequestDeedlineAppEventConsumer Send OK: Email = {0}", evnt.Email);
                }
            });
        }
    }
}