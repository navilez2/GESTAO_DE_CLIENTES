using Microsoft.AspNetCore.Mvc;
using API_DataBase.Models;

namespace API_DataBase.Controllers;
[ApiController]
[Route("[controller]")]
public class SituacaoController : ControllerBase
{
    public SituacaoController() { }

    [HttpGet]
    public IActionResult GetSituacao()
    {
        return Ok(new DBConnection.Controller().SPS_SITUACAO());
    }

    [HttpGet("{id}")]
    public IActionResult GetSituacao(int id)
    {
        try
        {
            IEnumerable<Situacao> situacao = new DBConnection.Controller().SPS_SITUACAO(id);
            if (situacao.Count<Situacao>() < 1) return NotFound();
            return Ok(situacao);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult AddSituacao([FromBody] Situacao situacao)
    {
        try
        {
            new DBConnection.Controller().SPI_SITUACAO(situacao);

            return CreatedAtAction(nameof(GetSituacao), 0, situacao);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut]
    public IActionResult UpdateSituacao([FromBody] Situacao situacao)
    {
        try
        {
            new DBConnection.Controller().SPU_SITUACAO(situacao);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteSituacao(int id)
    {
        try
        {
            new DBConnection.Controller().SPD_SITUACAO(id);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
