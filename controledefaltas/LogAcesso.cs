using Aspose.Pdf;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;

namespace controledefaltas
{
    public class LogAcesso
    {
        private string nomeUsuario;
        private string tipoAcesso;

        public string NomeUsuario { get => nomeUsuario; set => nomeUsuario = value; }
        public string TipoAcesso { get => tipoAcesso; set => tipoAcesso = value; }

        public void CadastrarLog()
        {
            try
            {
                string query = "INSERT INTO logacesso (nome_usuario, horaAcesso, tipoAcesso) VALUES (@nome, @hora, @tipo)";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@nome", NomeUsuario);
                command.Parameters.AddWithValue("@hora", DateTime.Now);
                command.Parameters.AddWithValue("@tipo", TipoAcesso);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message);
            }
        }
    }
}
