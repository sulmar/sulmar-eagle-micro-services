
public record GetMachineRequest
{
    public int Id { get; set; }
}

public class GetById : Endpoint<GetMachineRequest, Machine>
{
    private IMachineRepository repository;

    public GetById(IMachineRepository repository) => this.repository = repository;

    public override void Configure()
    {
        Get("/api/machines/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetMachineRequest req, CancellationToken ct)
    {
        var machine = await repository.GetByIdAsync(req.Id);

        await SendAsync(machine);
    }
}
