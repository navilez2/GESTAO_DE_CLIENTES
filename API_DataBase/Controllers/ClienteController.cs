using Microsoft.AspNetCore.Mvc;
using API_DataBase.Models;

namespace API_DataBase.Controllers;
[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    public ClienteController() { }

    [HttpGet]
    public IActionResult GetClientes()
    {
        return Ok(new DBConnection.Controller().SPS_CLIENTE());
    }

    [HttpGet("{id}")]
    public IActionResult GetClientes(int id)
    {
        try
        {
            IEnumerable<Cliente> cliente = new DBConnection.Controller().SPS_CLIENTE(id);
            if (cliente.Count<Cliente>() < 1) return NotFound();
            return Ok(cliente);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult AddCliente([FromBody] Cliente cliente)
    {
        try
        {
            new DBConnection.Controller().SPI_CLIENTE(cliente);

            return CreatedAtAction(nameof(GetClientes), 0, cliente);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut]
    public IActionResult UpdateCliente([FromBody] Cliente cliente)
    {
        try
        {
            new DBConnection.Controller().SPU_CLIENTE(cliente);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteCliente(int id)
    {
        try
        {
            new DBConnection.Controller().SPD_CLIENTE(id);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}
