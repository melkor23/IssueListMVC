using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Resources;
using System.Web.Http;
using IssueList.Infraestructura.Enum;
using IssueList.Infraestructura.Resources;

namespace IssueListMVC.Controllers
{
    [RoutePrefix("api/Enum")]
    [Authorize]
    public class EnumController : ApiController
    {
        
        [Route("GetSeverityEnum")]
        public List<ValueTextPair> GetSeverityEnum()
        {
            List<ValueTextPair> severityList = new List<ValueTextPair>();
            severityList.Add(new ValueTextPair((int)SeverityEnum.High, Resource.SeverityEnum_High));
            severityList.Add(new ValueTextPair((int)SeverityEnum.Medium, Resource.SeverityEnum_Medium));
            severityList.Add(new ValueTextPair((int)SeverityEnum.Low, Resource.SeverityEnum_Low));

            return severityList;
        }

        
        [Route("GetStatusEnum")]
        public List<ValueTextPair> GetStatusEnum()
        {
            List<ValueTextPair> statusList = new List<ValueTextPair>();
            statusList.Add(new ValueTextPair((int)StatusEnum.TODO , Resource.StatusEnum_TODO));
            statusList.Add(new ValueTextPair((int)StatusEnum.DOING, Resource.StatusEnum_DOING));
            statusList.Add(new ValueTextPair((int)StatusEnum.DONE, Resource.StatusEnum_DONE));

            return statusList;
        }

     
    }
}
