using CapaEntitades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            List<Usuario> Test = new CN_Usuario().Listar();

            Usuario oUsuario = new CN_Usuario().Listar()
                .Where( u => u.Documento == TxtDocumento.Text && u.Clave == TxtClave.Text).FirstOrDefault();

            if(oUsuario != null)
            {

                Inicio form = new Inicio(oUsuario);
                
                form.Show();
                this.Hide();

                form.FormClosing += Frm_Closing;
            }
            else
            {
                MessageBox.Show("No se encontro el usuario","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

            
        }


        private void Frm_Closing(object sender, FormClosingEventArgs e) 
        { 
            TxtDocumento.Text = string.Empty;
            TxtClave.Text = string.Empty;
            this.Show();
        }
    }
}
