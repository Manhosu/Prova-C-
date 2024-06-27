using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
    }

    // Rota: GET /api/clientes/{clienteId}/servicos
    [HttpGet("{clienteId}/servicos")]
    public IActionResult GetServicosContratados(int clienteId)
    {
        // Busca os serviços contratados pelo cliente com o ID especificado
        var servicosContratados = _clienteService.GetServicosContratados(clienteId);

        if (servicosContratados == null || servicosContratados.Count == 0)
        {
            return NotFound("Nenhum serviço contratado encontrado para este cliente");
        }

        return Ok(servicosContratados);
    }
}
