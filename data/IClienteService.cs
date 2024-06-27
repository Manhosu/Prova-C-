public interface IClienteService
{
    List<ServicoContratado> GetServicosContratados(int clienteId);
}

public class ClienteService : IClienteService
{
    private readonly ApplicationDbContext _context;

    public ClienteService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public List<ServicoContratado> GetServicosContratados(int clienteId)
    {
        var servicosContratados = _context.ServicosContratados
                                          .Where(sc => sc.ClienteId == clienteId)
                                          .ToList();
        return servicosContratados;
    }
}
