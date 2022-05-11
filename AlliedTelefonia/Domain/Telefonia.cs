using AlliedTelefonia.Data;
using AlliedTelefonia.Domain.Dto;
using AlliedTelefonia.Domain.Enum;
using AlliedTelefonia.Domain.ModelView;
using Dapper;

namespace AlliedTelefonia.Domain
{
	public class Telefonia
	{
		private readonly Context _context;
		public Telefonia(Context context)
		{
			_context = context;
		}

		#region ModelView
		public async Task<ModelViewPlanoOperadora> CadastrarPlanoOperadora(Plano plano)
		{
			var planoOperadora = new ModelViewPlanoOperadora();
			var taskCadastrar = Cadastrar(plano);

			await taskCadastrar;

			if (taskCadastrar.IsCompleted)
			{
				planoOperadora.Status = 200;
				planoOperadora.Msg = "Plano Cadastrado com sucesso";
			}

			return planoOperadora;
		}

		public async Task<ModelViewPlanoOperadora> AtualizarPlanoOperadora(Plano plano)
		{
			var planoOperadora = new ModelViewPlanoOperadora();
			var taskAtualizacao  = Atualizar(plano);

			await taskAtualizacao;

			if (taskAtualizacao.IsCompleted)
			{
				planoOperadora.Status = 200;
				planoOperadora.Msg = "Plano Atualizado com sucesso";
			}

			return planoOperadora;
		}

		public async Task<ModelViewPlanoOperadora> RemoverPlanoOperadora(Plano plano)
		{
			var planoOperadora = new ModelViewPlanoOperadora();
			var taskRemocao = Remover(plano);

			await taskRemocao;

			if (taskRemocao.IsCompleted)
			{
				planoOperadora.Status = 200;
				planoOperadora.Msg = "Plano Removido com sucesso";
			}

			return planoOperadora;
		}

		public async Task<ModelViewListaDePlanos> ListarPlanos(Plano plano)
		{
			var planosOperadora = new ModelViewListaDePlanos();
			var planos = await LocalizarPlanosOperadora(plano);
			planosOperadora.ListaPlanos = planos;
			return planosOperadora;
		}
		#endregion

		#region CRUD
		private Task Cadastrar(Plano plano)
		{
			return Task.Run(() =>
			{
				try
				{
					using (var con = _context.Connection())
					{
						 con.Execute(@"INSERT INTO PLANO (NR_MINUTO, FL_FRANQUIA_INTERNET, VL_PLANO, NR_TIPO_PLANO, NR_OPERADORA, NR_DDD) VALUES (@Minuto,@FranquiaInternet,@Valor,@TipoPlano,@Operadora,@Ddd)", new 
						 {
							 Minuto = plano.Minutos,
							 FranquiaInternet = plano.FranquiaInternet,
							 Valor = plano.Valor,
							 TipoPlano = (int)plano.Tipo,
							 Operadora = localizarNumeroOperadora(plano.Operadora).NrOperadora,
							 Ddd = localizarNumeroDDD(plano.UF).NrDDD,
						 });
					}
				}
				catch (Exception ex)
				{
					throw new Exception("Erro ao Cadastrar msg: ", ex);
				}
			});
		}

		private Task Atualizar(Plano plano)
		{
			return Task.Run(() =>
			{
				try
				{
					using (var con = _context.Connection())
					{
						con.Execute(@"UPDATE PLANO SET NR_MINUTO = @Minuto, FL_FRANQUIA_INTERNET = @FranquiaInternet, VL_PLANO = @Valor, NR_TIPO_PLANO= @TipoPlano, NR_OPERADORA = @Operadora, NR_DDD = @Ddd WHERE CD_PLANO = @cdPlanoValue", new 
						{
							Minuto = plano.Minutos,
							FranquiaInternet = plano.FranquiaInternet,
							Valor = plano.Valor,
							TipoPlano = (int)plano.Tipo,
							Operadora = localizarNumeroOperadora(plano.Operadora).NrOperadora,
							Ddd = localizarNumeroDDD(plano.UF).NrDDD,
							cdPlanoValue = plano.CodigoPlano
						});
					}
				}
				catch (Exception ex)
				{
					throw new Exception("Erro ao Atualizar msg: ", ex);
				}
			});

		}

