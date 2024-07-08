using Microsoft.AspNetCore.Mvc;
using scoopepooper_backend.Data;
using scoopepooper_backend.EfCore;
using scoopepooper_backend.Model;
using ShoppingWebApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace scoopepooper_backend.Controllers
{
    [ApiController]
    public class EntryApiController : ControllerBase
    {
        private readonly UserRepo _userRepo;
        private readonly EntryRepo _entryRepo;

        public EntryApiController(EF_DataContext eF_DataContext)
        {
            _userRepo = new UserRepo(eF_DataContext);
            _entryRepo = new EntryRepo(eF_DataContext);
        }

        // GET: api/<EntryApiController>
        [HttpGet]
        [Route("api/[controller]/GetEntries")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<EntryModel> data = _entryRepo.GetAll();
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

        // GET api/<EntryApiController>/5
        [HttpGet("{id}")]
        [Route("api/[controller]/GetEntry")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var data = _entryRepo.Get(id);
                if (data == null)
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

        // PUT api/<EntryApiController>/5
        [HttpPut("{id}")]
        [Route("api/[controller]/PutEntry")]
        public IActionResult Put(EntryModel entry, int editkey)
        {
            ResponseType type = ResponseType.Success;
            if(editkey != _userRepo.Get(entry.user_Id).editkey)
            {
                Exception ex = new Exception();
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
            try
            {
                _entryRepo.Update(entry);
                return Ok(ResponseHandler.GetAppResponse(type, entry));
            }
            catch (Exception ex) 
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<EntryApiController>/5
        [HttpDelete("{id}")]
        [Route("api/[controller]/DeleteEntry")]
        public IActionResult Delete(int id, int editkey)
        {
            ResponseType type = ResponseType.Success;
            var entry = _entryRepo.Get(id);
            if (editkey != _userRepo.Get(entry.user_Id).editkey)
            {
                Exception ex = new Exception();
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
            try
                {
                _entryRepo.Delete(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Entry has been deleted."));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));

            }
        }
    }
}
