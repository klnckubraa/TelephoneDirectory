using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Repositories.Interface;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TypeController : Controller
    {
        private readonly ITypeRepository _TypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TypeController> _logger;

        public TypeController(ITypeRepository typeRepository, IMapper mapper, ILogger<TypeController> logger)
        {
            _TypeRepository = typeRepository;
            _mapper = mapper;
            _logger = logger;
        }



        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var Type = _TypeRepository.GetAll();
            if (Type == null)
            {
                _logger.LogError("Type Controller'da listeleme yapılamadı.");
                return NotFound();
            }
            var typeDto = _mapper.Map<List<TypeDto>>(Type.ToList());
            _logger.LogInformation("Type Controller'da listelendi.");
            return Ok(typeDto);
        }

    }
}
