using CapaEntitades;
using CapaNegocio;
using SistemaVentas.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaVentas
{
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            CmbEstado.Items.Add(new OpcionCombo() { Valor = 1 , Texto ="Activo"});
            CmbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Inactivo" });

            CmbEstado.DisplayMember = "Texto";
            CmbEstado.ValueMember = "Valor";
            CmbEstado.SelectedIndex = 0;

            List<Rol> listRol = new CN_Rol().Listar();

            foreach (var rol in listRol) 
            {
                CmbRol.Items.Add(new OpcionCombo() { Valor = rol.IdRol, Texto = rol.Descripcion });
            }
            CmbRol.DisplayMember = "Texto";
            CmbRol.ValueMember = "Valor";
            CmbRol.SelectedIndex = 0;

            foreach (DataGridViewColumn item in dgvData.Columns)
            {
                if (item.Visible == true && item.Name != "BtnSeleccionar")
                {
                    CmbBusqueda.Items.Add(new OpcionCombo() { Valor = item.Name, Texto = item.HeaderText });
                }
            }

            CmbBusqueda.DisplayMember = "Texto";
            CmbBusqueda.ValueMember = "Valor";
            CmbBusqueda.SelectedIndex = 0;

            //Mostrar usuarios
            List<Usuario> lstUsuarios = new CN_Usuario().Listar();
            foreach (var item in lstUsuarios)
            {
                dgvData.Rows.Add(new object[]
                {
                "",
                item.IdUsuario,
                item.Documento,
                item.NombreCompleto,
                item.Correo,
                item.Clave,
                item.oRol.IdRol,
                item.oRol.Descripcion,
                item.Estado == true ? 1 : 0,
                item.Estado == true ?  "Activo" : "Inactivo"

                });
            }

        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Usuario usuario = new Usuario()
            {
                IdUsuario = Convert.ToInt32(TxtIdUsario.Text),
                Documento = TxtDocumento.Text,
                NombreCompleto = TxtNombreCompleto.Text,
                Correo = txtCorreo.Text,
                Clave = TxtClave.Text,
                oRol = new Rol() { IdRol = Convert.ToInt32( ((OpcionCombo)CmbRol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)CmbEstado.SelectedItem).Valor) == 1 ? true : false
            };

            int idUsuarioGenerado = new CN_Usuario().Registrar(usuario, out mensaje);

            if (idUsuarioGenerado != 0)
            {
                dgvData.Rows.Add(new object[]
                {
                    "",
                    TxtIdUsario.Text,
                    TxtDocumento.Text,
                    TxtNombreCompleto.Text,
                    txtCorreo.Text,
                    TxtClave.Text,
                    ((OpcionCombo)CmbRol.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)CmbRol.SelectedItem).Texto.ToString(),
                    ((OpcionCombo)CmbEstado.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)CmbEstado.SelectedItem).Texto.ToString()

                });
            }
            else
            {
                MessageBox.Show(mensaje);
            }
            
            Limpiar();
        }

        private void Limpiar()
        {
            TxtIndice.Text = "-1";
            TxtIdUsario.Text = default;
                TxtDocumento.Text = default;
                TxtNombreCompleto.Text = default;
                txtCorreo.Text = default;
                TxtClave.Text = default;
            TxtConfirmarClave = default;
            CmbRol.SelectedIndex = 0;
            CmbEstado.SelectedIndex = 0;
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) 
                return;
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds,DataGridViewPaintParts.All);

                var w = Properties.Resources.check20.Width;
                var h = Properties.Resources.check20.Height;

                var x = e.CellBounds.Left + (e.CellBounds.Width - w)/2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "BtnSeleccionar")
            {
                int indice = e.RowIndex;

                if(indice >= 0)
                {
                    TxtIndice.Text = indice.ToString();
                    TxtIdUsario.Text = dgvData.Rows[indice].Cells["IdUsuario"].Value.ToString();
                    TxtDocumento.Text = dgvData.Rows[indice].Cells["NroDocumento"].Value.ToString();
                    TxtNombreCompleto.Text = dgvData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtCorreo.Text = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    TxtClave.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();


                    foreach (OpcionCombo oc in CmbRol.Items)
                    { 
                        if((Int32)oc.Valor == Convert.ToInt32(dgvData.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indiceCombo = CmbRol.Items.IndexOf(oc);
                            CmbRol.SelectedIndex = indiceCombo;
                            break;
                        }
                    }

                    foreach (OpcionCombo oc in CmbEstado.Items)
                    {
                        if ((Int32)oc.Valor == Convert.ToInt32(dgvData.Rows[indice].Cells["Estado"].Value))
                        {
                            int indiceCombo = CmbEstado.Items.IndexOf(oc);
                            CmbEstado.SelectedIndex = indiceCombo;
                            break;
                        }
                    }

                }
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {

        }
    }
}
