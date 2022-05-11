using AlliedTelefonia.Domain.Enum;

namespace AlliedTelefonia.Domain.Dto
{
	public class Plano
	{
		public int CodigoPlano { get; set; }
		public int Minutos { get; set; }
		public bool FranquiaInternet { get; set; }
		public decimal Valor { get; set; }
		public TipoPlanoEnum Tipo { get; set; }
		public string? Operadora { get; set; }
		public string? UF { get; set; }

	}
}
