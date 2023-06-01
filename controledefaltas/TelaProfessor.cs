using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using Aspose.Pdf;

namespace controledefaltas
{
    public partial class TelaProfessor : Form
    {
        private int idProfessor;
        private DataTable faltas = new DataTable();
        private DataTable alunos = new DataTable();
        public TelaProfessor(int id)
        {
            InitializeComponent();
            idProfessor = id;
            relogio.Interval = 1000;
            relogio.Tick += new EventHandler(relogio_tick);
            relogio.Start();
        }
        private void relogio_tick(object sender, EventArgs e)
        {
            lblRelogio.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Professor_Load(object sender, EventArgs e)
        {
            Professor prof = new Professor();
            prof.Id = idProfessor;
            ArrayList dados = prof.PesquisaProfessorPorId();
            string nome = "";
            foreach (var row in dados)
            {
                Professor p = (Professor) row;
                nome = p.Nome;
            }
            LogAcesso log = new LogAcesso();
            lblNome.Text = nome;
            log.NomeUsuario = nome;
            log.TipoAcesso = "ENTRADA";
            log.CadastrarLog();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int select = tbProfessor.SelectedIndex;
            if(select == 1) 
            {
                AtualizaListaDisciplinas();
            }
            else if(select == 2)
            {
                cmbExportarRelatorio_Ano.SelectedIndex = 0;
                cmbExportarRelatorio_Formato.SelectedIndex = 1;
            }
            else if(select == 3)
            {
                Disciplina d = new Disciplina();
                d.Professor.Id = idProfessor;
                ArrayList disciplinas = d.PegaDisciplinasProfessor();
                if(disciplinas == null)
                {
                    cmbAlunosMatriculados.Items.Add("Nenhuma disciplina cadastrada");
                }
                else
                {
                    foreach (var row in disciplinas)
                    {
                        Disciplina disc = (Disciplina)row;
                        cmbAlunosMatriculados.Items.Add(disc.Nome);
                    }
                }
                cmbAlunosMatriculados.SelectedIndex = 0;
            }
            else if(select == 4)
            {
                AtualizaListaTurmas();
            }
            else if(select == 5)
            {
                AtualizaListaTurmasHistograma();
            }
        }
        private void AtualizaListaTurmasHistograma()
        {
            Turma t = new Turma();
            ArrayList turmas = t.PesquisarTurmas();
            foreach (var row in turmas)
            {
                Turma turma = (Turma)row;
                cmbTurmaHistograma.Items.Add(turma.Nome);
            }
        }
        private void AtualizaListaTurmas()
        {
            Turma t = new Turma();
            ArrayList turmas = t.PesquisarTurmas();
            foreach (var row in turmas)
            {
                Turma turma = (Turma) row;
                cmbExportarRelatorio_Turma.Items.Add(turma.Nome);
            }
        }
        private void AtualizaAlunosParaFaltas()
        {
            string nome = cmbDisciplinas.SelectedItem.ToString();
            int idDisciplina = 0;
            Disciplina dis = new Disciplina();
            dis.Nome = nome;
            ArrayList disciplina = dis.PesquisarDisciplinaPorNome();
            foreach (var row in disciplina)
            {
                Disciplina d = (Disciplina) row;
                idDisciplina = d.Id;
            }
            Aluno aluno = new Aluno();
            aluno.Disciplina.Id = idDisciplina;
            ArrayList alunos = aluno.PesquisarNomesAlunoMatriculados();
            foreach (var row in alunos)
            {
                Aluno a = (Aluno) row;
                dataGridViewFaltas.Rows.Add(a.Nome);
            }
            //DataTable dt = conexao.PesquisaIndiceDisciplina(nome);
            //int idDisciplina = (int) dt.Rows[0]["id"];
            //idDisciplinaFalta = idDisciplina;
            //alunos = conexao.PesquisarNomeAlunoMatriculado(idDisciplina);
            //foreach (DataRow row in alunos.Rows)
            //{
            //    dataGridViewFaltas.Rows.Add(row["nome"]);
            //}
            dataGridViewFaltas.Columns[dataGridViewFaltas.Columns.Count - 1].FillWeight = 1;
            dataGridViewFaltas.Columns[dataGridViewFaltas.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewFaltas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        }
        private void ConfigurarGrade()
        {
            if (dataGridViewFaltas.Columns.Count == 0)
            {
                dataGridViewFaltas.Columns.Add("nome", "nome");
                dataGridViewFaltas.Columns["nome"].Width = 100;

                var opcao = new DataGridViewComboBoxColumn();
                opcao.Name = "Quantidade de faltas";
                opcao.HeaderText = "Quantidade de faltas";
                opcao.Width = 100;
                dataGridViewFaltas.Columns.Add(opcao);

                ObterFaltas();
                foreach (DataRow row in faltas.Rows)
                {
                    opcao.Items.Add(row["Faltas"]);
                }
                dataGridViewFaltas.RowsAdded += DataGridViewFaltas_RowsAdded;
            }
        }
        private void DataGridViewFaltas_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var comboBoxCell = dataGridViewFaltas.Rows[e.RowIndex].Cells["Quantidade de faltas"] as DataGridViewComboBoxCell;
            if (comboBoxCell != null)
            {
                comboBoxCell.Value = "0";
            }
        }
        private void ObterFaltas()
        {
            faltas.Rows.Clear();
            faltas.Columns.Clear();
            faltas.Columns.Add("Faltas", typeof(string));
            faltas.Rows.Add("0");
            faltas.Rows.Add("1");
            faltas.Rows.Add("2");
            faltas.Rows.Add("3");
            faltas.Rows.Add("4");
        }
        private void AtualizaAlunosMatriculados()
        {
            int idDisciplina = 0;
            string nomeDisciplina = cmbAlunosMatriculados.SelectedItem.ToString();
            Disciplina dis = new Disciplina();
            dis.Nome = nomeDisciplina;
            ArrayList array = dis.PesquisarDisciplinaPorNome();
            foreach (var row in array)
            {
                Disciplina d = (Disciplina)row;
                idDisciplina = d.Id;
            }
            dis.Id = idDisciplina;
            Disciplina disciplina = new Disciplina();
            disciplina.Id = idDisciplina;
            array = disciplina.PesquisarNomeAlunoMatriculado();
            DataTable dt = new DataTable();
            dt.Columns.Add("Nome aluno");
            foreach (var linha in array)
            {
                Disciplina f = (Disciplina)linha;
                DataRow row = dt.NewRow();
                row["Nome aluno"] = f.Nome;
                dt.Rows.Add(row);
            }
            //DataTable dt = conexao.PesquisaIndiceDisciplina(nomeDisciplina);
            //int idDisciplina = (int) dt.Rows[0]["id"];
            //dt = conexao.PesquisarNomeAlunoMatriculado(idDisciplina);
            dataGridViewAlunosMatriculados.DataSource = dt;
            dataGridViewAlunosMatriculados.Columns[dataGridViewAlunosMatriculados.Columns.Count - 1].FillWeight = 1;
            dataGridViewAlunosMatriculados.Columns[dataGridViewAlunosMatriculados.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewAlunosMatriculados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        }
        private void AtualizaListaDisciplinas()
        {
            Disciplina d = new Disciplina();
            d.Professor.Id = idProfessor;
            ArrayList disciplinas = d.PegaDisciplinasProfessor();
            foreach (var row in disciplinas)
            {
                Disciplina dis = (Disciplina)row;
                cmbDisciplinas.Items.Add(dis.Nome);
            }
            //int id = (int) dtG.Rows[0]["id"];
            //DataTable dt = conexao.PesquisaDisciplinasDoProfessor(id);
            //foreach (DataRow row in dt.Rows)
            //{
            //    cmbDisciplinas.Items.Add(row["nome"]);
            //}
            if (cmbDisciplinas.Items.Count != 0)
            {
                cmbDisciplinas.SelectedIndex = 0;
            }
            else
            {
                cmbDisciplinas.Items.Add("Nenhuma disciplina cadastrada");
                cmbDisciplinas.SelectedIndex = 0;
            }
        }

        private void tbProfessor_Deselected(object sender, TabControlEventArgs e)
        {
            cmbDisciplinas.Items.Clear();
            cmbAlunosMatriculados.Items.Clear();
            cmbExportarRelatorio_Turma.Items.Clear();
            cmbTurmaHistograma.Items.Clear();
        }

        private void cmbAlunosMatriculados_SelectedValueChanged(object sender, EventArgs e)
        {
            AtualizaAlunosMatriculados();
        }

        private void cmbDisciplinas_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridViewFaltas.Rows.Clear();
            ConfigurarGrade();
            AtualizaAlunosParaFaltas();
        }

        private void btnLancarFaltas_Click(object sender, EventArgs e)
        {
            int idAluno = 0;
            int idDisciplina = 0;
            for (int i = 0; i < dataGridViewFaltas.Rows.Count -1; i++)
            {
                string nome = dataGridViewFaltas.Rows[i].Cells[0].Value.ToString();
                Aluno aluno = new Aluno();
                aluno.Nome = nome;
                ArrayList dadosAluno = aluno.PesquisarAlunoPorNome();
                foreach (var row in dadosAluno)
                {
                    Aluno a = (Aluno) row;
                    idAluno = a.Id;
                }
                Disciplina dis = new Disciplina();
                dis.Nome = cmbDisciplinas.SelectedItem.ToString();
                ArrayList dadosDisciplina = dis.PesquisarDisciplinaPorNome();
                foreach (var row in dadosDisciplina)
                {
                    Disciplina d = (Disciplina) row;
                    idDisciplina = d.Id;
                }
                DateTime selectedDate = monthCalendar1.SelectionStart;
                string formattedDate = selectedDate.ToString("yyyy-MM-dd");
                string faltas = dataGridViewFaltas.Rows[i].Cells[1].Value.ToString();
                Faltas falta = new Faltas();
                falta.Aluno.Id = idAluno;
                falta.Disciplina.Id = idDisciplina;
                falta.DataFaltas = formattedDate;
                falta.QtdeFaltas = faltas;
                falta.InsereFaltasAlunos();
            }

        }

        private void btnPesquisarFaltas_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar2.SelectionStart;
            string formattedDate = selectedDate.ToString("yyyy-MM-dd");
            Faltas falta = new Faltas();
            falta.DataFaltas = formattedDate;
            ArrayList faltas = falta.ConsultarFaltas();
            DataTable dt = new DataTable();
            dt.Columns.Add("Nome aluno");
            dt.Columns.Add("Disciplina");
            dt.Columns.Add("Data da falta");
            dt.Columns.Add("Quantidade de faltas");
            foreach (var linha in faltas)
            {
                Faltas f = (Faltas)linha;
                DataRow row = dt.NewRow();
                string[] data = f.DataFaltas.Split(' ');
                row["Nome aluno"] = f.NomeAluno;
                row["Disciplina"] = f.NomeDisciplina;
                row["Data da falta"] = data[0];
                row["Quantidade de faltas"] = f.QtdeFaltas;
                dt.Rows.Add(row);
            }
            if (dt.Rows.Count > 0)
            {
                dataGridViewConsultarFaltas.DataSource = dt;
                dataGridViewConsultarFaltas.Columns[dataGridViewConsultarFaltas.Columns.Count - 1].FillWeight = 1;
                dataGridViewConsultarFaltas.Columns[dataGridViewConsultarFaltas.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewConsultarFaltas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridViewConsultarFaltas.Visible = true;
            }
            else
            {
                dataGridViewConsultarFaltas.Visible = false;
                MessageBox.Show("Não existem faltas para a data selecionada!");
            }

        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            tbProfessor.SelectedIndex = 1;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            string data = DateTime.Now.ToString();
           // conexao.CadastraLogAcesso(nomeProfessor, 0, data, "SAIDA");
            Dispose();
        }
        private void btnGerarRelatório_Click(object sender, EventArgs e)
        {
            string ano = cmbExportarRelatorio_Ano.SelectedItem.ToString();
            int mes = cmbExportarRelatorio_Mes.SelectedIndex + 1;
            string mes1 = "";
            if(mes <= 9)
            {
                mes1 = "0" + mes;
            }
            else
            {
                mes1 = "1" + mes;
            }
            string data = ano + "-" + mes1;
            //DataTable dt = conexao.GerarRelatorio(data);
            //GerarPDF(dt);
        }
        public void GerarPDF(DataTable dt)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Arquivos PDF|*.pdf";
            saveFileDialog.Title = "Salvar arquivo PDF";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                using (PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create)))
                {
                    document.Open();
                    PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);
                    string[] columnNames = { "Nome aluno", "Disciplina", "Data da falta", "Quantidade de faltas" };
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        pdfTable.AddCell(columnNames[i]);
                    }
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        for (int column = 0; column < dt.Columns.Count; column++)
                        {
                            pdfTable.AddCell(dt.Rows[row][column].ToString());
                            
                        }
                    }
                    document.Add(pdfTable);

                    document.Close();

                    MessageBox.Show("Arquivo PDF gerado com sucesso!");
                }
            }
        }
        private void btnGerarRelatório_Click_1(object sender, EventArgs e)
        {
            Turma t = new Turma();
            Faltas falta = new Faltas();
            t.Nome = cmbExportarRelatorio_Turma.SelectedItem.ToString();
            ArrayList turma = t.PesquisarTurmaPorNome();
            int idTurma = 0;
            foreach (var linha in turma)
            {
                Turma x = (Turma)linha;
                idTurma = x.Id;
            }
            falta.AnoFalta = cmbExportarRelatorio_Ano.SelectedItem.ToString();
            falta.MesFalta = cmbExportarRelatorio_Mes.SelectedIndex + 1;
            falta.Turma = idTurma;
            ArrayList faltasTurma = falta.GerarRelatorioFaltas();
            DataTable dt = new DataTable();
            dt.Columns.Add("Nome aluno");
            dt.Columns.Add("Disciplina");
            dt.Columns.Add("Data da falta");
            dt.Columns.Add("Quantidade de faltas");
            foreach (var linha in faltasTurma)
            {
                Faltas f = (Faltas)linha;
                DataRow row = dt.NewRow();
                row["Nome aluno"] = f.NomeAluno;
                row["Disciplina"] = f.NomeDisciplina;
                row["Data da falta"] = f.DataFaltas;
                row["Quantidade de faltas"] = f.QtdeFaltas;
                dt.Rows.Add(row);
            }
            GerarPDF(dt);
        }

        private void btnGerarHistograma_Click(object sender, EventArgs e)
        {
            Faltas faltas = new Faltas();
            Turma turma = new Turma();
            turma.Nome = cmbTurmaHistograma.SelectedItem.ToString();
            ArrayList turmas = turma.PesquisarTurmaPorNome();
            int idTurma = 0;
            foreach (var linha in turmas)
            {
                Turma t = (Turma)linha;
                idTurma = t.Id;
            }
            faltas.AnoFalta = cmbAnoHistograma.SelectedItem.ToString();
            faltas.MesFalta = cmbMesHistograma.SelectedIndex + 1;
            faltas.Turma = idTurma;
            ArrayList resultado = faltas.GerarHistograma();
            DataTable dt = new DataTable();
            dt.Columns.Add("Dia");
            dt.Columns.Add("Total de faltas");
            foreach (var linha in resultado)
            {
                Faltas f = (Faltas)linha;
                DataRow row = dt.NewRow();
                row["Dia"] = f.DataFaltas;
                row["Total de faltas"] = f.QtdeFaltas;
                dt.Rows.Add(row);
            }
            Chart histogramaFaltas = new Chart();
            histogramaFaltas.Series.Add(new Series("Total de faltas"));
            histogramaFaltas.Series["Total de faltas"].ChartType = SeriesChartType.Column;

            foreach (DataRow linha in dt.Rows)
            {
                DateTime dataFalta = Convert.ToDateTime(linha["Dia"]);
                int qtdeFaltas = Convert.ToInt32(linha["Total de faltas"]);
                histogramaFaltas.Series["Total de faltas"].Points.AddXY(dataFalta.Day, qtdeFaltas);
            }

            ChartArea chartarea = new ChartArea();
            chartarea.AxisX.MajorGrid.Enabled = false;
            chartarea.AxisX.Title = "Dias";
            chartarea.AxisX.TitleFont = new System.Drawing.Font("Arial", 12f);
            histogramaFaltas.ChartAreas.Add(chartarea);

            histogramaFaltas.ChartAreas[0].AxisY.Title = "Quantidade de faltas";
            histogramaFaltas.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12f);
            Form form = new Form();
            form.Controls.Add(histogramaFaltas);
            histogramaFaltas.Dock = DockStyle.Fill;
            form.WindowState = FormWindowState.Maximized;
            form.ShowDialog();
        }
    }
}
