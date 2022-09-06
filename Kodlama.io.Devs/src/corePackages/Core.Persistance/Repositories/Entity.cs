using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories
{
    public class Entity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        //This one will be added after configurate users.
        //public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        //This one will be added after configurate users.
        //public int ModifiedBy { get; set; }

        public Entity()
        {

        }
        //Initial parameters given because these properties not obligatory at the beginning.
        public Entity(int id, DateTime createdDate = default, DateTime modifiedDate = default, bool isActive = true) : this()
        {
            Id = id;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            IsActive = isActive;
        }
    }
}
