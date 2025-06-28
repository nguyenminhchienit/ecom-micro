﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Featutes.V1.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public long Id { get; private set; }

        public DeleteOrderCommand(long id)
        {
            Id = id;
        }
    }
}
