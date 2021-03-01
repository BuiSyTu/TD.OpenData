using System.Web.Http;
using System.Web.Http.ModelBinding;
using TD.Core.Api.Mvc;
using TD.OPDT.Data.FilterModels;
using TD.OPDT.Data.Models;
using TD.OPDT.Data.Repositories;

namespace TD.OPDT.Api.Controllers
{
    public class OfficesController : TDApiController
    {
        private OfficeRepository _officeRepository;
        public OfficesController(OfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        [Route("")]
        [Route("~/opdtapi/offices")]
        [HttpGet]
        public IHttpActionResult GetOffices()
        {
            var offices = _officeRepository.GetAll();
            return ApiOk(offices);
        }

        [Route("")]
        [Route("~/opdtapi/Offices")]
        [HttpPost]
        public IHttpActionResult PostOffice(Office Office)
        {
            if (!ModelState.IsValid)
            {
                return ApiBadRequest(null, ModelState);
            }

            var entity = _officeRepository.Add(Office);
            return ApiCreated(entity);
        }

        [Route("{id:int:min(1)}")]
        [Route("~/opdtapi/Offices/{id:int:min(1)}")]
        [HttpGet]
        public IHttpActionResult GetOffice(int id)
        {
            var office = _officeRepository.GetById(id);

            if (office == null)
            {
                return ApiNotFound();
            }

            return ApiOk(office);
        }

        [Route("{id:int:min(1)}")]
        [Route("~/opdtapi/Offices/{id:int:min(1)}")]
        [HttpPut]
        public IHttpActionResult PutOffice(int id, Office change)
        {
            if (!ModelState.IsValid)
            {
                return ApiBadRequest(null, ModelState);
            }

            if (id != change.Id)
            {
                return ApiBadRequest();
            }

            _officeRepository.Update(change);
            return ApiNoContent();
        }

        [Route("{id:int:min(1)}")]
        [Route("~/opdtapi/Offices/{id:int:min(1)}")]
        [HttpDelete]
        public IHttpActionResult DeleteOffice(int id)
        {
            var office = _officeRepository.GetById(id);

            if (office == null)
            {
                return ApiNotFound();
            }

            _officeRepository.Delete(office);
            return ApiNoContent();
        }
    }
}
