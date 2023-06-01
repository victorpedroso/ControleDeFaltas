using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace controledefaltas
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            relogio.Interval = 1000;
            relogio.Tick += new EventHandler(relogio_tick);
            relogio.Start();
        }
        private void relogio_tick(object sender, EventArgs e)
        {
            lblRelogio.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();
            user.Email = txtLogin.Text;
            user.Senha = txtSenha.Text;
            ArrayList usuarios = user.ConsultarUsuario();
            if (usuarios != null)
            {
                int id = 0;
                int nivelAcesso = 0;
                string nome = "";
                string email = "";
                string senha = "";
                foreach (Usuario usuario in usuarios)
                {
                    id = usuario.Id;
                    nome = usuario.Nome;
                    email = usuario.Email;
                    senha = usuario.Senha;
                    nivelAcesso = usuario.NivelAcesso;
                }
                if(nivelAcesso == 0)
                {
                    Professor p = new Professor();
                    p.Email = email;
                    p.Senha = senha;
                    ArrayList array = p.PesquisaProfessorEmailSenha();
                    foreach (Professor prof in array)
                    {
                        id = prof.Id;
                        nome = prof.Nome;
                    }
                    TelaProfessor tela = new TelaProfessor(id);
                    tela.ShowDialog();
                    this.Close();
                }
                else
                {
                    TelaAdministrador tela = new TelaAdministrador();
                    tela.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Usuario ou senha inválidos");
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
