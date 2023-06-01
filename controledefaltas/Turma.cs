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
    public class Turma
    {
        private int id;
        private string nome;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }

        public ArrayList PesquisarTurmas()
        {
            ArrayList turmas = new ArrayList();
            try
            {
                string query = "SELECT * FROM turmas ORDER BY nome";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Turma t = new Turma();
                    t.Id = reader.GetInt32("id");
                    t.Nome = reader.GetString("nome");
                    turmas.Add(t);
                }
                reader.Close();
                return turmas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar turmas: " + ex.Message);
                return null;
            }
        }
        public ArrayList PesquisarTurmaPorNome()
        {
            ArrayList turmas = new ArrayList();
            try
            {
                string query = "SELECT * FROM turmas WHERE nome = @nome";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@nome", Nome);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Turma t = new Turma();
                    t.Id = reader.GetInt32("id");
                    t.Nome = reader.GetString("nome");
                    turmas.Add(t);
                }
                reader.Close();
                return turmas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar turmas: " + ex.Message);
                return null;
            }
        }
        public override string ToString()
        {
            /*ArrayList atributos = new ArrayList();
            atributos.Add(this.Id);
            atributos.Add(this.Nome);
            return string.Join("", atributos.ToArray());
            */
            return this.Id.ToString();
        }
    }
}
