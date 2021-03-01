using System.Web.Http;
using System.Web.Http.ModelBinding;
using TD.Core.Api.Mvc;
using TD.OPDT.Data.FilterModels;
using TD.OPDT.Data.Models;
using TD.OPDT.Data.Repositories;

namespace TD.OPDT.Api.Controllers
{
    public class DataSetsController : TDApiController
    {
        private IDataSetRepository _dataSetRepository;
        public DataSetsController(IDataSetRepository dataSetRepository)
        {
            _dataSetRepository = dataSetRepository;
        }

        [Route("")]
        [Route("~/opdtapi/dataSets")]
        [HttpGet]
        public IHttpActionResult GetdataSets()
        {
            var dataSets = _dataSetRepository.GetAll();
            return ApiOk(dataSets);
        }

        [Route("")]
        [Route("~/opdtapi/dataSets")]
        [HttpPost]
        public IHttpActionResult PostdataSet(DataSet dataSet)
        {
            if (!ModelState.IsValid)
            {
                return ApiBadRequest(null, ModelState);
            }

            var entity = _dataSetRepository.Add(dataSet);
            return ApiCreated(entity);
        }

        [Route("{id:int:min(1)}")]
        [Route("~/opdtapi/dataSets/{id:int:min(1)}")]
        [HttpGet]
        public IHttpActionResult GetdataSet(int id)
        {
            var dataSet = _dataSetRepository.GetById(id);

            if (dataSet == null)
            {
                return ApiNotFound();
            }

            return ApiOk(dataSet);
        }

        [Route("{id:int:min(1)}")]
        [Route("~/opdtapi/dataSets/{id:int:min(1)}")]
        [HttpPut]
        public IHttpActionResult PutdataSet(int id, DataSet change)
        {
            if (!ModelState.IsValid)
            {
                return ApiBadRequest(null, ModelState);
            }

            if (id != change.Id)
            {
                return ApiBadRequest();
            }

            _dataSetRepository.Update(change);
            return ApiNoContent();
        }

        [Route("{id:int:min(1)}")]
        [Route("~/opdtapi/dataSets/{id:int:min(1)}")]
        [HttpDelete]
        public IHttpActionResult DeletedataSet(int id)
        {
            var dataSet = _dataSetRepository.GetById(id);

            if (dataSet == null)
            {
                return ApiNotFound();
            }

            _dataSetRepository.Delete(dataSet);
            return ApiNoContent();
        }
    }
}
