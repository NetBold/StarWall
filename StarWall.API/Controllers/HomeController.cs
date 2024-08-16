using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarWall.Core.Interfaces;

namespace StarWall.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("GetWebsiteInfo")]
        public async Task<IActionResult> GetWebsiteInfo()
        {
            _logger.LogInformation($"GET Attempt for {nameof(GetWebsiteInfo)}");
            var allWebSiteInfos = await _unitOfWork.WebSiteInfos.GetAll();
            var webSiteInfo = allWebSiteInfos.FirstOrDefault();
            return Ok(webSiteInfo);
        }
    }
}
