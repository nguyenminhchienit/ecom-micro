﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Domain.Interfaces
{
    public interface IEntityAuditBase<T> : IEntityBase<T>, IAuditable
    {
    }
}
