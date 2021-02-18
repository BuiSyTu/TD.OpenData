using System.Web.Http;
using TD.Core.Api.Mvc;
using TD.OPDT.Data.FilterModels;
using TD.OPDT.Data.Models;
using TD.OPDT.Data.Repositories;

namespace TD.OPDT.Api.Controllers
{
    //[RoutePrefix("sites/[SiteName]/_apis/opdtapi/fields")]
    public class FieldsController : TDApiController
    {
        private IFieldRepository _fieldRepository;
        public FieldsController(IFieldRepository fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }

        [Route("")]
        [Route("~/opdtapi/fields")]
        [HttpGet]
        public IHttpActionResult GetFields([FromUri] FieldFilterModel filterModel)
        {
            var fields = _fieldRepository.Get(filterModel);
            return ApiOk(fields);
        }

        [Route("")]
        [Route("~/opdtapi/fields")]
        [HttpPost]
        public IHttpActionResult PostField(Field field)
        {
            if (!ModelState.IsValid) {
                return ApiBadRequest(null, ModelState);
            }

            var fields = _fieldRepository.Create(field);
            return ApiCreated(field);
        }

        [Route("{id:int:min(1)}")]
        [Route("~/opdtapi/fields/{id:int:min(1)}")]
        [HttpGet]
        public IHttpActionResult GetField(int id)
        {
            var field = _fieldRepository.GetById(id);

            if (field == null) {
                return ApiNotFound();
            }

            return ApiOk(field);
        }

        [Route("{id:int:min(1)}")]
        [Route("~/opdtapi/fields/{id:int:min(1)}")]
        [HttpPut]
        public IHttpActionResult PutField(int id, Field change)
        {
            if (ModelState.IsValid)
            {
                return ApiBadRequest(null, ModelState);
            }

            if (id != change.Id)
            {
                return ApiBadRequest();
            }

            _fieldRepository.Update(change);
            return ApiNoContent();
        }

        [Route("{id:int:min(1)}")]
        [Route("~/opdtapi/fields/{id:int:min(1)}")]
        [HttpDelete]
        public IHttpActionResult DeleteField(int id)
        {
            var field = _fieldRepository.GetById(id);

            if (field == null)
            {
                return ApiNotFound();
            }

            _fieldRepository.Remove(field);
            return ApiNoContent();
        }
    }
}
