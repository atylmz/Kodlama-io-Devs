using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Domain.Entities
{
    public class ProgrammingLanguage : Entity
    {
        public string Name { get; set; }
        
        
        public ProgrammingLanguage()
        {
        }

        public ProgrammingLanguage(int id, string name,bool isActive = true, DateTime createdDate = default, DateTime modifiedDate = default)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }
    }
}
