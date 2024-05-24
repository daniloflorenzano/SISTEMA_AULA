using Microsoft.AspNetCore.Mvc;
using SISTEMA_AULA.MODEL.Models;
using SISTEMA_AULA.MODEL.Services;
using SISTEMA_AULA.MODEL.ViewModel;

namespace SISTEMA_AULA.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
        
    private readonly ServiceCliente _service;

    public ClienteController(DbsistemasContext context)
    {
        _service = new ServiceCliente(context);
    }
        
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.oRepositoryCliente.SelecionarTodosAsync());
    }
    [HttpGet("GetClientesById/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.oRepositoryCliente.SelecionarChaveAsync(id));
    }
        
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ClienteVM clienteVm)
    {
        var cliente = ConvertToCliente(clienteVm);
        await _service.oRepositoryCliente.IncluirAsync(cliente);
        return NoContent();
    }
        
    private Cliente ConvertToCliente(ClienteVM clienteVm)
    {
        return new Cliente
        {
            CliNome = clienteVm.NomeCliente,
            CliCpfcnpj = (clienteVm.CPF ?? clienteVm.CNPJ)!,
            CliDataCadastro = DateTime.Now,
            CliDataNascimento = clienteVm.DataNascimento,
            CliEmail = clienteVm.Email,
            CliNomeMae = clienteVm.NomeMae,
            CliSexo = clienteVm.Sexo,
            CliTelefone = clienteVm.Telefone
        };
    }
    
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ClienteVM clienteVm)
    {
        if (clienteVm.CodigoCliente is null ) 
            return BadRequest("Código do cliente não informado");
        
        var existingClient = await _service.oRepositoryCliente.SelecionarChaveAsync(clienteVm.CodigoCliente);
        if (existingClient is null)
            return NotFound("Cliente não encontrado");
        
        existingClient.CliNome = clienteVm.NomeCliente;
        existingClient.CliCpfcnpj = (clienteVm.CPF ?? clienteVm.CNPJ)!;
        existingClient.CliDataNascimento = clienteVm.DataNascimento;
        existingClient.CliEmail = clienteVm.Email;
        existingClient.CliNomeMae = clienteVm.NomeMae;
        existingClient.CliSexo = clienteVm.Sexo;
        existingClient.CliTelefone = clienteVm.Telefone;
        
        await _service.oRepositoryCliente.AlterarAsync(existingClient);
        
        return NoContent();
    }
}