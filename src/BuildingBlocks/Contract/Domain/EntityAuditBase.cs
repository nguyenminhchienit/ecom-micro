﻿using Contract.Common.Interfaces;
using Contract.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Domain
{
    public abstract class EntityAuditBase<T> : EntityBase<T>, IEntityAuditBase<T>
    {
        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}
