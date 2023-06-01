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
    public class Professor
    {
        private int id;
        private string nome;
        private string email;
        private string senha;
        private string registro;

        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Registro { get => registro; set => registro = value; }
        public int Id { get => id; set => id = value; }

        public ArrayList PesquisarProfessores()
        {
            ArrayList professor = new ArrayList();
            try
            {
                string query = "SELECT * FROM professor ORDER BY nome";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Professor p = new Professor();
                    p.Id = reader.GetInt32("id");
                    p.Nome = reader.GetString("nome");
                    p.Email = reader.GetString("email");
                    p.Senha = reader.GetString("senha");
                    p.Registro = reader.GetString("registro");
                    professor.Add(p);
                }
                reader.Close();
                return professor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message);
                return null;
            }
        }

        public ArrayList PegaNomeProfessor()
        {
            ArrayList professor = new ArrayList();
            try
            {
                string query = "SELECT * FROM professor ORDER BY nome";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@registro", registro);
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Professor p = new Professor();
                    p.nome = reader.GetString("nome");
                    professor.Add(p);
                }
                reader.Close();
                return professor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message);
                return null;
            }
        }
        public ArrayList PesquisaProfessorEmailMatricula()
        {
            ArrayList professor = new ArrayList();
            try
            {
                string query = "SELECT * FROM professor WHERE email = @email AND registro = @registro";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@email", Email);
                command.Parameters.AddWithValue("@registro", Registro);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Professor p = new Professor();
                    p.id = reader.GetInt32("id");
                    p.nome = reader.GetString("nome");
                    professor.Add(p);
                }
                reader.Close();
                return professor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message);
                return null;
            }
        }
        public ArrayList PesquisaProfessorPorId()
        {
            ArrayList professor = new ArrayList();
            try
            {
                string query = "SELECT * FROM professor WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Professor p = new Professor();
                    p.id = reader.GetInt32("id");
                    p.nome = reader.GetString("nome");
                    p.email = reader.GetString("email");
                    p.Senha = reader.GetString("senha");
                    p.registro = reader.GetString("registro");
                    professor.Add(p);
                }
                reader.Close();
                return professor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message);
                return null;
            }
        }
        public ArrayList PesquisaProfessorEmailSenha()
        {
            ArrayList professor = new ArrayList();
            try
            {
                string query = "SELECT * FROM professor WHERE email = @email AND senha = @senha";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@email", Email);
                command.Parameters.AddWithValue("@senha", Senha);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Professor p = new Professor();
                    p.id = reader.GetInt32("id");
                    p.nome = reader.GetString("nome");
                    professor.Add(p);
                }
                reader.Close();
                return professor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message);
                return null;
            }
        }
        public void InserirProfessor()
        {
            ArrayList prof = PesquisaProfessorEmailMatricula();
            int t = prof.Count;
            if(t > 0)
            {
                MessageBox.Show("Professor ja possui cadastro");
            }
            else
            {
                try
                {
                    string query = "INSERT INTO professor (nome, email, senha, registro) VALUES (@nome, @email, @senha, @registro)";
                    MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                    command.Parameters.AddWithValue("@nome", Nome);
                    command.Parameters.AddWithValue("@email", Email);
                    command.Parameters.AddWithValue("@senha", Senha);
                    command.Parameters.AddWithValue("@registro", Registro);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro no banco de dados: " + ex.Message);
                }
            }
        }
        public override string ToString()
        {
            /*ArrayList atributos = new ArrayList();
            atributos.Add(this.Id);
            atributos.Add(this.Nome);
            atributos.Add(this.Email);
            atributos.Add(this.Senha);
            atributos.Add(this.Registro);
            return string.Join(", ", atributos.ToArray());
            */
            return this.nome.ToString();
        }
    }
}
