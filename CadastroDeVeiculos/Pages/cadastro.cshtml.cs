using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CadastroDeVeiculos.Pages
{
    public class cadastroModel : PageModel
    {
        public IndexModel.VeiculosInfo veiculosInfo = new IndexModel.VeiculosInfo();
        public String erro = "";
        public String sucesso = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            veiculosInfo.placa = Request.Form["placa"];
            veiculosInfo.renavam = Request.Form["renavam"];
            veiculosInfo.nome = Request.Form["nome"];
            veiculosInfo.cpf = Request.Form["cpf"];

            if (veiculosInfo.placa.Length == 0 | veiculosInfo.renavam.Length == 0 | veiculosInfo.nome.Length == 0 | veiculosInfo.cpf.Length == 0)
            {
                erro = "Todos os campos são obrigatorios!";
                return;
            }

            //salvar informações

            try
            {
                String connectionSring = "Data Source=.\\sqlexpress;Initial Catalog=veiculos;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionSring))
                {
                    connection.Open();
                    String sql = "INSERT INTO veiculos" + "(placa, renavam, nome, cpf) VALUES" + "(@placa, @renavam, @nome, @cpf);";

                    using (SqlCommand comando = new SqlCommand(sql, connection))
                    {
                        comando.Parameters.AddWithValue("@placa", veiculosInfo.placa);
                        comando.Parameters.AddWithValue("@renavam", veiculosInfo.renavam);
                        comando.Parameters.AddWithValue("@nome", veiculosInfo.nome);
                        comando.Parameters.AddWithValue("@cpf", veiculosInfo.cpf);

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return;
            }

            veiculosInfo.placa = "";
            veiculosInfo.renavam = "";
            veiculosInfo.nome = "";
            veiculosInfo.cpf = "";
            sucesso = "Veiculo cadastrado!";
        }
    }
}
