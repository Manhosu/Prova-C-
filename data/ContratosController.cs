using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/[controller]")]
public class ContratosController : ControllerBase
{
    private readonly ContratosController _contratoService;

    public ContratosController(ContratosController contratoService)
    {
        _contratoService = contratoService ?? throw new ArgumentNullException(nameof(contratoService));
    }

    // Rota: POST /api/contratos
    [HttpPost]
    public IActionResult RegistrarContrato([FromBody] Contrato contrato)
    {
        // Validação simples dos dados recebidos (exemplo)
        if (contrato == null)
        {
            return BadRequest("Dados inválidos para o contrato");
        }

        // Registra o contrato utilizando um serviço adequado
        var contratoRegistrado = _contratoService.RegistrarContrato(contrato);

        // Verifica se o contrato foi registrado com sucesso
        if (contratoRegistrado == null)
        {
            return BadRequest("Não foi possível registrar o contrato");
        }

        return Ok("Contrato registrado com sucesso");
    }
}
