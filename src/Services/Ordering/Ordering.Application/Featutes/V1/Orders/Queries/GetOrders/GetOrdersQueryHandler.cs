﻿using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Featutes.V1.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ApiResult<List<OrderRequest>>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;
        private readonly ILogger _logger;

        public GetOrdersQueryHandler(IMapper mapper, IOrderRepository repository, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(repository));
        }

        private const string MethodName = "GetOrdersQueryHandler";

        public async Task<ApiResult<List<OrderRequest>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {MethodName} - Username: {request.UserName}");

            var orderEntities = await _repository.GetOrdersByUserName(request.UserName);
            var orderList = _mapper.Map<List<OrderRequest>>(orderEntities);

            _logger.Information($"END: {MethodName} - Username: {request.UserName}");

            return new ApiSuccessResult<List<OrderRequest>>(orderList);
        }
    }
}
