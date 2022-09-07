using Core.Application.Requests;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Commands.SoftDeleteProgrammingLanguage;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Dtos;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Models;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KodlamaDevs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguagesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguageCommand)
        {
            CreatedProgrammingLanguageDto result = await Mediator.Send(createProgrammingLanguageCommand);

            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new() { PageRequest = pageRequest };
            ProgrammingLanguageListModel result = await Mediator.Send(getListProgrammingLanguageQuery);

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProgrammingLanguageQuery getByIdProgrammingLanguageQuery)
        {
            ProgrammingLanguageGetByIdDto programmingLanguageGetByIdDto = await Mediator.Send(getByIdProgrammingLanguageQuery);

            return Ok(programmingLanguageGetByIdDto);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand)
        {
            UpdatedProgrammingLanguageDto result = await Mediator.Send(updateProgrammingLanguageCommand);

            return Ok(result);
        }

        [HttpPost("harddelete")]
        public async Task<IActionResult> HardDelete([FromBody] DeleteProgrammingLanguageCommand deleteProgrammingLanguageCommand)
        {
            DeletedProgrammingLanguageDto result = await Mediator.Send(deleteProgrammingLanguageCommand);

            return Ok(result);
        }

        [HttpPost("delete")]

        public async Task<IActionResult> Delete([FromBody] SoftDeleteProgrammingLanguageCommand softDeleteProgrammingLanguageCommand)
        {
            SoftDeletedProgrammingLanguageDto result = await Mediator.Send(softDeleteProgrammingLanguageCommand);

            return Ok();
        }
    }
}
