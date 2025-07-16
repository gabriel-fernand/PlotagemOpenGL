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
    public partial class Dessaturacao : Form
    {
        private static string connectionStringConfigBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.configBD};Uid=Admin;Pwd=;";

        public Dessaturacao()
        {
            InitializeComponent();
            carregarDados();
        }
        private void carregarDados()
        {
            var rw = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];

            quedaSup.Text = rw["Sat_QuedaAbaixoDe"].ToString();
            limRec.Text = rw["Sat_Recalcular"].ToString();
            Desprezar.Text = rw["Sat_DesprezarAbaixo"].ToString();
        }

        private void aplicar_Click(object sender, EventArgs e)
        {
            try
            {
                // Atualiza os valores no DataTable tbl_ParametrosParaAnalisar
                var rw = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];

                rw["Sat_QuedaAbaixoDe"] = quedaSup.Text;
                rw["Sat_Recalcular"] = limRec.Text;
                rw["Sat_DesprezarAbaixo"] = Desprezar.Text;

                GlobVar.tbl_ParametrosParaAnalisar.AcceptChanges(); // Confirma as alterações no DataTable

                // Atualiza os valores na tabela do banco de dados
                string sql = @"
                    UPDATE tbl_ParametrosParaAnalise 
                    SET 
                    Sat_QuedaAbaixoDe = ?, 
                    Sat_Recalcular = ?, 
                    Sat_DesprezarAbaixo = ?";

                using (OleDbCommand cmd = new OleDbCommand(sql, GlobVar.ConnectionConfig))
                {
                    cmd.Parameters.AddWithValue("@Sat_QuedaAbaixoDe", quedaSup.Text);
                    cmd.Parameters.AddWithValue("@Sat_Recalcular", limRec.Text);
                    cmd.Parameters.AddWithValue("@Sat_DesprezarAbaixo", Desprezar.Text);

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
