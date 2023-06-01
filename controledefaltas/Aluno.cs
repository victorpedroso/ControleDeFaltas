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
    public class Aluno
    {
        private int id;
        private string nome;
        private string email;
        private string matricula;
        private Curso curso;
        private Disciplina disciplina;

        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
        public string Matricula { get => matricula; set => matricula = value; }
        public Curso Curso { get => curso; set => curso = value; }
        public int Id { get => id; set => id = value; }
        public Disciplina Disciplina { get => disciplina; set => disciplina = value; }

        public Aluno()
        {
            this.Curso = new Curso();
            this.Disciplina = new Disciplina();
        }
        public ArrayList PesquisarNomesAlunoMatriculados()
        {
            ArrayList alunos = new ArrayList();
            try
            {
                string query = "SELECT alunos.nome FROM alunos INNER JOIN alunosmatriculados ON alunosmatriculados.aluno_matricula = alunos.id WHERE alunosmatriculados.disciplina_id  = @idDisciplina";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@idDisciplina", Disciplina.Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Aluno a = new Aluno();
                    a.Nome = reader.GetString("nome");
                    alunos.Add(a);
                }
                reader.Close();
                return alunos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar disciplina: " + ex.Message);
                return null;
            }
        }
        public void InserirNovoAluno()
        {
            try
            {
                string query = "INSERT INTO alunos (nome, email, matricula, curso) VALUES (@nome, @email, @matricula, @curso)";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@matricula", matricula);
                    command.Parameters.AddWithValue("@curso", Curso.Id);
                    command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar aluno: " + ex.Message);
            }
        }
        public ArrayList PesquisarAluno()
        {
            ArrayList alunos = new ArrayList();
            try
            {
                string query = "SELECT alunos.*, cursos.* FROM alunos INNER JOIN cursos ON alunos.curso = cursos.id WHERE matricula = @matricula";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@matricula", matricula);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Aluno c = new Aluno();
                    c.Id = reader.GetInt32(0);
                    c.Nome = reader.GetString(1);
                    c.Email = reader.GetString(2);
                    c.Matricula = reader.GetString(3);
                    Curso curso = new Curso();
                    curso.Id = reader.GetInt32(4);
                    curso.Nome = reader.GetString(6);
                    c.Curso = curso;
                    alunos.Add(c);
                }
                reader.Close();
                return alunos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message);
                return null;
            }
        }
        public ArrayList PesquisarAlunoPorNome()
        {
            ArrayList alunos = new ArrayList();
            try
            {
                string query = "SELECT * FROM alunos WHERE nome = @nome";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@nome", Nome);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Aluno c = new Aluno();
                    c.Id = reader.GetInt32(0);
                    //c.Nome = reader.GetString(1);
                    //c.Email = reader.GetString(2);
                    //c.Matricula = reader.GetString(3);
                    //Curso curso = new Curso();
                    //curso.Id = reader.GetInt32(4);
                    //curso.Nome = reader.GetString(6);
                    //c.Curso = curso;
                    alunos.Add(c);
                }
                reader.Close();
                return alunos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no banco de dados: " + ex.Message);
                return null;
            }
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
