using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class MontagemForm : Form
    {
        public static ToolTip NomesCanais = new ToolTip();

        public MontagemForm()
        {
            InitializeComponent();
            ConfigurarTooltips(panelImagem); // Configura os tooltips para todos os botões do painel principal
            NomesCanais.SetToolTip(EOE, "EOE");

        }


        private void ConfigurarTooltips(Control container)
        {
            foreach (Control controle in container.Controls)
            {
                if (controle is RadioButton botao)
                {
                    // Adiciona os eventos para exibir e esconder o tooltip
                    botao.MouseEnter += (s, e) => MostrarTooltip(botao);
                    botao.MouseLeave += (s, e) => EsconderTooltip();
                }
            }
        }
        private void MostrarTooltip(RadioButton botao)
        {
            // Configura o conteúdo e a posição do tooltip
            NomesCanais.Show(botao.Name, botao, 0, botao.Height); // Dura 2 segundos
        }
        private void EsconderTooltip()
        {
            NomesCanais.Hide(panelImagem); // Esconde o tooltip do painel principal
        }


    }
}
