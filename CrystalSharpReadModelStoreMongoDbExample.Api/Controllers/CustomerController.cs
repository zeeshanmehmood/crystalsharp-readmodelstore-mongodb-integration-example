﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrystalSharp.Application;
using CrystalSharp.Application.Execution;
using CrystalSharp.Infrastructure.Paging;
using CrystalSharpReadModelStoreMongoDbExample.Api.Dto;
using CrystalSharpReadModelStoreMongoDbExample.Application.Commands;
using CrystalSharpReadModelStoreMongoDbExample.Application.Queries;
using CrystalSharpReadModelStoreMongoDbExample.Application.ReadModels;
using CrystalSharpReadModelStoreMongoDbExample.Application.Responses;

namespace CrystalSharpReadModelStoreMongoDbExample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;

        public CustomerController(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
        }

        [HttpPost]
        [Route("create-customer")]
        public async Task<ActionResult<CommandExecutionResult<CustomerResponse>>> PostCreateCustomer([FromBody] CreateCustomerRequest request)
        {
            CreateCustomerCommand command = new() { Name = request.Name, Address = request.Address };

            return await _commandExecutor.Execute(command, CancellationToken.None).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("orders")]
        public async Task<ActionResult<QueryExecutionResult<PagedResult<CustomerOrderReadModel>>>> GetCustomerOrders([FromQuery] CustomerOrderListQuery query)
        {
            return await _queryExecutor.Execute(query, CancellationToken.None).ConfigureAwait(false);
        }
    }
}
