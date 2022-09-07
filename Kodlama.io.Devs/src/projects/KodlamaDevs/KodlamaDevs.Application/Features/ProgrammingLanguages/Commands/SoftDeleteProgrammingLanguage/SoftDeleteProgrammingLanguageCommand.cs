using AutoMapper;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Dtos;
using KodlamaDevs.Application.Features.ProgrammingLanguages.Rules;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Commands.SoftDeleteProgrammingLanguage
{
    public class SoftDeleteProgrammingLanguageCommand : IRequest<SoftDeletedProgrammingLanguageDto>
    {
        public int Id { get; set; }

        public class SoftDeleteProgrammingLanguageCommandHandler : IRequestHandler<SoftDeleteProgrammingLanguageCommand, SoftDeletedProgrammingLanguageDto>
        {
            private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
            private readonly IMapper _mapper;
            private readonly ProgrammingLanguageRules _programmingLanguageRules;

            public SoftDeleteProgrammingLanguageCommandHandler(ProgrammingLanguageRules programmingLanguageRules, IMapper mapper, IProgrammingLanguageRepository programmingLanguageRepository)
            {
                _programmingLanguageRules = programmingLanguageRules;
                _mapper = mapper;
                _programmingLanguageRepository = programmingLanguageRepository;
            }

            public async Task<SoftDeletedProgrammingLanguageDto> Handle(SoftDeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _programmingLanguageRules.ProgrammingLanguageHasToBeExistWhenDeleted(request.Id);

                ProgrammingLanguage programmingLanguage = _mapper.Map<ProgrammingLanguage>(request);
                ProgrammingLanguage softDeletedProgrammingLanguage = await _programmingLanguageRepository.SoftDeleteAsync(programmingLanguage);
                SoftDeletedProgrammingLanguageDto softDeletedProgrammingLanguageDto = _mapper.Map<SoftDeletedProgrammingLanguageDto>(softDeletedProgrammingLanguage);

                return softDeletedProgrammingLanguageDto;
            }
        }
    }
}
