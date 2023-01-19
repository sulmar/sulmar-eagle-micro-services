public class GetAll : EndpointWithoutRequest<IEnumerable<Machine>>
{
    private IMachineRepository repository;
    
    public GetAll(IMachineRepository repository)
    {
        this.repository = repository;
    }

    public override void Configure()
    {
        Get("/api/machines");
        AllowAnonymous();
    }

    public  override async Task HandleAsync(CancellationToken ct)
    {
        var machines = await repository.GetAllAsync();

        await SendAsync(machines);
    }

   
}