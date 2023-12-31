using Microsoft.AspNetCore.Mvc;
using PhoneBook.Exceptions;
using PhoneBook.Model;
using PhoneBook.Services;
using PhoneBook.Logging;

namespace PhoneBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookService _phoneBookService;
        private readonly ILogger<PhoneBookController> _logger;

        public PhoneBookController(IPhoneBookService phoneBookService, ILogger<PhoneBookController> logger)
        {
            _phoneBookService = phoneBookService;
            _logger = logger;
        }

        [HttpGet]
        [Route("list")]
        public IEnumerable<PhoneBookEntry> List()
        {
            Logger.WriteLog($"An admin just had the names listed");
            return _phoneBookService.List();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody]PhoneBookEntry newEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _phoneBookService.Add(newEntry);

            if (PhoneBook.Services.DictionaryPhoneBookService.AddSuccess == "SUCCESS")
            {
                return Ok("User added Succesfully");
            }

            else 
            {
                return BadRequest("User could not be added");
            }
            
        }

        [HttpPut]
        [Route("deleteByName")]
        public IActionResult DeleteByName([FromQuery] string name)
        {
            try
            {
                _phoneBookService.DeleteByName(name);

                return Ok("User Deleted Succesfully");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("deleteByNumber")]
        public IActionResult DeleteByNumber([FromQuery] string number)
        {
            try
            {
                _phoneBookService.DeleteByNumber(number);

                return Ok("User Deleted Successfully");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}