using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KodlamaDevs.Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task ProgrammingLanguageCanNotBeDublicateWhenInserted(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(p => p.Name == name);
            if (result.Items.Any()) throw new BusinessException("ProgrammingLanguage name already exists");
        }

        public async Task ProgrammingLanguageCanNotBeDublicateWhenUpdated(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(p => p.Name == name);
            if (result.Items.Any()) throw new BusinessException("ProgrammingLanguage name already exists");
        }

        public async Task ProgrammingLanguageHasToBeExistWhenUpdated(int id)
        {
            ProgrammingLanguage? result = await _programmingLanguageRepository.GetAsync(p => p.Id == id);
            if (result == null) throw new BusinessException("ProgrammingLanguage does not exist");
        }
        public async Task ProgrammingLanguageHasToBeExistWhenDeleted(int id)
        {
            ProgrammingLanguage? result = await _programmingLanguageRepository.GetAsync(p => p.Id == id);
            if (result == null) throw new BusinessException("ProgrammingLanguage does not exist");
        }
        public async Task ProgrammingLanguageHasToBeExistWhenSoftDeleted(int id)
        {
            ProgrammingLanguage? result = await _programmingLanguageRepository.GetAsync(p => p.Id == id);
            if (result == null) throw new BusinessException("ProgrammingLanguage does not exist");
        }
    }
}
