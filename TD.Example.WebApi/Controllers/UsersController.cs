using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using TD.Core.Api.Mvc;
using TD.Example.Library.Models;
using TD.Example.Library.Repositories;
using TD.Example.Library.ViewModels;

namespace SharePointProject2.WebApi.Controller
{
    public class UsersController : TDApiController
    {
        private readonly IUserRepository _UserRepository;

        public UsersController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        // GET: webapi/Users
        [Route("webapi/Users")]
        [HttpGet]
        public IHttpActionResult GetUsers(int skip = 0, int top = 100, string q = null, string orderBy = null, bool count = false)
        {
            ICollection<string> collecionOrderBy = null;
            if (!string.IsNullOrEmpty(orderBy))
            {
                collecionOrderBy = new Regex(@"\s*,\s*").Split(orderBy);
            }
            var data = _UserRepository.Get(skip, top, q, false, collecionOrderBy);
            return ApiOk(data,
                        null,
                        (result) =>
                        {
                            if (count)
                            {
                                result.ExtensionData["count"] = skip == 0 && top == 0
                                    ? data.Count
                                    : _UserRepository.Count(q, false);
                            }
                        });
        }

        [Route("webapi/Users/search")]
        [HttpPost]
        public object GetUsers(DataTableAjaxPostModel model)
        {
            var User = _UserRepository.CustomServerSideSearchAction(model);
            return User;
        }

        // GET: webapi/Users/5
        [Route("webapi/Users/{id:int:min(1)}")]
        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            var User = _UserRepository.GetById(id);
            if (User == null)
            {
                return NotFound();
            }
            return ApiOk(User);
        }

        // PUT: webapi/Users/5
        [Route("webapi/Users/{id:int:min(1)}")]
        [HttpPut]
        public IHttpActionResult PutUser(int id, User change)
        {
            if (!ModelState.IsValid)
            {
                return ApiBadRequest(null, ModelState);
            }
            _UserRepository.Update(id, change);
            return ApiNoContent();
        }

        // POST: webapi/Users
        [Route("webapi/Users")]
        [HttpPost]
        public IHttpActionResult PostUser(User entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var User = _UserRepository.Add(entity);
            return ApiCreated(User);
        }

        // DELETE: webapi/Users/5
        [Route("webapi/Users/{id:int:min(1)}")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            var User = _UserRepository.Delete(id);
            return ApiOk();
        }

        // GET: webapi/Users/count
        [Route("webapi/Users/count")]
        [HttpGet]
        public IHttpActionResult GetCountUser()
        {
            var count = _UserRepository.Count();
            if (count == 0)
            {
                return ApiNotFound();
            }
            return ApiOk(count);
        }
    }
}
