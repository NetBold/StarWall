using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using StarWall.Core.DTOs;
using StarWall.Core.Interfaces;
using StarWall.Domain.ContactEntities;

namespace StarWall.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ContactController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] ContactDTO contactDTO)
        {
            _logger.LogInformation($"POST Attempt for {nameof(SendMessage)}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST Attempt for {nameof(SendMessage)}");
                return BadRequest(ModelState);
            }
            var contact = _mapper.Map<Contact>(contactDTO);
            contact.CreationDate = DateTime.Now;
            contact.IsSeenByAdmin = false;
            await _unitOfWork.Contacts.Insert(contact);
            await _unitOfWork.Save();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllMessages")]
        public async Task<IActionResult> GetAllMessages()
        {
            _logger.LogInformation($"GET Attempt for {nameof(SendMessage)}");
            var messages = await _unitOfWork.Contacts.GetAll();
            return Ok(messages);
        }
    }
}
