using AlliedTelefonia.Controllers;
using AlliedTelefonia.Data;
using AlliedTelefonia.Domain;
using AlliedTelefonia.Domain.Dto;
using AlliedTelefonia.Domain.Enum;
using AlliedTelefonia.Domain.Manager;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace AlliedTelefoniaTest
{
	[TestClass]
	public class TesteTelefonia
	{

		[TestMethod]
		public void CadastroControllerTestMethod()
		{
			var context = new Mock<Context>();
			var telefonia = new Mock<Telefonia>();
			var controller = new Mock<OperadoraController>();

			//var mockTelefonia = new Telefonia(tt.Object);
			var plano = new Plano
			{
				UF = "SP"
			};

			

			//var controller = new OperadoraController(mockTelefonia);

			//var result = controller.ListarPlano(plano).Result;


			// arrange
			//var tt = _operadoraController.CadastrarTelefonia(new Plano
			//{
			//	CodigoPlano = 1,
			//	Minutos = 150,
			//	FranquiaInternet = true,
			//	Valor = 110,
			//	Tipo = TipoPlanoEnum.Controle,
			//	Operadora = "TIM",
			//	UF = "SP"
			//}).Result;

			// act

			// assert  
			//Assert.AreEqual("", result);

		}

		[TestMethod]
		public void AtualizacaoControllerTestMethod()
		{
			// arrange


			// act
	

			// assert  
		}

		[TestMethod]
		public void RemocaoControllerTestMethod()
		{
			// arrange


			// act


			// assert  
		}

		[TestMethod]
		public void ListarPlanoControllerTestMethod()
		{
			// arrange
		



			// act

			// assert  
		}

	}
}