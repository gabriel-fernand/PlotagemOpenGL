using PlotagemOpenGL.auxi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels.AuxiAutoAnalise
{
    public partial class Apneia : Form
    {
        private static string connectionStringConfigBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.configBD};Uid=Admin;Pwd=;";

        public Apneia()
        {
            InitializeComponent();
            carregarDados();
        }
        private void carregarDados()
        {
            var rw = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];

            limApn.Text = rw["ApHip_LimiarAp"].ToString();
            limHipo.Text = rw["ApHip_LimiarHip"].ToString();
            DurMin.Text = rw["ApHip_DuracaoMin"].ToString();
            Inter.Text = rw["ApHip_IntervMin"].ToString();
            tmJanelBasal.Text = rw["ApHip_Dur_Jan_Basal"].ToString();
            tmJanelaEventos.Text = rw["ApHip_Dur_Jan_Evento"].ToString();
            Fluxo.Checked = true;
        }
        private void aplicar_Click(object sender, EventArgs e)
        {
            try
            {
                // Atualiza os valores no DataTable tbl_ParametrosParaAnalisar
                var rw = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];

                rw["ApHip_LimiarAp"] = limApn.Text;
                rw["ApHip_LimiarHip"] = limHipo.Text;
                rw["ApHip_DuracaoMin"] = DurMin.Text;
                rw["ApHip_IntervMin"] = Inter.Text;
                rw["ApHip_Dur_Jan_Basal"] = tmJanelBasal.Text;
                rw["ApHip_Dur_Jan_Evento"] = tmJanelaEventos.Text;

                GlobVar.tbl_ParametrosParaAnalisar.AcceptChanges(); // Confirma as alterações no DataTable

                // Atualiza os valores na tabela do banco de dados
                string sql = @"
                    UPDATE tbl_ParametrosParaAnalise 
                    SET 
                    ApHip_LimiarAp = ?, 
                    ApHip_LimiarHip = ?, 
                    ApHip_DuracaoMin = ?, 
                    ApHip_IntervMin = ?, 
                    ApHip_Dur_Jan_Basal = ?, 
                    ApHip_Dur_Jan_Evento = ?";

                using (OleDbCommand cmd = new OleDbCommand(sql, GlobVar.ConnectionConfig))
                {
                    cmd.Parameters.AddWithValue("@ApHip_LimiarAp", limApn.Text);
                    cmd.Parameters.AddWithValue("@ApHip_LimiarHip", limHipo.Text);
                    cmd.Parameters.AddWithValue("@ApHip_DuracaoMin", DurMin.Text);
                    cmd.Parameters.AddWithValue("@ApHip_IntervMin", Inter.Text);
                    cmd.Parameters.AddWithValue("@ApHip_Dur_Jan_Basal", tmJanelBasal.Text);
                    cmd.Parameters.AddWithValue("@ApHip_Dur_Jan_Evento", tmJanelaEventos.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        MessageBox.Show("Parâmetros atualizados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Nenhum registro foi atualizado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar os parâmetros: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
