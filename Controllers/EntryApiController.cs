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
            try
            {
                IEnumerable<EntryModel> data = _entryRepo.GetAll();
                if (!data.Any())
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<EntryApiController>/5
        [HttpGet]
        [Route("api/[controller]/GetEntry/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var data = _entryRepo.Get(id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<EntryApiController>/5
        [HttpPut]
        [Route("api/[controller]/PutEntry")]
        public IActionResult Put(EntryModel entry, int editkey)
        {
            if(editkey != _userRepo.Get(entry.user_Id).editkey)
            {
                return NotFound();
            }
            try
            {
                _entryRepo.Update(entry);
                return Ok(entry);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<EntryApiController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteEntry/{id}")]
        public IActionResult Delete(int id, int editkey)
        {
            var entry = _entryRepo.Get(id);
            if (editkey != _userRepo.Get(entry.user_Id).editkey)
            {
                return NotFound();
            }
            try
            {
                _entryRepo.Delete(id);
                return Ok("Entry has been deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }
        }
    }
}
