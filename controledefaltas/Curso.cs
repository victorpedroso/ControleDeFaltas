using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace controledefaltas
{
    public class Curso
    {
        private int id;
        private string nome;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }

        public void CadastrarCurso()
        {
            ArrayList curso = PesquisarCursoPorNome();
            if(curso.Count > 0)
            {
                MessageBox.Show("Curso já cadastrado!");
            }
            else
            {
                try
                {
                    string query = "INSERT INTO cursos (nome) VALUES (@nome)";
                    MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                    command.Parameters.AddWithValue("@nome", nome);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar curso: " + ex.Message);
                }
            }
        }
        public ArrayList PesquisarCursoPorNome()
        {
            ArrayList cursos = new ArrayList();
            try
            {
                string query = "SELECT * FROM cursos WHERE nome = @nome";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@nome", Nome);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Curso c = new Curso();
                    c.Id = reader.GetInt32("id");
                    c.Nome = reader.GetString("nome");
                    cursos.Add(c);
                }
                reader.Close();
                return cursos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar nomes dos cursos: " + ex.Message);
                return null;
            }
        }

        public ArrayList PesquisarCurso()
        {
            ArrayList cursos = new ArrayList();
            try
            {
                string query = "SELECT * FROM cursos ORDER BY nome";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Curso c = new Curso();
                    c.id = reader.GetInt32("id");
                    c.nome = reader.GetString("nome");
                    cursos.Add(c);
                }
                reader.Close();
                return cursos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar nomes dos cursos: " + ex.Message);
                return null;
            }
        }
        public Curso PesquisarNomeCurso()
        {
            try
            {
                string query = "SELECT * FROM cursos WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();
                Curso curso = null;
                if (reader.Read())
                {
                    string nome = reader.GetString("nome");
                    curso = new Curso();
                    curso.Nome = nome;
                }
                reader.Close();
                return curso;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar cursos: " + ex.Message);
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
            return this.nome.ToString();
        }

    }
}
