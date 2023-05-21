using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static CadastroDeVeiculos.Pages.IndexModel;

namespace CadastroDeVeiculos.Pages
{

    public class visualizarModel : PageModel
    {
		public VeiculosInfo veiculosInfo = new VeiculosInfo();
		public String erro = "";
		public String sucesso = "";
		public List<VeiculosInfo> ListaVeiculos = new List<VeiculosInfo>();
        
        public void OnGet()
        {
            String placa = Request.Query["placa"];

            try
            {
                String connectionSring = "Data Source=.\\sqlexpress;Initial Catalog=veiculos;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionSring))
                {
                    connection.Open();
                    String sql = "SELECT * FROM veiculos WHERE placa = @placa";

                    using (SqlCommand comando = new SqlCommand(sql, connection))
                    {
                        comando.Parameters.AddWithValue("@placa", placa);

                        using (SqlDataReader reader = comando.ExecuteReader())
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
                erro = ex.Message;
                return;
            }
        }
        public void OnPost()
        {
			

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
