using AlliedTelefonia.Domain;
using AlliedTelefonia.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AlliedTelefonia.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OperadoraController : Controller
	{

		private readonly Telefonia _telefonia;
		public OperadoraController(Telefonia telefonia)
		{
			_telefonia = telefonia;
		}

		[HttpPost("cadastrar")]
		public async Task<IActionResult> CadastrarTelefonia([FromBody] Plano plano)
		{
			try
			{
				
				return Ok(await _telefonia.CadastrarPlanoOperadora(plano));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ToString());
			}

		}

		[HttpPost("atualizar")]
		public async Task<IActionResult> AtualizarTelefonia([FromBody] Plano plano)
		{
			//[FromBody] string pessoa
			try
			{
				return Ok(await _telefonia.AtualizarPlanoOperadora(plano));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ToString());
			}

		}

		[HttpPost("remover")]
		public async Task<IActionResult> RemoverTelefonia([FromBody] Plano plano)
		{
			try
			{
				return Ok(await _telefonia.RemoverPlanoOperadora(plano));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ToString());
			}

		}

		[HttpPost("listarPlano")]
		public async Task<IActionResult> ListarPlano([FromBody] Plano plano)
		{
			try
			{
				return Ok(await _telefonia.ListarPlanos(plano));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ToString());
			}

		}

	}
}
