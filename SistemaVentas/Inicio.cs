using CapaEntitades;
using CapaNegocio;
using FontAwesome.Sharp;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace SistemaVentas
{
    public partial class Inicio : Form
    {
        private static Usuario _user;
        private static IconMenuItem _menuActivo = default;
        private static Form _formularioActivo = default;

        public Inicio(Usuario user = null)
        {
            if (user == null) 
            { 
                user = new Usuario() 
                { 
                    NombreCompleto = "Admin predefinido",
                    IdUsuario = 1

                };
            }
            _user = user;

            InitializeComponent();
        }

        private void Inicio_Load(object sender, System.EventArgs e)
        {
            List<Permiso> permisos = new CN_Permiso().Listar(_user.IdUsuario);

            foreach (IconMenuItem item in menuStrip1.Items)
            {
                bool encontrado = permisos.Any(m => m.NombreMenu == item.Name);

                if (encontrado == false)
                {
                    item.Visible = false;
                }
            }
            LblUsuario.Text = _user.NombreCompleto;
        }

        private void AbrirFormulario(IconMenuItem menu, Form frm)
        {
            if (_menuActivo != null) 
            { 
                _menuActivo.BackColor = Color.White;
            }
            menu.BackColor = Color.Silver;
            _menuActivo = menu;

            if(_formularioActivo != null)
            {
                _formularioActivo.Close();
            }

            _formularioActivo = frm;
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.BackColor= Color.SteelBlue;
            
            Contenedor.Controls.Add(frm);
            frm.Show();
        }

        private void MenuUsuario_Click(object sender, System.EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender,new FrmUsuarios());
        }

        private void SubMenuCategoria_Click(object sender, System.EventArgs e)
        {
            AbrirFormulario(menumantenedor, new FrmCategoria());
        }

        private void SubMenuProducto_Click(object sender, System.EventArgs e)
        {
            AbrirFormulario(menumantenedor, new FrmProducto());
        }

        private void SubMenuRegistrar_Click(object sender, System.EventArgs e)
        {
            AbrirFormulario(menuventas, new FrmVentas());
        }

        private void SubMenuDetalleVenta_Click(object sender, System.EventArgs e)
        {
            AbrirFormulario(menuventas,new FrmDetalleVenta());
        }

        private void SubMenuRegistrarCompra_Click(object sender, System.EventArgs e)
        {
            AbrirFormulario(menucompras, new FrmCompras());
        }

        private void SubMenuDetalleCompra_Click(object sender, System.EventArgs e)
        {
            AbrirFormulario(menucompras,new FrmDetalleCompra());
        }

        private void MenuClientes_Click(object sender, System.EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FrmClientes());
        }

        private void MenuProveedores_Click(object sender, System.EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FrmProveedores());
        }

        private void MenuReportes_Click(object sender, System.EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FrmReportes());
        }


    }
}
