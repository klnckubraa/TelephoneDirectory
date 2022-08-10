using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication1.DbModel;
using WebApplication1.DTOs;
using WebApplication1.Repositories;
using WebApplication1.Repositories.Interface;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DetailController : Controller
    {
        private readonly IDetailRepository _DetailRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DetailController> _logger;
        public DetailController(IDetailRepository detailRepository, IMapper mapper, ILogger<DetailController> logger)
        {
            _DetailRepository = detailRepository;
            _mapper = mapper;
            _logger = logger;
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var Detail = _DetailRepository.GetAll();
            if (Detail == null)
            {
                _logger.LogError("Detail Controller'da Listeleme yapılamadı.");
                return NotFound();
            }
            var DetailDto = _mapper.Map<List<DetailDto>>(Detail.ToList());
            _logger.LogInformation("Detail Controller'da Listelendi.");
            return Ok(DetailDto);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var detail = _DetailRepository.GetById(id);
            if (detail == null)
            {
                _logger.LogError("Detail Controller'da böyle bir veri bulunamadı.");
                return NotFound("Böyle bir veri yok.");
            }
            var DetailDto = _mapper.Map<DetailDto>(detail);
            _logger.LogInformation("Detail Controller'da veri çekildi.");
            return Ok(DetailDto);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModal<DetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]

        [HttpPost("Create")]
        public IActionResult Create([FromBody] DetailCreateDto detailCreate)
        {
            var data = _mapper.Map<Detail>(detailCreate);

            if (_DetailRepository.Create(data))
            {
                var dto = _mapper.Map<DetailDto>(data);
                _logger.LogInformation("Detail Controller'da veri eklendi." + detailCreate.Email + " " + detailCreate.Number + " " + detailCreate.PersonId + " " + detailCreate.TypeId);
                return Ok(ResponseModal<DetailDto>.Success(dto, "Başarılı", true));
            }
            else
            {
                _logger.LogError("Detail Controller'da veri eklenemedi." + detailCreate.Email + " " + detailCreate.Number + " " + detailCreate.PersonId + " " + detailCreate.TypeId);
                return BadRequest(ResponseModal<DetailCreateDto>.Errors(detailCreate, "Hatalı", false));
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModal<DetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]

        [HttpPut("Edit")]
        public IActionResult Edit([FromBody] DetailDto detailUpdate)
        {
            var data = _mapper.Map<Detail>(detailUpdate);
            if (_DetailRepository.Update(data))
            {
                var dto = _mapper.Map<DetailDto>(data);
                _logger.LogInformation("Detail Controller'da veri düzenlendi." + detailUpdate.Email + " " + detailUpdate.Number + " " + detailUpdate.PersonId + " " + detailUpdate.TypeId);
                return Ok(ResponseModal<DetailDto>.Success(dto, "Başarılı", true));
            }
            else
            {
                _logger.LogError("Detail Controller'da veri düzenlenemedi." + detailUpdate.Email + " " + detailUpdate.Number + " " + detailUpdate.PersonId + " " + detailUpdate.TypeId);
                return BadRequest(ResponseModal<DetailDto>.Errors(detailUpdate, "Hatalı", false));
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModal<DetailDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _DetailRepository.GetById(id);
            var dto = _mapper.Map<DetailDto>(data);
            // data.IsDeleted = false; Soft Delete 
            if (data == null)
            {
                _logger.LogError("Detail Controller'da böyle bir veri yok");
                return NotFound(ResponseModal<Detail>.Errors(data, "Böyle bir veri yok", false));
            }
            if (_DetailRepository.Delete(id) == true)
            {
                _logger.LogInformation("Detail Controller'da veri silindi.");
                return Ok(ResponseModal<DetailDto>.Success(dto, "Silindi", true));
            }
            else
            {
                _logger.LogError("Detail Controller'da veri silinemedi.");
                return BadRequest(ResponseModal<Detail>.Errors(data, "Hatalı", false));
            }
        }
    }
}
