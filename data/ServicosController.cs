using Microsoft.AspNetCore.Mvc;
using System;
namespace ServicosController.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicosController : ControllerBase
{
    private readonly ServicosController _servicoService;

    public ServicosController(ServicosController servicoService)
    {
        _servicoService = servicoService ?? throw new ArgumentNullException(nameof(servicoService));
    }

    // Rota: GET /api/servicos/{id}
    [HttpGet("{id}")]
    public IActionResult GetServicoById(int id)
    {
        var servico = _servicoService.GetServicoById(id);

        if (servico == null)
        {
            return NotFound("Serviço não encontrado");
        }

        return Ok(servico);
    }
}
