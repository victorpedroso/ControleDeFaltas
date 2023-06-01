using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace controledefaltas
{
    public class Usuario
    {
        private int id;
        private string nome;
        private string email;
        private string senha;
        private int nivelAcesso;

        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Nome { get => nome; set => nome = value; }
        public int NivelAcesso { get => nivelAcesso; set => nivelAcesso = value; }

        public ArrayList ConsultarUsuario()
        {
            ArrayList usuario = new ArrayList();
            try
            {
                string query = "SELECT * FROM usuarios WHERE email = @email AND senha = @senha";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@email", Email);
                command.Parameters.AddWithValue("@senha", Senha);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Usuario user = new Usuario();
                    user.Id = reader.GetInt32("id");
                    user.Nome = reader.GetString("nome");
                    user.Email = reader.GetString("email");
                    user.Senha = reader.GetString("senha");
                    user.NivelAcesso = reader.GetInt32("nivelAcesso");
                    usuario.Add(user);
                }
                reader.Close();
                return usuario;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar usuario: " + ex.Message);
                return null;
            }
        }
        public override string ToString()
        {
            ArrayList atributos = new ArrayList();
            atributos.Add(this.Id);
            return string.Join("", atributos.ToArray());
            //return this.nome.ToString();
        }

    }
}