		private Task Remover(Plano plano)
		{
			return Task.Run(() =>
			{
				try
				{
					using (var con = _context.Connection())
					{
						con.Execute(@"DELETE FROM PLANO WHERE CD_PLANO = @cdPlanoValue", new 
						{
							cdPlanoValue = plano.CodigoPlano
						});
					}
				}
				catch (Exception ex)
				{
					throw new Exception("Erro ao Remover msg: ", ex);
				}
			});
		}

		#endregion CRUD

		#region List

		private async Task<List<Plano>> LocalizarPlanosOperadora(Plano plano)
		{
			var listaPlanos = new List<Plano>();
			var param = new DynamicParameters();

			var where = "";

			if (plano.Tipo != null && plano.Operadora != null && plano.UF != null)
			{
				where = "WHERE P.NR_TIPO_PLANO = @Value and D.NR_DDD = @DDDValue and O.NR_OPERADORA = @OperadoraValue";
				param.Add("@Value", plano.Tipo);
				param.Add("@DDDValue", localizarNumeroDDD(plano.UF).NrDDD);
				param.Add("@OperadoraValue", localizarNumeroOperadora(plano.Operadora).NrOperadora);

			}
			else
			{
				if (plano.UF != null)
				{
					where = "WHERE D.NR_DDD = @Value ";
					param.Add("@Value", localizarNumeroDDD(plano.UF).NrDDD);
				}
				else if (plano.Operadora != null)
				{
					var yy = localizarNumeroOperadora(plano.Operadora).NrOperadora;
					where = "WHERE O.NR_OPERADORA = @Value ";
					param.Add("@Value", localizarNumeroOperadora(plano.Operadora).NrOperadora);
				}
				else if (plano.Tipo != null)
				{
					where = "WHERE P.NR_TIPO_PLANO = @Value ";
					param.Add("@Value", plano.Tipo);
				}
			}

			var query = @$"SELECT [CD_PLANO] AS CodigoPlano
							  ,P.[NR_MINUTO] AS Minutos
							  ,P.[FL_FRANQUIA_INTERNET] AS FranquiaInternet
							  ,P.[VL_PLANO] AS Valor
							  ,P.[NR_TIPO_PLANO] AS Tipo
							  ,O.NM_OPERADORA AS Operadora
							  ,D.UF as UF
						  FROM
							[PLANO] P
							join OPERADORA O ON O.NR_OPERADORA = P.NR_OPERADORA
							join DDD D ON D.NR_DDD = P.NR_DDD {where}";
			

			using (var con = _context.Connection())
			{
				listaPlanos = con.Query<Plano>(query,param).ToList();
			}

			return await Task.FromResult(listaPlanos);
		}

		#endregion List

		#region Dto Localizar
		public DDD localizarNumeroDDD(string uf)
		{
			using (var con = _context.Connection())
			{
				return con.QueryFirstOrDefault<DDD>(@"SELECT NR_DDD as NrDDD, UF  FROM DDD WHERE UF = @Estado ", new { Estado = uf });
			}
		}
		public Operadora localizarNumeroOperadora(string operadora)
		{
			using (var con = _context.Connection())
			{
				return con.QueryFirstOrDefault<Operadora>(@"SELECT NR_OPERADORA as NrOperadora, NM_OPERADORA as NmOperadora FROM OPERADORA WHERE NM_OPERADORA = @OperadoraValue ", new { OperadoraValue = operadora });
			}
		}
		#endregion Dto Localizar

	}
}
