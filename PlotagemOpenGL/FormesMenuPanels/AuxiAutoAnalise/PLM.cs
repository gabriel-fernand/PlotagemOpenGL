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
    public partial class PLM : Form
    {
        private static string connectionStringConfigBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.configBD};Uid=Admin;Pwd=;";

        public PLM()
        {
            InitializeComponent();
            carregarDados();
        }
        private void carregarDados()
        {
            var rw = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];

            DurMin.Text = rw["PLM_Dur_Min_Ev"].ToString();
            DurMax.Text = rw["PLM_Dur_Max_Ev"].ToString();
            Inter.Text = rw["PLM_Interv_Min_Entre_Ev"].ToString();
            DisMinEntreInicioDosEventosPPLM.Text = rw["PLM_Distancia_Min"].ToString();
            DistMaxEntreEventosPPLM.Text = rw["PLM_Distancia_Max"].ToString();
            qtdMinParaPLM.Text = rw["PLM_Qtd_Min_Para_Ser_PLM"].ToString();
            tmJanelBasal.Text = rw["PLM_Dur_Jan_Basal"].ToString();
            tmJanEventos.Text = rw["PLM_Dur_Jan_Evento"].ToString();
            NumEventomaiorBasal.Text = rw["PLM_Num_Vezes_Amplitude_Basal"].ToString();
            amplBasal.Text = rw["PLM_Fator_Amplitude"].ToString();
            FtMultAmplEst.Text = rw["PLM_Fator_Mult_Ampl_Estagio"].ToString();
            DetecEventEstZero.Checked = Convert.ToBoolean(rw["PLM_Est_0"]);
            NumVezesMaiorAmpliBasal.Text = "4";
            numVezesMaiorJanelaEventos.Text = "4";

            basal.Checked = true;
            PnAmbas.Checked = true;
        }
        private void aplicar_Click(object sender, EventArgs e)
        {
            try
            {
                // Atualiza os valores no DataTable tbl_ParametrosParaAnalisar
                var rw = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];

                rw["PLM_Dur_Min_Ev"] = DurMin.Text;
                rw["PLM_Dur_Max_Ev"] = DurMax.Text;
                rw["PLM_Interv_Min_Entre_Ev"] = Inter.Text;
                rw["PLM_Distancia_Min"] = DisMinEntreInicioDosEventosPPLM.Text;
                rw["PLM_Distancia_Max"] = DistMaxEntreEventosPPLM.Text;
                rw["PLM_Qtd_Min_Para_Ser_PLM"] = qtdMinParaPLM.Text;
                rw["PLM_Dur_Jan_Basal"] = tmJanelBasal.Text;
                rw["PLM_Dur_Jan_Evento"] = tmJanEventos.Text;
                rw["PLM_Num_Vezes_Amplitude_Basal"] = NumEventomaiorBasal.Text;
                rw["PLM_Fator_Amplitude"] = amplBasal.Text;
                rw["PLM_Fator_Mult_Ampl_Estagio"] = FtMultAmplEst.Text;
                rw["PLM_Est_0"] = DetecEventEstZero.Checked;

                GlobVar.tbl_ParametrosParaAnalisar.AcceptChanges(); // Confirma as alterações no DataTable

                // Atualiza os valores na tabela do banco de dados
                string sql = @"
                    UPDATE tbl_ParametrosParaAnalise 
                    SET 
                    PLM_Dur_Min_Ev = ?, 
                    PLM_Dur_Max_Ev = ?, 
                    PLM_Interv_Min_Entre_Ev = ?, 
                    PLM_Distancia_Min = ?, 
                    PLM_Distancia_Max = ?,
                    PLM_Qtd_Min_Para_Ser_PLM = ?,
                    PLM_Dur_Jan_Basal = ?,
                    PLM_Dur_Jan_Evento = ?,
                    PLM_Num_Vezes_Amplitude_Basal = ?,
                    PLM_Fator_Amplitude = ?,
                    PLM_Fator_Mult_Ampl_Estagio = ?,
                    PLM_Est_0 = ?,
                    ApHip_Dur_Jan_Basal = ?, 
                    ApHip_Dur_Jan_Evento = ?";

                using (OleDbCommand cmd = new OleDbCommand(sql, GlobVar.ConnectionConfig))
                {
                    cmd.Parameters.AddWithValue("@PLM_Dur_Min_Ev", DurMin.Text);
                    cmd.Parameters.AddWithValue("@PLM_Dur_Max_Ev", DurMax.Text);
                    cmd.Parameters.AddWithValue("@PLM_Interv_Min_Entre_Ev", Inter.Text);
                    cmd.Parameters.AddWithValue("@PLM_Distancia_Min", DisMinEntreInicioDosEventosPPLM.Text);
                    cmd.Parameters.AddWithValue("@PLM_Distancia_Max", DistMaxEntreEventosPPLM.Text);
                    cmd.Parameters.AddWithValue("@PLM_Qtd_Min_Para_Ser_PLM", qtdMinParaPLM);
                    cmd.Parameters.AddWithValue("@PLM_Dur_Jan_Basal", tmJanelBasal.Text);
                    cmd.Parameters.AddWithValue("@PLM_Dur_Jan_Evento", tmJanEventos.Text);
                    cmd.Parameters.AddWithValue("@PLM_Num_Vezes_Amplitude_Basal", NumEventomaiorBasal.Text);
                    cmd.Parameters.AddWithValue("@PLM_Fator_Amplitude", amplBasal.Text);
                    cmd.Parameters.AddWithValue("@PLM_Fator_Mult_Ampl_Estagio", FtMultAmplEst.Text);
                    cmd.Parameters.AddWithValue("@PLM_Est_0", DetecEventEstZero.Checked);

                    cmd.Parameters.AddWithValue("@ApHip_Dur_Jan_Basal", tmJanelBasal.Text);
                    cmd.Parameters.AddWithValue("@ApHip_Dur_Jan_Evento", qtdMinParaPLM.Text);

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
