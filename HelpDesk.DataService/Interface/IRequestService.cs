﻿using HelpDesk.Common;
using HelpDesk.DataService.Filters;
using HelpDesk.DTO;
using HelpDesk.DTO.Parameters;
using HelpDesk.Entity;
using System.Collections.Generic;

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
        
        IEnumerable<StatusRequestDTO> GetListStatus(bool archive);
        IEnumerable<StatusRequest> GetListRawStatus(bool archive);

        int GetCountRequiresConfirmation(long employeeId);

        IEnumerable<Year> GetListArchiveYear(long employeeId);

        IEnumerable<RequestEventDTO> GetListRequestEvent(long requestId);

        long Save(RequestParameter dto);
        void CreateRequestEvent(long userId, RequestEventParameter dto);
    }
}
