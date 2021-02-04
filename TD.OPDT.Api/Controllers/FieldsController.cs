using System.Web.Http;
using TD.Core.Api.Mvc;

namespace TD.OPDT.Api.Controllers
{
    //[RoutePrefix("sites/[SiteName]/_apis/opdtapi/fields")]
    public class FieldsController : TDApiController
    {
        public FieldsController()
        {

        }

        //[Route("opdtapi/Fields/count")]
        [Route("opdtapi/Fields/count")]
        [HttpGet]
        public IHttpActionResult Count()
        {
            return ApiOk(1);
        }

    }
}
