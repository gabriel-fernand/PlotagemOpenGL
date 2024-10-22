using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels
{
    partial class PagSelecionadasImpressao
    {
        private DataGridView dataGridViewPaginas;
        private ListBox listBoxArquivos;
        private Button btnSelecionarTudo, btnImprimir, btnVisualizar, btnExcluir, btnFechar;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewPaginas = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            listBoxArquivos = new ListBox();
            btnSelecionarTudo = new Button();
            btnImprimir = new Button();
            btnVisualizar = new Button();
            btnExcluir = new Button();
            btnFechar = new Button();
            label2 = new Label();
            label3 = new Label();
            btnSelecTodoExame = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPaginas).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewPaginas
            // 
            dataGridViewPaginas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPaginas.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6 });
            dataGridViewPaginas.Location = new System.Drawing.Point(12, 33);
            dataGridViewPaginas.Name = "dataGridViewPaginas";
            dataGridViewPaginas.RowHeadersWidth = 51;
            dataGridViewPaginas.Size = new System.Drawing.Size(616, 399);
            dataGridViewPaginas.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Código";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "Código";
            dataGridViewTextBoxColumn1.Width = 125;
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Montagem";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 125;
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Pág. Inicial";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 125;
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.ReadOnly = true;

            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Época";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.Width = 125;
            dataGridViewTextBoxColumn4.ReadOnly = true;

            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Duração(seg.)";
            dataGridViewTextBoxColumn5.MinimumWidth = 6;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.Width = 125;
            dataGridViewTextBoxColumn5.ReadOnly = true;

            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Ref";
            dataGridViewTextBoxColumn6.MinimumWidth = 6;
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            dataGridViewTextBoxColumn6.Width = 125;
            // 
            // listBoxArquivos
            // 
            listBoxArquivos.Location = new System.Drawing.Point(12, 481);
            listBoxArquivos.Name = "listBoxArquivos";
            listBoxArquivos.Size = new System.Drawing.Size(616, 104);
            listBoxArquivos.TabIndex = 1;
            // 
            // btnSelecionarTudo
            // 
            btnSelecionarTudo.Font = new System.Drawing.Font("Arial", 7.8F);
            btnSelecionarTudo.Location = new System.Drawing.Point(12, 611);
            btnSelecionarTudo.Name = "btnSelecionarTudo";
            btnSelecionarTudo.Size = new System.Drawing.Size(98, 48);
            btnSelecionarTudo.TabIndex = 2;
            btnSelecionarTudo.Text = "Selecionar\nTudo";
            btnSelecionarTudo.Click += btnSelecionarTudo_Click;
            // 
            // btnImprimir
            // 
            btnImprimir.Font = new System.Drawing.Font("Arial", 7.8F);
            btnImprimir.Location = new System.Drawing.Point(116, 611);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new System.Drawing.Size(98, 48);
            btnImprimir.TabIndex = 3;
            btnImprimir.Text = "Imprimir";
            // 
            // btnVisualizar
            // 
            btnVisualizar.Font = new System.Drawing.Font("Arial", 7.8F);
            btnVisualizar.Location = new System.Drawing.Point(324, 611);
            btnVisualizar.Name = "btnVisualizar";
            btnVisualizar.Size = new System.Drawing.Size(98, 48);
            btnVisualizar.TabIndex = 4;
            btnVisualizar.Text = "Visualizar";
            btnVisualizar.Click += btnVisualizar_Click;
            // 
            // btnExcluir
            // 
            btnExcluir.Font = new System.Drawing.Font("Arial", 7.8F);
            btnExcluir.Location = new System.Drawing.Point(428, 611);
            btnExcluir.Name = "btnExcluir";
            btnExcluir.Size = new System.Drawing.Size(98, 48);
            btnExcluir.TabIndex = 5;
            btnExcluir.Text = "Excluir";
            btnExcluir.Click += btnExcluir_Click;
            // 
            // btnFechar
            // 
            btnFechar.Font = new System.Drawing.Font("Arial", 7.8F);
            btnFechar.Location = new System.Drawing.Point(532, 611);
            btnFechar.Name = "btnFechar";
            btnFechar.Size = new System.Drawing.Size(98, 48);
            btnFechar.TabIndex = 6;
            btnFechar.Text = "Fechar";
            btnFechar.Click += btnFechar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 458);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(42, 20);
            label2.TabIndex = 7;
            label2.Text = "Telas";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 9);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(59, 20);
            label3.TabIndex = 8;
            label3.Text = "Páginas";
            // 
            // btnSelecTodoExame
            // 
            btnSelecTodoExame.Font = new System.Drawing.Font("Arial", 7.8F);
            btnSelecTodoExame.Location = new System.Drawing.Point(220, 611);
            btnSelecTodoExame.Name = "btnSelecTodoExame";
            btnSelecTodoExame.Size = new System.Drawing.Size(98, 48);
            btnSelecTodoExame.TabIndex = 9;
            btnSelecTodoExame.Text = "Selecionar\nTodo Exame";
            btnSelecTodoExame.Click += btnbtnSelecTodoExame_Click;
            // 
            // PagSelecionadasImpressao
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(640, 671);
            Controls.Add(btnSelecTodoExame);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dataGridViewPaginas);
            Controls.Add(listBoxArquivos);
            Controls.Add(btnSelecionarTudo);
            Controls.Add(btnImprimir);
            Controls.Add(btnVisualizar);
            Controls.Add(btnExcluir);
            Controls.Add(btnFechar);
            Name = "PagSelecionadasImpressao";
            Text = "Páginas e Telas Selecionadas para Impressão";
            ((System.ComponentModel.ISupportInitialize)dataGridViewPaginas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label label1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private Label label2;
        private Label label3;
        private Button btnSelecTodoExame;
    }
}