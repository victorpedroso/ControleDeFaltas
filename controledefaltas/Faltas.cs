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
    public class Faltas
    {
        private string nomeAluno;
        private string nomeDisciplina;
        private string dataFaltas;
        private string qtdeFaltas;
        private Disciplina disciplina;
        private int turma;
        private int mesFalta;
        private string anoFalta;
        private Aluno aluno;

        public string NomeAluno { get => nomeAluno; set => nomeAluno = value; }
        public string NomeDisciplina { get => nomeDisciplina; set => nomeDisciplina = value; }
        public string DataFaltas { get => dataFaltas; set => dataFaltas = value; }
        public string QtdeFaltas { get => qtdeFaltas; set => qtdeFaltas = value; }
        public Disciplina Disciplina { get => disciplina; set => disciplina = value; }
        public int Turma { get => turma; set => turma = value; }
        public int MesFalta { get => mesFalta; set => mesFalta = value; }
        public string AnoFalta { get => anoFalta; set => anoFalta = value; }
        public Aluno Aluno { get => aluno; set => aluno = value; }

        public Faltas()
        {
            this.Disciplina = new Disciplina();
            this.Aluno = new Aluno();
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
                    Faltas f = new Faltas();
                    f.NomeAluno = reader.GetString(0);
                    f.NomeDisciplina = reader.GetString(1);
                    f.DataFaltas = reader.GetString(2);
                    f.QtdeFaltas = reader.GetString(3);
                    faltas.Add(f);
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
        public ArrayList ConsultarFaltasPorDisciplina()
        {
            ArrayList faltas = new ArrayList();
            try
            {
                string query = "SELECT alunos.nome AS NomeAluno, disciplinas.nome AS NomeDisciplina, faltas.dataFalta AS DataFalta, faltas.qtdeFaltas AS QtdeFaltas FROM alunos INNER JOIN faltas ON faltas.aluno_id = alunos.id INNER JOIN disciplinas ON faltas.disciplina_id = disciplinas.id WHERE faltas.dataFalta = @data AND disciplinas.nome = @nome";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@data", DataFaltas);
                command.Parameters.AddWithValue("@nome", NomeDisciplina);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Faltas f = new Faltas();
                    f.NomeAluno = reader.GetString(0);
                    f.NomeDisciplina = reader.GetString(1);
                    f.DataFaltas = reader.GetString(2);
                    f.QtdeFaltas = reader.GetString(3);
                    faltas.Add(f);
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

        public ArrayList GerarRelatorioFaltas()
        {
            ArrayList faltas = new ArrayList();
            try
            {
                string query = "SELECT alunos.nome AS NomeAluno, disciplinas.nome AS NomeDisciplina, faltas.dataFalta AS DataFalta, faltas.qtdeFaltas AS QtdeFaltas FROM alunos INNER JOIN faltas ON faltas.aluno_id = alunos.id INNER JOIN disciplinas ON faltas.disciplina_id = disciplinas.id WHERE month(faltas.dataFalta) = @mes AND year(faltas.dataFalta) = @ano AND alunos.turma = @turma;";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@mes", MesFalta);
                command.Parameters.AddWithValue("@ano", AnoFalta);
                command.Parameters.AddWithValue("turma", Turma);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Faltas f = new Faltas();
                    f.NomeAluno = reader.GetString(0);
                    f.NomeDisciplina = reader.GetString(1);
                    f.DataFaltas = reader.GetString(2);
                    f.QtdeFaltas = reader.GetString(3);
                    faltas.Add(f);
                }
                reader.Close();
                return faltas;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar faltas: " + ex.Message);
                return null;
            }
        }
        public ArrayList GerarHistograma()
        {
            ArrayList faltas = new ArrayList();
            try
            {
                string query = "SELECT DATE(faltas.dataFalta) AS dia, SUM(faltas.qtdeFaltas) AS totalFaltas FROM faltas INNER JOIN alunos ON faltas.aluno_id = alunos.id WHERE alunos.turma = @turma AND MONTH(faltas.dataFalta) = @mes AND YEAR(faltas.dataFalta) = @ano GROUP BY dia ORDER BY dia ASC;";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("turma", Turma);
                command.Parameters.AddWithValue("@mes", MesFalta);
                command.Parameters.AddWithValue("@ano", AnoFalta);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Faltas f = new Faltas();
                    f.DataFaltas = reader.GetString(0);
                    f.QtdeFaltas = reader.GetString(1);
                    faltas.Add(f);
                }
                reader.Close();
                return faltas;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar faltas: " + ex.Message);
                return null;
            }
        }
        public void InsereFaltasAlunos()
        {
            
            try
            {
                string query = "INSERT INTO faltas (aluno_id, disciplina_id, dataFalta, qtdeFaltas) VALUES (@aluno_id, @disciplina_id, @dataFalta, @qtdeFaltas)";
                MySqlCommand command = new MySqlCommand(query, Banco.getConexao());
                command.Parameters.AddWithValue("@aluno_id", Aluno.Id);
                command.Parameters.AddWithValue("@disciplina_id", Disciplina.Id);
                command.Parameters.AddWithValue("@dataFalta", DataFaltas);
                command.Parameters.AddWithValue("@qtdeFaltas", QtdeFaltas);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir faltas: " + ex.Message);
            }
        }
    }
}
