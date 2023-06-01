using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controledefaltas
{
    public class Banco
    {
        private static MySqlConnection conexao;

        public static MySqlConnection getConexao()
        {
            if(Banco.conexao == null)
            {
                Banco.conexao = new MySqlConnection("Server=localhost;Database=controledefaltas;Uid=root;Pwd=;");
                Banco.conexao.Open();
            }
            return Banco.conexao;
        }
        public static void FecharConexao()
        {
            Banco.conexao.Close();
        }
    }
}
