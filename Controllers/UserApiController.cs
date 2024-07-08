using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using scoopepooper_backend.Data;
using scoopepooper_backend.EfCore;
using scoopepooper_backend.Model;
using ShoppingWebApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace scoopepooper_backend.Controllers
{
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly UserRepo _userRepo;
        private readonly EntryRepo _entryRepo;
        public UserApiController(EF_DataContext eF_DataContext)
        {
            _userRepo = new UserRepo(eF_DataContext);
            _entryRepo = new EntryRepo(eF_DataContext);
        }
        // GET: api/<UserApiController>
        [HttpGet]
        [Route("api/[controller]/GetUsers")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<UserModel> data = _userRepo.GetAll();
                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<UserApiController>
        [HttpPost]
        [Route("api/[controller]/PostUser")]
        public IActionResult Post()
        {
            UserModel user = new UserModel();
            Random rand = new Random();
            ResponseType type = ResponseType.Success;
            var someNumber = rand.Next(1000, 9999);
            try
            {
                user.editkey = someNumber;
                var userid = _userRepo.Create(user);
                EntryModel entry = new EntryModel();
                entry.id = userid;
                _entryRepo.Create(entry);
                return Ok(ResponseHandler.GetAppResponse(type, user.editkey));

            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }

        }

        // DELETE api/<UserApiController>/5
        [HttpDelete("{id}")]
        [Route("api/[controller]/DeleteUser")]
        public IActionResult Delete(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                _userRepo.Delete(id);
                return Ok(ResponseHandler.GetAppResponse(type, "User has been deleted."));
            }
            catch (Exception ex) 
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }
    }
}
