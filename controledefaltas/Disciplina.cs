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
    public class Disciplina
    {
        private int id;
        private string nome;
        private Professor professor;
        private string horario;
        private Curso curso;
        private string qtdeHoras;
        private string qtdeMatriculados;
        private int idAluno;
        private string dataFaltas;
        private string qtdeFaltas;
        private Aluno aluno;
        public string Nome { get => nome; set => nome = value; }
        public Professor Professor { get => professor; set => professor = value; }
        public string Horario { get => horario; set => horario = value; }
        public Curso Curso { get => curso; set => curso = value; }
        public string QtdeHoras { get => qtdeHoras; set => qtdeHoras = value; }
        public string QtdeMatriculados { get => qtdeMatriculados; set => qtdeMatriculados = value; }
        public int Id { get => id; set => id = value; }
        public int IdAluno { get => idAluno; set => idAluno = value; }
        public string DataFaltas { get => dataFaltas; set => dataFaltas = value; }
        public string QtdeFaltas { get => qtdeFaltas; set => qtdeFaltas = value; }
        public Aluno Aluno { get => aluno; set => aluno = value; }

        public Disciplina()
        {
            this.Curso = new Curso();
            this.Professor = new Professor();
        }

        public ArrayList PesquisarNomeAlunoMatriculado()
        {
            ArrayList alunos = new ArrayList();
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT alunos.nome FROM alunos INNER JOIN alunosmatriculados ON alunosmatriculados.aluno_matricula = alunos.id WHERE alunosmatriculados.disciplina_id  = @idDisciplina";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@idDisciplina", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Disciplina d = new Disciplina();
                    d.Nome = reader.GetString(0);
                    alunos.Add(d);
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
        
        public ArrayList PesquisarDisciplinaPorNome()
        {
            ArrayList disciplina = new ArrayList();
            try
            {
                string query = "SELECT * FROM disciplinas WHERE nome = @nome";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@nome", Nome);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Disciplina d = new Disciplina();
                    d.Id = reader.GetInt32("id");
                    d.Nome = reader.GetString("nome");
                    disciplina.Add(d);
                }
                reader.Close();
                return disciplina;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar disciplina: " + ex.Message);
                return null;
            }
        }

        public ArrayList PegaDisciplinasProfessor()
        {
            ArrayList disciplinas = new ArrayList();
            try
            {
                string query = "SELECT * FROM disciplinas WHERE responsavel = @responsavel";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@responsavel", Professor.Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Disciplina d = new Disciplina();
                    d.Id = reader.GetInt32("id");
                    d.Nome = reader.GetString("nome");
                    disciplinas.Add(d);
                }
                reader.Close();
                return disciplinas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar disciplina: " + ex.Message);
                return null;
            }
        }
        public void CadastrarDisciplina()
        {
            try
            {
                string query = "INSERT INTO disciplinas (nome, responsavel, curso, qtdeHoras) VALUES (@nome, @responsavel, @curso, @qtdeHoras)";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@nome", nome);
                command.Parameters.AddWithValue("@responsavel", Professor.Id);
                command.Parameters.AddWithValue("@curso", Curso.Id);
                command.Parameters.AddWithValue("@qtdeHoras", qtdeHoras);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar curso: " + ex.Message);
            }
        }

        public ArrayList PesquisarDisciplinas()
        {
            ArrayList disciplinas = new ArrayList();
            try
            {
                string query = "SELECT * FROM disciplinas ORDER BY nome";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Disciplina dis = new Disciplina();
                    dis.Id = reader.GetInt32("id");
                    dis.Nome = reader.GetString("nome");
                    Professor p = new Professor();
                    p.Id = reader.GetInt32("responsavel");
                    dis.Professor = p;
                    Curso curso = new Curso();
                    curso.Id = reader.GetInt32("curso");
                    dis.Curso = curso;
                    disciplinas.Add(dis);
                }
                reader.Close();
                return disciplinas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar disciplinas: " + ex.Message);
                return null;
            }
        }
        public ArrayList PesquisarDisciplinasRespCurso()
        {
            ArrayList disciplinas = new ArrayList();
            try
            {
                string query = "SELECT disciplinas.nome AS NomeDisciplina, professor.nome AS NomeProfessor, cursos.nome AS NomeCurso, disciplinas.qtdeHoras AS QtdeHoras FROM disciplinas INNER JOIN professor ON disciplinas.responsavel = professor.id INNER JOIN cursos ON disciplinas.curso = cursos.id;";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Disciplina dis = new Disciplina();
                    dis.Nome = reader.GetString(0);
                    Professor p = new Professor();
                    p.Nome = reader.GetString(1);
                    dis.Professor = p;
                    Curso curso = new Curso();
                    curso.Nome = reader.GetString(2);
                    dis.Curso = curso;
                    dis.qtdeHoras = reader.GetString(3);
                    disciplinas.Add(dis);
                }
                reader.Close();
                return disciplinas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar disciplinas: " + ex.Message);
                return null;
            }
        }
        public void CadastrarAlunoDisciplina()
        {
            ArrayList aluno = PesquisarAlunoMatriculado();
            int t = aluno.Count;
            if(t > 0)
            {
                MessageBox.Show("Aluno já esta cadastrado na disciplina");
            }
            else
            {
                try
                {
                    string query = "INSERT INTO alunosmatriculados (aluno_matricula, disciplina_id) VALUES (@aluno_matricula, @disciplina_id)";
                    MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                    command.Parameters.AddWithValue("@aluno_matricula", idAluno);
                    command.Parameters.AddWithValue("@disciplina_id", id);
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar matricula: " + ex.Message);
                }
            }
            
        }
        public ArrayList PesquisarAlunoMatriculado()
        {
            ArrayList aluno = new ArrayList();
            try
            {
                string query = "SELECT * FROM alunosmatriculados WHERE aluno_matricula = @aluno_matricula AND disciplina_id = @disciplina_id";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@aluno_matricula", idAluno);
                command.Parameters.AddWithValue("@disciplina_id", id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Disciplina disciplina = new Disciplina();
                    disciplina.idAluno = reader.GetInt32("aluno_matricula");
                    disciplina.id = reader.GetInt32("disciplina_id");
                    aluno.Add(disciplina);
                }
                reader.Close();
                return aluno;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar matricula: " + ex.Message);
                return null;
            }
        }
        public ArrayList ConsultarFaltas()
        {
            ArrayList faltas = new ArrayList();
            try
            {
                string query = "SELECT alunos.nome AS NomeAluno, disciplinas.nome AS NomeDisciplina, faltas.dataFalta AS DataFalta, faltas.qtdeFaltas AS QtdeFaltas FROM alunos INNER JOIN faltas ON faltas.aluno_id = alunos.id INNER JOIN disciplinas ON faltas.disciplina_id = disciplinas.id WHERE faltas.dataFalta = @data";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@data", DataFaltas);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Aluno a = new Aluno();
                    Disciplina disciplina = new Disciplina();
                    a.Nome = reader.GetString(0);
                    disciplina.Aluno = a;
                    disciplina.Nome = reader.GetString(1);
                    disciplina.DataFaltas = reader.GetString(2);
                    disciplina.qtdeFaltas = reader.GetString(3);
                    faltas.Add(disciplina);
                }
                reader.Close();
                return faltas;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar matricula: " + ex.Message);
                return null;
            }
        }
        public override string ToString()
        {
            return this.nome.ToString();
        }
    }
}
