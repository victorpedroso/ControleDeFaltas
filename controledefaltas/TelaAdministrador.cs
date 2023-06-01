using MySqlX.XDevAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace controledefaltas
{
    public partial class TelaAdministrador : Form
    {
        public TelaAdministrador()
        {
            InitializeComponent();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string email = txtEmail.Text;
            string matricula = txtMatricula.Text;
            Curso curso = (Curso) cmbCadastrarAlunoCurso.SelectedItem;

            if(nome == "" || email == "" || matricula == "")
            {
                MessageBox.Show("Preencha todos os campos!");
            }
            else
            {
                Aluno a = new Aluno();
                a.Nome = nome;
                a.Email = email;
                a.Matricula = matricula;
                a.Curso = curso;
                ArrayList aluno = a.PesquisarAluno();
                int t = aluno.Count;
                if (t > 0)
                {
                    MessageBox.Show("Aluno já possui cadastro");
                }
                else
                { 
                    a.InserirNovoAluno();
                }
            }
            txtNome.Text = "";
            txtEmail.Text = "";
            txtMatricula.Text = "";
        }

        private void pictureBox8_Click_1(object sender, EventArgs e)
        {
            tbEstudante.SelectedIndex = 1;
        }

        private void tbEstudante_Selected(object sender, TabControlEventArgs e)
        {
        

        }

        private void tbEstudante_Deselected(object sender, TabControlEventArgs e)
        {
            //cmbProcurarAluno_Curso.Items.Clear();
            //cmbProcurarAluno_Professor.Items.Clear();
            cmbMatricularAluno_Disciplinas.Items.Clear();
            cmbCadastrarAlunoCurso.Items.Clear();
            cmbAdicionarNovaDisciplina_Cursos.Items.Clear();
            cmbAdicionarNoveDisciplina_Prof.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string curso = txtNomeCurso.Text;
            if(curso == "")
            {
                MessageBox.Show("Preencha o nome do curso!");
            }
            else
            {
                Curso c  = new Curso();
                c.Nome = curso;
                c.CadastrarCurso();
            }
            txtNomeCurso.Text = "";
        }
        private void tbEstudante_SelectedIndexChanged(object sender, EventArgs e)
        {
            int select = tbEstudante.SelectedIndex;
            if(select == 1)
            {
                AtualizaCadastroCursos();
            }
            if(select == 2) 
            {
                AtualizaProcuraAluno();
            }
            else if(select == 3)
            {
                txtNomeCurso.Text = "";
            }
            else if(select == 4)
            {
                AtualizaListaCursos();
            }
            else if(select == 5)
            {
                AtualizaDisciplinas();
            }
            else if(select == 6)
            {
                AtualizaCursosDisciplina();
                AtualizaProfessorDisciplina();
                cmbAdicionarNovaDisciplina_qtdeHoras.SelectedIndex = 0;
            }
            else if(select == 7)
            {
                ExibirDisciplinas();
            }
            else if(select == 9)
            {
                AtualizaProfessores();
            }
            else if(select == 10)
            {
                AtualizaDisciplinasFaltas();
            }
        }
        private void ExibirDisciplinas()
        {
            Disciplina d = new Disciplina();
            ArrayList disciplinas = d.PesquisarDisciplinasRespCurso();
            DataTable dt = new DataTable();
            dt.Columns.Add("Nome");
            dt.Columns.Add("Professor responsavel");
            dt.Columns.Add("Curso");
            dt.Columns.Add("Quantidade de horas");
            foreach (var disciplina in disciplinas)
            {
                Disciplina dis = (Disciplina)disciplina;
                dt.Rows.Add(dis.Nome, dis.Professor, dis.Curso, dis.QtdeHoras); 
            }
            dataGridViewDisciplinas.DataSource = dt;
            dataGridViewDisciplinas.Columns[dataGridViewDisciplinas.Columns.Count - 1].FillWeight = 1;
            dataGridViewDisciplinas.Columns[dataGridViewDisciplinas.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewDisciplinas.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        private void AtualizaProfessorDisciplina()
        {
            Professor prof = new Professor();
            ArrayList professores = prof.PesquisarProfessores();
            int t = professores.Count;
            for (int i = 0; i < t; i++)
            {
                cmbAdicionarNoveDisciplina_Prof.Items.Add((Professor)professores[i]);
            }
            if (professores.Count > 0)
            {
                cmbAdicionarNoveDisciplina_Prof.SelectedIndex = 0;
            }
            else
            {
                cmbAdicionarNoveDisciplina_Prof.Items.Add("Nenhum professor cadastrado");
            }
        }
        private void AtualizaCursosDisciplina()
        {
            Curso curso = new Curso();
            ArrayList cursos = curso.PesquisarCurso();
            int t = cursos.Count;
            for (int i = 0; i < t; i++)
            {
                cmbAdicionarNovaDisciplina_Cursos.Items.Add((Curso)cursos[i]);
            }
            if (cmbAdicionarNovaDisciplina_Cursos.Items.Count > 0)
            {
                cmbAdicionarNovaDisciplina_Cursos.SelectedIndex = 0;
            }
            else
            {
                cmbAdicionarNovaDisciplina_Cursos.Items.Add("Nenhum curso cadastrado");
            }
        }
        private void AtualizaDisciplinas()
        {
            Disciplina disciplina = new Disciplina();
            ArrayList disciplinas = disciplina.PesquisarDisciplinas();
            int t = disciplinas.Count;
            for (int i = 0; i < t; i++)
            {
                cmbMatricularAluno_Disciplinas.Items.Add((Disciplina)disciplinas[i]);
            }
            /*
            DataTable dt = conexao.PesquisarDisciplinas();
            foreach (DataRow row in dt.Rows)
            {
                cmbMatricularAluno_Disciplinas.Items.Add(row["nome"]);
            }*/
            if (cmbMatricularAluno_Disciplinas.Items.Count != 0)
            {
                cmbMatricularAluno_Disciplinas.SelectedIndex = 0;
            }
            else
            {
                cmbMatricularAluno_Disciplinas.Items.Add("Nenhuma disciplina cadastrada");
                cmbMatricularAluno_Disciplinas.SelectedIndex = 0;
            }
        }
        private void AtualizaProfessores()
        {
            Professor p = new Professor();
            ArrayList professores = p.PesquisarProfessores();
            DataTable dt = new DataTable();
            dt.Columns.Add("Nome");
            dt.Columns.Add("Email");
            dt.Columns.Add("Senha");
            dt.Columns.Add("Registro");
            foreach (var professor in professores)
            {
                Professor prof = (Professor) professor;
                dt.Rows.Add(prof.Nome, prof.Email, prof.Senha, prof.Registro);
            }
            dataGridViewProfessor.DataSource = dt;
            dataGridViewProfessor.Columns[dataGridViewProfessor.Columns.Count - 1].FillWeight = 1;
            dataGridViewProfessor.Columns[dataGridViewProfessor.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewProfessor.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        private void AtualizaListaCursos()
        {
            Curso curso = new Curso();
            ArrayList cursos = curso.PesquisarCurso();
            dataGridViewCursos.DataSource = cursos;
            dataGridViewCursos.Columns[dataGridViewCursos.Columns.Count - 1].FillWeight = 1;
            dataGridViewCursos.Columns[dataGridViewCursos.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCursos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        }
        private void AtualizaCadastroCursos()
        {
            Curso c = new Curso();
            ArrayList cursos = c.PesquisarCurso();
            int t = cursos.Count;

            for(int i =0;i < t; i++)
            {
                cmbCadastrarAlunoCurso.Items.Add((Curso) cursos[i]);
            }
        }
        private void AtualizaDisciplinasFaltas()
        {
            Disciplina disciplina = new Disciplina();
            ArrayList disciplinas = disciplina.PesquisarDisciplinas();
            int t = disciplinas.Count;
            for (int i = 0; i < t; i++)
            {
                cmbConsultarFaltas_Disciplinas.Items.Add((Disciplina)disciplinas[i]);
            }
        }
        private void AtualizaProcuraAluno()
        {
      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nome = txtNomeProf.Text;
            string email = txtEmailProf.Text;
            string senha = txtSenhaProf.Text;
            string registro = txtRegistroProf.Text;
            Professor p = new Professor();
            p.Nome = nome;
            p.Email = email;
            p.Senha = senha;
            p.Registro = registro;
            p.InserirProfessor();
            txtNomeProf.Text = "";
            txtEmailProf.Text = "";
            txtSenhaProf.Text = "";
            txtRegistroProf.Text = "";

        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            ArrayList alunos = new ArrayList();
            string matricula = txtProcurarAluno_Matricula.Text;
            Aluno a = new Aluno();
            a.Matricula = matricula;
            ArrayList aluno = a.PesquisarAluno();
            int t = aluno.Count;
            for(int i = 0; i < t; i++)
            {
                alunos.Add(aluno[i]);
            }
            viewProcurarAluno.DataSource = alunos;
        }

        private void btnCadastrarAlunoDisciplina_Click(object sender, EventArgs e)
        {
            Disciplina disciplina = (Disciplina) cmbMatricularAluno_Disciplinas.SelectedItem;
            Aluno a = new Aluno();
            a.Matricula = txtMatricularAluno_Matricula.Text;
            ArrayList aluno = a.PesquisarAluno();
            Disciplina disc = new Disciplina();
            disc.Id = disciplina.Id;
            string idAluno = aluno[0].ToString();
            disc.IdAluno = Convert.ToInt32(idAluno);
            disc.CadastrarAlunoDisciplina();
            txtMatricularAluno_Matricula.Text = "";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Banco.FecharConexao();
        }
        private void btnCadastrarDisciplina_Click(object sender, EventArgs e)
        {
            string nomeDisciplina = txtNomeDisciplina.Text;
            Professor responsavel = (Professor) cmbAdicionarNoveDisciplina_Prof.SelectedItem;
            Curso curso = (Curso) cmbAdicionarNovaDisciplina_Cursos.SelectedItem;
            string qtdeHoras = cmbAdicionarNovaDisciplina_qtdeHoras.SelectedItem.ToString();
            Disciplina materia = new Disciplina();
            materia.Nome = nomeDisciplina;
            materia.Professor = responsavel;
            materia.Curso = curso;
            materia.QtdeHoras = qtdeHoras;
            materia.CadastrarDisciplina();
            txtNomeDisciplina.Text = "";
            txtNomeDisciplina.Focus();
        }

        private void Administrador_Load(object sender, EventArgs e)
        {

        }

        private void btnImportarProfessor_Click(object sender, EventArgs e)
        {
            string linha = "";
            string[] linhaseparada = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos CSV (*.csv)|*.csv";
            openFileDialog.Title = "Selecionar arquivo CSV";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                StreamReader reader = new StreamReader(filePath, Encoding.UTF8, true);
                try
                {
                    int i = 0;
                    while (true)
                    {
                        linha = reader.ReadLine();
                        if (linha == null) break;
                        if (i == 0)
                        {
                            i++;
                            continue;
                        }
                        linhaseparada = linha.Split(',');
                        string resultado = string.Format(
                        @"Linha - 
                    Campo 1: {0}
                    Campo 2: {1}
                    Campo 3: {2}
                    Campo 4: {3}", linhaseparada[0], linhaseparada[1], linhaseparada[2], linhaseparada[3]);
                        Professor prof = new Professor();
                        prof.Nome = linhaseparada[0];
                        prof.Email = linhaseparada[1];
                        prof.Senha = linhaseparada[2];
                        prof.Registro = linhaseparada[3];
                        prof.InserirProfessor();
                    }
                    MessageBox.Show("Dados importados com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao abrir o arquivo: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Nenhum arquivo selecionado.");
            }
            

        }

        private void btnImportarAluno_Click(object sender, EventArgs e)
        {
            string linha = "";
            string[] linhaseparada = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos CSV (*.csv)|*.csv";
            openFileDialog.Title = "Selecionar arquivo CSV";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                StreamReader reader = new StreamReader(filePath, Encoding.UTF8, true);
                try
                {
                    int i = 0;
                    while (true)
                    {
                        linha = reader.ReadLine();
                        if (linha == null) break;
                        if (i == 0)
                        {
                            i++;
                            continue;
                        }
                        linhaseparada = linha.Split(',');
                        string resultado = string.Format(
                        @"Linha - 
                    Campo 1: {0}
                    Campo 2: {1}
                    Campo 3: {2}
                    Campo 4: {3}", linhaseparada[0], linhaseparada[1], linhaseparada[2], linhaseparada[3]);
                        Aluno aluno = new Aluno();
                        int id = 0;
                        aluno.Nome = linhaseparada[0];
                        aluno.Email = linhaseparada[1];
                        aluno.Matricula = linhaseparada[2];
                        Curso curso = new Curso();
                        curso.Nome = linhaseparada[3];
                        ArrayList array = curso.PesquisarCursoPorNome();
                        foreach (var row in array)
                        {
                            Curso c = (Curso)row;
                            id = c.Id;
                        }
                        aluno.Curso.Id = id;
                        aluno.InserirNovoAluno();
                    }
                    MessageBox.Show("Dados importados com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao abrir o arquivo: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Nenhum arquivo selecionado.");
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            viewProcurarAluno.DataSource = null;

        }

        private void btnConsultarFaltas_Pesquisar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Nome do aluno");
            dt.Columns.Add("Nome da disciplina");
            dt.Columns.Add("Data das faltas");
            dt.Columns.Add("Quantidade de faltas");
            Faltas fal = new Faltas();
            string dataSelecionada = calendariaConsultarFaltas.SelectionRange.Start.ToString("yyyy-MM-dd");
            fal.DataFaltas = dataSelecionada;
            if (cmbConsultarFaltas_Disciplinas.SelectedItem == null)
            { 
                ArrayList faltas = fal.ConsultarFaltas();
                foreach (var falta in faltas)
                {
                    Faltas f = (Faltas)falta;

                    dt.Rows.Add(f.NomeAluno, f.NomeDisciplina, f.DataFaltas, f.QtdeFaltas);
                }

            }
            else
            {
                fal.NomeDisciplina = cmbConsultarFaltas_Disciplinas.SelectedItem.ToString();
                ArrayList faltas = fal.ConsultarFaltasPorDisciplina();
                if(faltas.Count > 0)
                {
                    foreach (var falta in faltas)
                    {
                        Faltas f = (Faltas)falta;
                        dt.Rows.Add(f.NomeAluno, f.NomeDisciplina, f.DataFaltas, f.QtdeFaltas);
                    }
                }
                else
                {
                    MessageBox.Show("Não há faltas cadastradas nessa disciplina para o dia informado");
                }
                
            }
            dataGridViewConsultarFaltas.DataSource = dt;
            dataGridViewConsultarFaltas.Columns[dataGridViewConsultarFaltas.Columns.Count - 1].FillWeight = 1;
            dataGridViewConsultarFaltas.Columns[dataGridViewConsultarFaltas.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewConsultarFaltas.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);



        }
    }
}
