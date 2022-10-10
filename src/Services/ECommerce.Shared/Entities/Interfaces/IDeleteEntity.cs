using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Entities.Interfaces
{
    public interface IDeleteEntity : IEntityBase
    {
        bool IsDeleted { get; }

        void Delete();

        void UnDelete();
    }
}