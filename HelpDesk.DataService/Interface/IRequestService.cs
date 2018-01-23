﻿using HelpDesk.Common;
using HelpDesk.DataService.Filters;
using HelpDesk.DataService.DTO;
using HelpDesk.DataService.DTO.Parameters;
using HelpDesk.Entity;
using System.Collections.Generic;
using System;

namespace HelpDesk.DataService.Interface
{
    public interface IRequestService
    {
        RequestParameter Get(long id = 0);
        RequestParameter GetNewByRequestId(long requestId);
        RequestParameter GetNewByObjectId(long objectId);
        void Delete(long id);
        IEnumerable<RequestDTO> GetListByEmployee(long employeeId, RequestFilter filter, OrderInfo orderInfo, ref PageInfo pageInfo);
        IEnumerable<RequestDTO> GetList(long userId, RequestFilter filter, OrderInfo orderInfo, ref PageInfo pageInfo);
        int GetCountRequiresConfirmationEmployee(long employeeId);
        int GetCountRequiresReaction(long userId);
        long Save(RequestParameter dto);
        void CreateRequestEvent(long userId, RequestEventParameter dto);
        Interval<DateTime, DateTime?> GetAllowableDeadLine(long requestId);
        IEnumerable<SimpleDTO> GetListDescriptionProblem(string name, long objectId);
        IEnumerable<RequestStateCountDTO> GetListRequestStateCount(long userId);


        #region методы с кэшированием
        IEnumerable<Year> GetListEmployeeArchiveYear(long employeeId);
        IEnumerable<Year> GetListArchiveYear(long userId);
        IEnumerable<RequestEventDTO> GetListRequestEvent(long requestId);
        IEnumerable<StatusRequestDTO> GetListStatus(bool archive);
        IEnumerable<StatusRequest> GetListRawStatus(bool archive);
        #endregion методы с кэшированием
    }
}
