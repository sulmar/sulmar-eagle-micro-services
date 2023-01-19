public class Add : Endpoint<Machine, Machine>
{
    private readonly IMachineRepository repository;

    public Add(IMachineRepository repository) => this.repository = repository;

    public override void Configure()
    {
        Post("/api/customers");        
        AllowAnonymous();        
    }
    
    public override async Task HandleAsync(Machine req, CancellationToken ct)
    {
        await repository.AddAsync(req);         
        
        await SendCreatedAtAsync<GetById>(new { id = req.Id}, req);
    }
}
