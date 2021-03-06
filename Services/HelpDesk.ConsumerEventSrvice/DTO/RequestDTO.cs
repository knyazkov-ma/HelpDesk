﻿using HelpDesk.Common.Helpers;
using HelpDesk.DataService.Common.DTO;
using HelpDesk.Entity;
using System;

namespace HelpDesk.ConsumerEventService.DTO
{
    /// <summary>
    /// Заявка
    /// </summary>
    public class RequestDTO
    {
        
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Описание проблемы
        /// </summary>
        public string DescriptionProblem { get; set; }

        /// <summary>
        /// RAW-состояние
        /// </summary>
        public StatusRequest Status { get; set; }

        /// <summary>
        /// Факторизованное состояние
        /// </summary>
        public StatusRequestEnum StatusRequest { get; set; }

        public string StatusName { get { return StatusRequest.GetDisplayName(); }}

        public string StatusBackColor { get { return Status.BackColor; }}
        
        /// <summary>
        /// Дата подачи заявки
        /// </summary>
        public DateTime DateInsert { get; set; }

        /// <summary>
        /// Дата последнего изменения заявки
        /// </summary>
        public DateTime? DateUpdate { get; set; }
                       

        /// <summary>
        /// Плановая дата окончания работ по заявке (изначально проставляется системой автоматически)
        /// </summary>
        public DateTime? DateEndPlan { get; set; }

        /// <summary>
        /// Фактическая дата окончания работ по заявке (проставляется при подтверждении закрытия заявки как текущая дата)
        /// </summary>
        public DateTime? DateEndFact { get; set; }

        /// <summary>
        /// Наименование Исполнителя
        /// </summary>
        public string WorkerName { get; set; }

        

        public RequestObject Object { get; set; }

        
        
        /// <summary>
        /// Количество переносов срока
        /// </summary>
        public int CountCorrectionDateEndPlan { get; set; }

        /// <summary>
        /// Архивная
        /// </summary>
        public bool Archive { get; set; }

        

        /// <summary>
        /// Полное наименование объекта заявки (ТО и т.п.)
        /// </summary>
        public string ObjectName
        {
            get
            {
                return RequestObjectDTO.GetObjectName(Object.ObjectType.Name, 
                    Object.SoftName, 
                    Object.HardType, 
                    Object.Model);
            }
        }
        
        public string EmployeeFM { get; set; }
        public string EmployeeIM { get; set; }
        public string EmployeeOT { get; set; }

        public string EmployeeName { get { return String.Format("{0} {1} {2}", EmployeeFM, EmployeeIM, EmployeeOT); } }
        public string EmployeePostName { get; set; }

        public string EmployeeCabinet { get; set; }

        public string EmployeePhone { get; set; }

        public string EmployeeOrganizationName { get; set; }
        public string EmployeeOrganizationAddress { get; set; }

        public WorkerUser User { get; set; }
        
        

        #region только для активных заявок
        /// <summary>
        /// Просрочена
        /// </summary>
        public bool Expired
        {
            get
            {
                if (DateEndFact.HasValue)
                    return DateEndPlan.Value < DateEndFact.Value;

                if (DateEndPlan.HasValue)
                    return DateEndPlan.Value < DateTime.Now;

                return false;
            }
        }

        /// <summary>
        /// Истекает срок
        /// </summary>
        public bool Deadline
        {
            get
            {
                if (Object.ObjectType.DeadlineHour > 0 && DateEndPlan.HasValue && !DateEndFact.HasValue)
                    return DateEndPlan.Value <= DateTime.Now.AddHours(Object.ObjectType.DeadlineHour) && DateEndPlan.Value > DateTime.Now;

                return false;
            }
        }

        /// <summary>
        /// Последнее событие
        /// </summary>
        public RequestEventDTO LastEvent { get; set; }
        #endregion только для активных заявок

    }
}
