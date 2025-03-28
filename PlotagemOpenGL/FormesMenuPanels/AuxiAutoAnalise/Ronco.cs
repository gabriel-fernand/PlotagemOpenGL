using PlotagemOpenGL.auxi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels.AuxiAutoAnalise
{
    public partial class Ronco : Form
    {
        private static string connectionStringConfigBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.configBD};Uid=Admin;Pwd=;";

        public Ronco()
        {
            InitializeComponent();
            carregarDados();
        }
        private void carregarDados()
        {
            var rw = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];

            DurMin.Text = rw["Ronco_Dur_Min_Ev"].ToString();
            DurMax.Text = rw["Ronco_Dur_Max_Ev"].ToString();
            canula.Checked = true;
        }
        private void aplicar_Click(object sender, EventArgs e)
        {
            try
            {
                // Atualiza os valores no DataTable tbl_ParametrosParaAnalisar
                var rw = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];

                rw["Ronco_Dur_Min_Ev"] = DurMin.Text;
                rw["Ronco_Dur_Max_Ev"] = DurMax.Text;

                GlobVar.tbl_ParametrosParaAnalisar.AcceptChanges(); // Confirma as alterações no DataTable

                // Atualiza os valores na tabela do banco de dados
                using (OdbcConnection conn = new OdbcConnection(connectionStringConfigBd))
                {
                    conn.Open();
                    string sql = @"
                        UPDATE tbl_ParametrosParaAnalise 
                        SET 
                        Ronco_Dur_Min_Ev = ?, 
                        Ronco_Dur_Max_Ev = ?";

                    using (OdbcCommand cmd = new OdbcCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ronco_Dur_Min_Ev", DurMin.Text);
                        cmd.Parameters.AddWithValue("@Ronco_Dur_Max_Ev", DurMax.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            MessageBox.Show("Parâmetros atualizados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Nenhum registro foi atualizado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar os parâmetros: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ronco_Load(object sender, EventArgs e)
        {

        }
    }
}
