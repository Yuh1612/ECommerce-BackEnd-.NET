using ECommerce.Shared.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Entities.Base
{
    public class AuditAndDeleteEntity : AuditEntity, IDeleteEntity
    {
        public bool IsDeleted { get; protected set; }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void UnDelete()
        {
            IsDeleted = false;
        }
    }
}