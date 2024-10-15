namespace PlotagemOpenGL.FormesMenuPanels
{
    partial class ProfileForm
    {
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.TextBox txtAltura;
        private System.Windows.Forms.TextBox txtPeso;
        private System.Windows.Forms.ComboBox cmbSexo; // Alterado para ComboBox
        private System.Windows.Forms.DateTimePicker dtpDataNascimento; // DateTimePicker para data de nascimento
        private System.Windows.Forms.DateTimePicker dtpDataExame; // DateTimePicker para data de exame
        private System.Windows.Forms.TextBox txtDuracao;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtCpf;
        private System.Windows.Forms.TextBox txtMedicoSolicitante;
        private System.Windows.Forms.TextBox txtRg;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnOk;

        private void InitializeComponent()
        {
            txtNome = new System.Windows.Forms.TextBox();
            txtAltura = new System.Windows.Forms.TextBox();
            txtPeso = new System.Windows.Forms.TextBox();
            cmbSexo = new System.Windows.Forms.ComboBox();
            dtpDataNascimento = new System.Windows.Forms.DateTimePicker();
            dtpDataExame = new System.Windows.Forms.DateTimePicker();
            txtDuracao = new System.Windows.Forms.TextBox();
            txtEmail = new System.Windows.Forms.TextBox();
            txtCpf = new System.Windows.Forms.TextBox();
            txtMedicoSolicitante = new System.Windows.Forms.TextBox();
            txtRg = new System.Windows.Forms.TextBox();
            txtObservacao = new System.Windows.Forms.TextBox();
            btnAlterar = new System.Windows.Forms.Button();
            btnOk = new System.Windows.Forms.Button();
            Paciente = new System.Windows.Forms.Label();
            DataNascimento = new System.Windows.Forms.Label();
            Sexo = new System.Windows.Forms.Label();
            Altura = new System.Windows.Forms.Label();
            Peso = new System.Windows.Forms.Label();
            Email = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            Duracao = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            RG = new System.Windows.Forms.Label();
            CPF = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            PgparaImpressao = new System.Windows.Forms.TextBox();
            PeriodosDeMapeamento = new System.Windows.Forms.TextBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            Bebe = new System.Windows.Forms.RadioButton();
            Infantil = new System.Windows.Forms.RadioButton();
            Adulto = new System.Windows.Forms.RadioButton();
            txtArquivo = new System.Windows.Forms.TextBox();
            Arquivo = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // txtNome
            // 
            txtNome.Font = new System.Drawing.Font("Arial", 10.2F);
            txtNome.Location = new System.Drawing.Point(20, 29);
            txtNome.Name = "txtNome";
            txtNome.ReadOnly = true;
            txtNome.Size = new System.Drawing.Size(620, 27);
            txtNome.TabIndex = 0;
            txtNome.TabStop = false;
            txtNome.KeyPress += txtNome_KeyPress;
            // 
            // txtAltura
            // 
            txtAltura.Font = new System.Drawing.Font("Arial", 10.2F);
            txtAltura.Location = new System.Drawing.Point(145, 80);
            txtAltura.Name = "txtAltura";
            txtAltura.ReadOnly = true;
            txtAltura.Size = new System.Drawing.Size(83, 27);
            txtAltura.TabIndex = 1;
            txtAltura.TabStop = false;
            txtAltura.KeyPress += txtAlturaa_KeyPress;
            // 
            // txtPeso
            // 
            txtPeso.Font = new System.Drawing.Font("Arial", 10.2F);
            txtPeso.Location = new System.Drawing.Point(232, 80);
            txtPeso.Name = "txtPeso";
            txtPeso.ReadOnly = true;
            txtPeso.Size = new System.Drawing.Size(86, 27);
            txtPeso.TabIndex = 2;
            txtPeso.TabStop = false;
            txtPeso.KeyPress += txtPeso_KeyPress;
            // 
            // cmbSexo
            // 
            cmbSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbSexo.Enabled = false;
            cmbSexo.Font = new System.Drawing.Font("Arial", 10.2F);
            cmbSexo.Location = new System.Drawing.Point(20, 80);
            cmbSexo.Name = "cmbSexo";
            cmbSexo.Size = new System.Drawing.Size(123, 27);
            cmbSexo.TabIndex = 4;
            cmbSexo.TabStop = false;
            // 
            // dtpDataNascimento
            // 
            dtpDataNascimento.Enabled = false;
            dtpDataNascimento.Font = new System.Drawing.Font("Arial", 10.2F);
            dtpDataNascimento.Location = new System.Drawing.Point(324, 80);
            dtpDataNascimento.Name = "dtpDataNascimento";
            dtpDataNascimento.Size = new System.Drawing.Size(316, 27);
            dtpDataNascimento.TabIndex = 3;
            dtpDataNascimento.TabStop = false;
            // 
            // dtpDataExame
            // 
            dtpDataExame.Enabled = false;
            dtpDataExame.Font = new System.Drawing.Font("Arial", 10.2F);
            dtpDataExame.Location = new System.Drawing.Point(20, 308);
            dtpDataExame.Name = "dtpDataExame";
            dtpDataExame.Size = new System.Drawing.Size(323, 27);
            dtpDataExame.TabIndex = 5;
            dtpDataExame.TabStop = false;
            // 
            // txtDuracao
            // 
            txtDuracao.Font = new System.Drawing.Font("Arial", 10.2F);
            txtDuracao.Location = new System.Drawing.Point(475, 308);
            txtDuracao.Name = "txtDuracao";
            txtDuracao.ReadOnly = true;
            txtDuracao.Size = new System.Drawing.Size(105, 27);
            txtDuracao.TabIndex = 6;
            txtDuracao.TabStop = false;
            // 
            // txtEmail
            // 
            txtEmail.Font = new System.Drawing.Font("Arial", 10.2F);
            txtEmail.Location = new System.Drawing.Point(20, 132);
            txtEmail.Name = "txtEmail";
            txtEmail.ReadOnly = true;
            txtEmail.Size = new System.Drawing.Size(482, 27);
            txtEmail.TabIndex = 7;
            txtEmail.TabStop = false;
            // 
            // txtCpf
            // 
            txtCpf.Font = new System.Drawing.Font("Arial", 10.2F);
            txtCpf.Location = new System.Drawing.Point(234, 187);
            txtCpf.Name = "txtCpf";
            txtCpf.ReadOnly = true;
            txtCpf.Size = new System.Drawing.Size(200, 27);
            txtCpf.TabIndex = 8;
            txtCpf.TabStop = false;
            txtCpf.KeyPress += txtAltura_KeyPress;
            // 
            // txtMedicoSolicitante
            // 
            txtMedicoSolicitante.Font = new System.Drawing.Font("Arial", 10.2F);
            txtMedicoSolicitante.Location = new System.Drawing.Point(20, 242);
            txtMedicoSolicitante.Name = "txtMedicoSolicitante";
            txtMedicoSolicitante.ReadOnly = true;
            txtMedicoSolicitante.Size = new System.Drawing.Size(482, 27);
            txtMedicoSolicitante.TabIndex = 9;
            txtMedicoSolicitante.TabStop = false;
            txtMedicoSolicitante.KeyPress += txtNome_KeyPress;
            // 
            // txtRg
            // 
            txtRg.Font = new System.Drawing.Font("Arial", 10.2F);
            txtRg.Location = new System.Drawing.Point(19, 187);
            txtRg.Name = "txtRg";
            txtRg.ReadOnly = true;
            txtRg.Size = new System.Drawing.Size(200, 27);
            txtRg.TabIndex = 10;
            txtRg.TabStop = false;
            txtRg.KeyPress += txtAltura_KeyPress;
            // 
            // txtObservacao
            // 
            txtObservacao.Font = new System.Drawing.Font("Arial", 10.2F);
            txtObservacao.Location = new System.Drawing.Point(20, 420);
            txtObservacao.Multiline = true;
            txtObservacao.Name = "txtObservacao";
            txtObservacao.ReadOnly = true;
            txtObservacao.Size = new System.Drawing.Size(630, 60);
            txtObservacao.TabIndex = 11;
            txtObservacao.TabStop = false;
            // 
            // btnAlterar
            // 
            btnAlterar.Font = new System.Drawing.Font("Arial", 10.2F);
            btnAlterar.Location = new System.Drawing.Point(20, 486);
            btnAlterar.Name = "btnAlterar";
            btnAlterar.Size = new System.Drawing.Size(115, 37);
            btnAlterar.TabIndex = 12;
            btnAlterar.Text = "Alterar";
            btnAlterar.Click += btnAlterar_Click;
            // 
            // btnOk
            // 
            btnOk.Font = new System.Drawing.Font("Arial", 10.2F);
            btnOk.Location = new System.Drawing.Point(535, 486);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(115, 37);
            btnOk.TabIndex = 13;
            btnOk.Text = "Ok";
            btnOk.Click += btnOk_Click;
            // 
            // Paciente
            // 
            Paciente.AutoSize = true;
            Paciente.Font = new System.Drawing.Font("Arial", 10.2F);
            Paciente.Location = new System.Drawing.Point(20, 6);
            Paciente.Name = "Paciente";
            Paciente.Size = new System.Drawing.Size(73, 19);
            Paciente.TabIndex = 14;
            Paciente.Text = "Paciente";
            // 
            // DataNascimento
            // 
            DataNascimento.AutoSize = true;
            DataNascimento.Font = new System.Drawing.Font("Arial", 10.2F);
            DataNascimento.Location = new System.Drawing.Point(324, 58);
            DataNascimento.Name = "DataNascimento";
            DataNascimento.Size = new System.Drawing.Size(133, 19);
            DataNascimento.TabIndex = 15;
            DataNascimento.Text = "Data Nascimento";
            // 
            // Sexo
            // 
            Sexo.AutoSize = true;
            Sexo.Font = new System.Drawing.Font("Arial", 10.2F);
            Sexo.Location = new System.Drawing.Point(20, 59);
            Sexo.Name = "Sexo";
            Sexo.Size = new System.Drawing.Size(45, 19);
            Sexo.TabIndex = 16;
            Sexo.Text = "Sexo";
            // 
            // Altura
            // 
            Altura.AutoSize = true;
            Altura.Font = new System.Drawing.Font("Arial", 10.2F);
            Altura.Location = new System.Drawing.Point(145, 58);
            Altura.Name = "Altura";
            Altura.Size = new System.Drawing.Size(76, 19);
            Altura.TabIndex = 17;
            Altura.Text = "Altura(m)";
            // 
            // Peso
            // 
            Peso.AutoSize = true;
            Peso.Font = new System.Drawing.Font("Arial", 10.2F);
            Peso.Location = new System.Drawing.Point(234, 59);
            Peso.Name = "Peso";
            Peso.Size = new System.Drawing.Size(75, 19);
            Peso.TabIndex = 18;
            Peso.Text = "Peso(kg)";
            // 
            // Email
            // 
            Email.AutoSize = true;
            Email.Font = new System.Drawing.Font("Arial", 10.2F);
            Email.Location = new System.Drawing.Point(20, 110);
            Email.Name = "Email";
            Email.Size = new System.Drawing.Size(49, 19);
            Email.TabIndex = 19;
            Email.Text = "Email";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial", 10.2F);
            label1.Location = new System.Drawing.Point(20, 286);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(120, 19);
            label1.TabIndex = 20;
            label1.Text = "Data do Exame";
            // 
            // Duracao
            // 
            Duracao.AutoSize = true;
            Duracao.Font = new System.Drawing.Font("Arial", 10.2F);
            Duracao.Location = new System.Drawing.Point(475, 286);
            Duracao.Name = "Duracao";
            Duracao.Size = new System.Drawing.Size(72, 19);
            Duracao.TabIndex = 21;
            Duracao.Text = "Duracao";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Arial", 10.2F);
            label2.Location = new System.Drawing.Point(20, 220);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(142, 19);
            label2.TabIndex = 22;
            label2.Text = "Medico Solicitante";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Arial", 10.2F);
            label3.Location = new System.Drawing.Point(20, 347);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(188, 19);
            label3.TabIndex = 23;
            label3.Text = "Paginas Para impressao";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Arial", 10.2F);
            label4.Location = new System.Drawing.Point(234, 347);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(195, 19);
            label4.TabIndex = 24;
            label4.Text = "Periodos de mapeamento";
            // 
            // RG
            // 
            RG.AutoSize = true;
            RG.Font = new System.Drawing.Font("Arial", 10.2F);
            RG.Location = new System.Drawing.Point(19, 165);
            RG.Name = "RG";
            RG.Size = new System.Drawing.Size(32, 19);
            RG.TabIndex = 25;
            RG.Text = "RG";
            // 
            // CPF
            // 
            CPF.AutoSize = true;
            CPF.Font = new System.Drawing.Font("Arial", 10.2F);
            CPF.Location = new System.Drawing.Point(234, 165);
            CPF.Name = "CPF";
            CPF.Size = new System.Drawing.Size(42, 19);
            CPF.TabIndex = 26;
            CPF.Text = "CPF";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Arial", 10.2F);
            label5.Location = new System.Drawing.Point(20, 398);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(96, 19);
            label5.TabIndex = 27;
            label5.Text = "Observacao";
            // 
            // PgparaImpressao
            // 
            PgparaImpressao.Font = new System.Drawing.Font("Arial", 10.2F);
            PgparaImpressao.Location = new System.Drawing.Point(20, 369);
            PgparaImpressao.Name = "PgparaImpressao";
            PgparaImpressao.ReadOnly = true;
            PgparaImpressao.Size = new System.Drawing.Size(200, 27);
            PgparaImpressao.TabIndex = 28;
            PgparaImpressao.TabStop = false;
            // 
            // PeriodosDeMapeamento
            // 
            PeriodosDeMapeamento.Font = new System.Drawing.Font("Arial", 10.2F);
            PeriodosDeMapeamento.Location = new System.Drawing.Point(234, 369);
            PeriodosDeMapeamento.Name = "PeriodosDeMapeamento";
            PeriodosDeMapeamento.ReadOnly = true;
            PeriodosDeMapeamento.Size = new System.Drawing.Size(195, 27);
            PeriodosDeMapeamento.TabIndex = 29;
            PeriodosDeMapeamento.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(Bebe);
            groupBox1.Controls.Add(Infantil);
            groupBox1.Controls.Add(Adulto);
            groupBox1.Font = new System.Drawing.Font("Arial", 10.2F);
            groupBox1.Location = new System.Drawing.Point(517, 113);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(123, 133);
            groupBox1.TabIndex = 30;
            groupBox1.TabStop = false;
            // 
            // Bebe
            // 
            Bebe.AutoSize = true;
            Bebe.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Bebe.Location = new System.Drawing.Point(15, 84);
            Bebe.Name = "Bebe";
            Bebe.Size = new System.Drawing.Size(68, 23);
            Bebe.TabIndex = 2;
            Bebe.TabStop = true;
            Bebe.Text = "Bebe";
            Bebe.UseVisualStyleBackColor = true;
            // 
            // Infantil
            // 
            Infantil.AutoSize = true;
            Infantil.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Infantil.Location = new System.Drawing.Point(15, 55);
            Infantil.Name = "Infantil";
            Infantil.Size = new System.Drawing.Size(78, 23);
            Infantil.TabIndex = 1;
            Infantil.TabStop = true;
            Infantil.Text = "Infantil";
            Infantil.UseVisualStyleBackColor = true;
            // 
            // Adulto
            // 
            Adulto.AutoSize = true;
            Adulto.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Adulto.Location = new System.Drawing.Point(15, 26);
            Adulto.Name = "Adulto";
            Adulto.Size = new System.Drawing.Size(75, 23);
            Adulto.TabIndex = 0;
            Adulto.TabStop = true;
            Adulto.Text = "Adulto";
            Adulto.UseVisualStyleBackColor = true;
            // 
            // txtArquivo
            // 
            txtArquivo.Font = new System.Drawing.Font("Arial", 10.2F);
            txtArquivo.Location = new System.Drawing.Point(354, 308);
            txtArquivo.Name = "txtArquivo";
            txtArquivo.ReadOnly = true;
            txtArquivo.Size = new System.Drawing.Size(105, 27);
            txtArquivo.TabIndex = 31;
            txtArquivo.TabStop = false;
            // 
            // Arquivo
            // 
            Arquivo.AutoSize = true;
            Arquivo.Font = new System.Drawing.Font("Arial", 10.2F);
            Arquivo.Location = new System.Drawing.Point(354, 286);
            Arquivo.Name = "Arquivo";
            Arquivo.Size = new System.Drawing.Size(64, 19);
            Arquivo.TabIndex = 32;
            Arquivo.Text = "Arquivo";
            // 
            // ProfileForm
            // 
            ClientSize = new System.Drawing.Size(662, 535);
            Controls.Add(Arquivo);
            Controls.Add(txtArquivo);
            Controls.Add(groupBox1);
            Controls.Add(PeriodosDeMapeamento);
            Controls.Add(PgparaImpressao);
            Controls.Add(label5);
            Controls.Add(CPF);
            Controls.Add(RG);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(Duracao);
            Controls.Add(label1);
            Controls.Add(Email);
            Controls.Add(Peso);
            Controls.Add(Altura);
            Controls.Add(Sexo);
            Controls.Add(DataNascimento);
            Controls.Add(Paciente);
            Controls.Add(txtNome);
            Controls.Add(txtAltura);
            Controls.Add(txtPeso);
            Controls.Add(txtDuracao);
            Controls.Add(txtEmail);
            Controls.Add(txtCpf);
            Controls.Add(txtMedicoSolicitante);
            Controls.Add(txtRg);
            Controls.Add(txtObservacao);
            Controls.Add(btnAlterar);
            Controls.Add(btnOk);
            Controls.Add(cmbSexo);
            Controls.Add(dtpDataNascimento);
            Controls.Add(dtpDataExame);
            Name = "ProfileForm";
            Text = "Dados do Exame";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label Paciente;
        private System.Windows.Forms.Label DataNascimento;
        private System.Windows.Forms.Label Sexo;
        private System.Windows.Forms.Label Altura;
        private System.Windows.Forms.Label Peso;
        private System.Windows.Forms.Label Email;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Duracao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label RG;
        private System.Windows.Forms.Label CPF;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PgparaImpressao;
        private System.Windows.Forms.TextBox PeriodosDeMapeamento;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Bebe;
        private System.Windows.Forms.RadioButton Infantil;
        private System.Windows.Forms.RadioButton Adulto;
        private System.Windows.Forms.TextBox txtArquivo;
        private System.Windows.Forms.Label Arquivo;
    }
}
