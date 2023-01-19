using Ardalis.ApiEndpoints;
using Machines.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Machines.Api.Endpoints
{
    
    public class Add : EndpointBaseAsync.WithRequest<Machine>.WithActionResult<Machine>
    {

        private readonly IMachineRepository repository;

        public Add(IMachineRepository repository) => this.repository = repository;

        [Microsoft.AspNetCore.Mvc.HttpPost("api/machines")]
        public override async Task<ActionResult<Machine>> HandleAsync(Machine request, CancellationToken cancellationToken = default)
        {
            await repository.AddAsync(request);

            return Ok(request);
        }
    }
}
