using Ardalis.ApiEndpoints;
using Machines.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Machines.Api.Endpoints
{
    
    public class GetById : EndpointBaseAsync.WithRequest<int>.WithActionResult<Machine>
    {
        private readonly IMachineRepository repository;

        public GetById(IMachineRepository repository) => this.repository = repository;

        [Microsoft.AspNetCore.Mvc.HttpGet("api/machines/{id}")]
        public override async Task<ActionResult<Machine>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            return await repository.GetByIdAsync(id);
        }
    }
}
