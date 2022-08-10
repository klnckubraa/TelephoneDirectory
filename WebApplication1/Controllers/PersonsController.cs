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

    public class PersonsController : Controller
    {
        private readonly IPersonRepository _PersonRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonsController> _logger;

        public PersonsController(IPersonRepository personRepository, IMapper mapper, ILogger<PersonsController> logger)
        {
            _PersonRepository = personRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        [HttpGet("GetAll")]

        public IActionResult GetAll()
        {
            var persons = _PersonRepository.GetAll();
            if (persons == null)
            {
                _logger.LogError("Person Controller'da listeleme yapılamadı.");
                return NotFound();
            }
            var PersonDto = _mapper.Map<List<PersonDto>>(persons.ToList());
            _logger.LogInformation("Person Controller'da listelendi.");
            return Ok(PersonDto);
        }



        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var persons = _PersonRepository.GetById(id);
            if (persons == null)
            {
                _logger.LogError("Person Controller'da böyle bir veri bulunamadı.");
                return NotFound("Böyle bir veri yok.");
            }
            var PersonsDto = _mapper.Map<PersonDto>(persons);
            _logger.LogInformation("Person Controller'da veri çekildi.");
            return Ok(PersonsDto);
        }


        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModal<PersonDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]

        [HttpPost("Create")]
        public IActionResult Create([FromBody] PersonCreateDto personCreate)
        {
            var data = _mapper.Map<Person>(personCreate);

            if (_PersonRepository.Create(data))
            {
                var dto = _mapper.Map<PersonDto>(data);
                _logger.LogInformation("Person Controller'da veri eklendi." + personCreate.Name + " " + personCreate.Surname + " ");
                return Ok(ResponseModal<PersonDto>.Success(dto, "Başarılı", true));
            }
            else
            {
                _logger.LogError("Person Controller'da veri eklenemedi." + personCreate.Name + " " + personCreate.Surname + " ");
                return BadRequest(ResponseModal<PersonCreateDto>.Errors(personCreate, "Hatalı", false));
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModal<PersonDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]

        [HttpPut("Edit")]
        public IActionResult Edit([FromBody] PersonDto personUpdate)
        {
            var data = _mapper.Map<Person>(personUpdate);

            if (_PersonRepository.Update(data))
            {
                var dto = _mapper.Map<PersonDto>(data);
                _logger.LogInformation("Person Controller'da veri düzenlendi." + personUpdate.Name + " " + personUpdate.Surname + " " + personUpdate.Id);
                return Ok(ResponseModal<PersonDto>.Success(dto, "Başarılı", true));
            }
            else
            {
                _logger.LogError("Person Controller'da veri düzenlenemedi." + personUpdate.Name + " " + personUpdate.Surname + " " + personUpdate.Id);
                return BadRequest(ResponseModal<PersonDto>.Errors(personUpdate, "Böyle bir veri yok", false));
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModal<PersonDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _PersonRepository.GetById(id);
            var dto = _mapper.Map<PersonDto>(data);

            if (data == null)
            {
                _logger.LogError("Person Controller'da böyle bir veri yok");
                return NotFound(ResponseModal<Person>.Errors(data, "Böyle bir veri yok", false));
            }

            else if (_PersonRepository.Delete(id) == true)
            {
                _logger.LogInformation("Person Controller'da veri silindi.");
                return Ok(ResponseModal<PersonDto>.Success(dto, "Silindi", true));
            }

            else
            {
                _logger.LogError("Person Controller'da veri silinemedi.");
                return BadRequest(ResponseModal<Person>.Errors(data, "Hatalı", false));
            }
        }
    }
}