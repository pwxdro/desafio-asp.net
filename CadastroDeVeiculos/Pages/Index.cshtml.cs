using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CadastroDeVeiculos.Pages
{
	public class IndexModel : PageModel
	{
		public List<VeiculosInfo> ListaVeiculos = new List<VeiculosInfo>();
		public void OnGet()
		{
			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=veiculos;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM veiculos";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								VeiculosInfo veiculosInfo = new VeiculosInfo();
								veiculosInfo.placa = reader.GetString(0);
								veiculosInfo.renavam = "" + reader.GetInt64(1);
								veiculosInfo.nome = reader.GetString(2);
								veiculosInfo.cpf = "" + reader.GetInt64(3);
								ListaVeiculos.Add(veiculosInfo);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("erro: " + ex.Message);
			}
		}

		public class VeiculosInfo
		{
			public String placa;
			public String renavam;
			public String nome;
			public String cpf;
		}
	}
}