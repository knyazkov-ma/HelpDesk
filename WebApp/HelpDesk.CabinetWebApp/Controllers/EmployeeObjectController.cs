﻿using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using HelpDesk.DataService.Interface;
using Microsoft.AspNet.Identity;
using HelpDesk.DataService.DTO;
using HelpDesk.Common;
using HelpDesk.DataService.Filters;
using HelpDesk.CabinetWebApp.Models;
using System.Collections;

namespace HelpDesk.CabinetWebApp.Controllers
{

    public class EmployeeObjectController : BaseApiController
    {
        private readonly IObjectService objectService;
        private readonly IEmployeeObjectService employeeObjectService;
                
        public EmployeeObjectController(
            IObjectService objectService,
            IEmployeeObjectService employeeObjectService)
        {
            this.objectService = objectService;
            this.employeeObjectService = employeeObjectService;
           
        }

        [Route("api/{lang}/EmployeeObject/GetNewRequestObjectIS")]
        [HttpGet]
        public IHttpActionResult GetNewRequestObjectIS()
        {
            return execute(delegate ()
            {
                RequestObjectISDTO obj = new RequestObjectISDTO();
                result = Json(new { success = true, data = obj });
            });
        }

        [Route("api/{lang}/EmployeeObject/GetNewRequestObjectTO")]
        [HttpGet]
        public IHttpActionResult GetNewRequestObjectTO()
        {
            return execute(delegate ()
            {
                RequestObjectTODTO obj = new RequestObjectTODTO();
                result = Json(new { success = true, data = obj });
            });
        }

        [Route("api/{lang}/EmployeeObject/GetListEmployeeObject")]
        [HttpGet]
        public IHttpActionResult GetListEmployeeObject([FromUri]EmployeeObjectFilter filter, [FromUri]OrderInfo orderInfo, [FromUri]PageInfo pageInfo)
        {
            return execute(delegate ()
            {
                long userId = User.Identity.GetUserId<long>();
                IEnumerable<EmployeeObjectDTO> list = employeeObjectService.GetListEmployeeObject(userId, filter, orderInfo, ref pageInfo);
                result = Json(new { success = true, data = list, totalCount = pageInfo.TotalCount, count = pageInfo.Count });
            });
        }

        [Route("api/{lang}/EmployeeObject/GetEmployeeObjectTree")]
        [HttpGet]
        [ResponseType(typeof(IList<jstree>))]
        public IEnumerable GetEmployeeObjectTree(long? parentId)
        {
            long userId = User.Identity.GetUserId<long>();
            IEnumerable<EmployeeObjectDTO> list = employeeObjectService.GetListEmployeeObject(userId);
            List<jstree> items = new List<jstree>();

            if (!parentId.HasValue)
            {
                items.Add(new jstree
                {
                    id = "-1",
                    parent = "#",
                    text = "ПО",
                    children = true
                });
                items.Add(new jstree
                {
                    id = "-2",
                    parent = "#",
                    text = "Оборудование",
                    children = true
                });

                return items;
            }

            if (parentId == -1)
            {
                foreach (EmployeeObjectDTO o in list)
                {
                    if (o.Soft)
                        items.Add(new jstree
                        {
                            id = o.ObjectId.ToString(),
                            parent = "-1",
                            text = o.ObjectName,
                            children = false
                        });
                }

                return items;
            }

            if (parentId == -2)
            {
                foreach (EmployeeObjectDTO o in list)
                {
                    if (!o.Soft)
                        items.Add(new jstree
                        {
                            id = o.ObjectId.ToString(),
                            parent = "-2",
                            text = o.ObjectName,
                            children = false
                        });
                }

                return items;
            }

            return items;
        }

        [Route("api/{lang}/EmployeeObject/GetListEmployeeObjectByName")]
        [HttpGet]
        public IHttpActionResult GetListEmployeeObjectByName(string objectName = null)
        {
            return execute(delegate ()
            {
                long userId = User.Identity.GetUserId<long>();
                IEnumerable<EmployeeObjectDTO> list = employeeObjectService.GetListEmployeeObject(userId, objectName);
                result = Json(new { success = true, data = list });
            });
        }

        [Route("api/{lang}/EmployeeObject/GetListAllowableObjectIS")]
        [HttpGet]
        public IHttpActionResult GetListAllowableObjectIS(string name = null)
        {
            return execute(delegate ()
            {
                long userId = User.Identity.GetUserId<long>();
                IEnumerable<RequestObjectISDTO> list = employeeObjectService.GetListAllowableObjectIS(userId, name);
                result = Json(new { success = true, data = list });
            });
        }

        [Route("api/{lang}/EmployeeObject/GetListAllowableObjectType")]
        [HttpGet]
        public IHttpActionResult GetListAllowableObjectType()
        {
            return execute(delegate ()
            {
                long userId = User.Identity.GetUserId<long>();
                IEnumerable<SimpleDTO> list = employeeObjectService.GetListAllowableObjectType(userId);
                result = Json(new { success = true, data = list });
            });
        }
        

        [Route("api/{lang}/EmployeeObject/GetListWare")]
        [HttpGet]
        public IHttpActionResult GetListWare()
        {
            return execute(delegate ()
            {
                IEnumerable<WareDTO> list = objectService.GetListWare();
                result = Json(new { success = true, data = list });
            });
        }

        
        [Route("api/{lang}/EmployeeObject/GetListHardType")]
        [HttpGet]
        public IHttpActionResult GetListHardType(string name = null)
        {
            return execute(delegate ()
            {
                IEnumerable<SimpleDTO> list = objectService.GetListHardType(name);
                result = Json(new { success = true, data = list });
            });
        }

        [Route("api/{lang}/EmployeeObject/GetListModel")]
        [HttpGet]
        public IHttpActionResult GetListModel(long manufacturerId, string name = null)
        {
            return execute(delegate ()
            {
                IEnumerable<SimpleDTO> list = objectService.GetListModel(manufacturerId, name);
                result = Json(new { success = true, data = list });
            });
        }

        [Route("api/{lang}/EmployeeObject/GetListManufacturer")]
        [HttpGet]
        public IHttpActionResult GetListManufacturer(string name = null)
        {
            return execute(delegate ()
            {
                IEnumerable<SimpleDTO> list = objectService.GetListManufacturer(name);
                result = Json(new { success = true, data = list });
            });
        }
        
        [Route("api/{lang}/EmployeeObject/AddIS")]
        [HttpPost]
        public IHttpActionResult AddIS(RequestObjectISDTO dto)
        {
            return execute(delegate ()
            {
                long userId = User.Identity.GetUserId<long>();
                employeeObjectService.AddIS(userId, dto);
                
                result = Json(new { success = true });
            });
        }

        [Route("api/{lang}/EmployeeObject/AddTO")]
        [HttpPost]
        public IHttpActionResult AddTO(RequestObjectTODTO dto)
        {
            return execute(delegate ()
            {
                long userId = User.Identity.GetUserId<long>();
                employeeObjectService.AddTO(userId, dto);

                result = Json(new { success = true });
            });
        }

        [Route("api/{lang}/EmployeeObject/Delete/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            return execute(delegate ()
            {
                long userId = User.Identity.GetUserId<long>();
                employeeObjectService.Delete(userId, id);
                result = Json(new { success = true });
            });
        }
    }
}