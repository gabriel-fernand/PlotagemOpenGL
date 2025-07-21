using Accord.Audio;
using Accord.Math;
using Cyotek.Windows.Forms;
using PlotagemOpenGL.auxi;
using PlotagemOpenGL.auxi.auxPlotagem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Tensorflow.Operations;

namespace PlotagemOpenGL.FormesMenuPanels.AuxiAutoAnalise
{
    internal class RegraPLM
    {


        DataTable DtMovimentoPnPlm = new DataTable();
        int codCanal;

        float taxaAmos;
        float DurMinEv;
        float DurMaxEv;
        float DistanciaMin;
        float DistanciaMax;
        float DurJanBasal;
        float DurJanEvento;
        float IntervaloMinEntreEv;
        float FatorAmplituide;
        bool Est_0 = false;
        float QtdMinParaSerPLM;
        float NumVezesDurMinEv;
        float NumVezesAmplitudeBasal;
        float tipoAmplBasal;
        float FatorMultAmpEstagio;
        AnaliseAuto owner;

        public RegraPLM(int codCanal, AnaliseAuto owner)
        {
            this.owner = owner;
            if (!VerificaSeExisteEvento()) return;
            this.codCanal = codCanal;
            EncheDt();
            AnalisaEventos();
        }
        private bool VerificaSeExisteEvento()
        {
            bool tem = false;
            
            if(GlobVar.eventosUpdate == null)
            {
                tem = false;
            }
            else
            {
                tem = GlobVar.eventosUpdate.AsEnumerable().Any(row => row.Field<int>("CodEvento") == 12 || row.Field<int>("CodEvento") == 22);
            }
            return tem;
        }
        private void EncheDt()
        {
            DtMovimentoPnPlm = GlobVar.eventosUpdate.AsEnumerable()
                .Where(row => row.Field<int>("CodEvento") == 12 || row.Field<int>("CodEvento") == 22)
                .OrderBy(row => row.Field<int>("Inicio")) // agora sim, ordenando aqui
                .CopyToDataTable(); // copia os dados ordenados para um novo DataTable
        }
        private void AnalisaEventos()
        {
            if (GlobVar.tbl_ParametrosParaAnalisar == null) return;

            var row = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];
            int indexx = GlobVar.codCanal.IndexOf(codCanal);
            int taxa = GlobVar.txPorCanal[indexx];

            if (row["PLM_Dur_Min_Ev"] == DBNull.Value || row["PLM_Dur_Max_Ev"] == DBNull.Value || row["PLM_Distancia_Min"] == DBNull.Value
                || row["PLM_Distancia_Max"] == DBNull.Value || row["PLM_Dur_Jan_Basal"] == DBNull.Value || row["PLM_Dur_Jan_Evento"] == DBNull.Value
                || row["PLM_Interv_Min_Entre_Ev"] == DBNull.Value || row["PLM_Fator_Amplitude"] == DBNull.Value || row["PLM_Est_0"] == DBNull.Value || row["PLM_Qtd_Min_Para_Ser_PLM"] == DBNull.Value
                || row["PLM_Tipo_Ampl_Basal"] == DBNull.Value || row["PLM_Num_Vezes_Dur_Min_Ev"] == DBNull.Value || row["PLM_Num_Vezes_Amplitude_Basal"] == DBNull.Value || row["PLM_Fator_Mult_Ampl_Estagio"] == DBNull.Value) { return; }
            else
            {

                DurMinEv = (float)Convert.ToDouble(row["PLM_Dur_Min_Ev"]) * taxa;
                DurMaxEv = Convert.ToInt32(row["PLM_Dur_Max_Ev"]) * taxa;

                DistanciaMin = Convert.ToInt32(row["PLM_Distancia_Min"]) * taxa;
                DistanciaMax = Convert.ToInt32(row["PLM_Distancia_Max"]) * taxa;

                DurJanBasal = Convert.ToInt32(row["PLM_Dur_Jan_Basal"]) * taxa;
                DurJanEvento = (float)Convert.ToDouble(row["PLM_Dur_Jan_Evento"]) * taxa;
                IntervaloMinEntreEv = (float)Convert.ToDouble(row["PLM_Interv_Min_Entre_Ev"]) * taxa;
                FatorAmplituide = Convert.ToInt32(row["PLM_Fator_Amplitude"]);
                Est_0 = Convert.ToBoolean(row["PLM_Est_0"]);
                QtdMinParaSerPLM = Convert.ToInt32(row["PLM_Qtd_Min_Para_Ser_PLM"]);
                NumVezesDurMinEv = Convert.ToInt32(row["PLM_Num_Vezes_Dur_Min_Ev"]);
                tipoAmplBasal = Convert.ToInt32(row["PLM_Num_Vezes_Amplitude_Basal"]);
                FatorMultAmpEstagio = Convert.ToInt32(row["PLM_Fator_Mult_Ampl_Estagio"]);

            }
            if (owner.cancellationToken.IsCancellationRequested) return;
            owner.AtualizarProgresso(3);
            List<DataRow> eventosExcluir = new();
            List<DataRow> eventosAdicionar = new();
            // Variáveis de controle
            int lastIni = -1;
            int lastFim = -1;
            int lasCodEvento = 0;
            int lasCodCanal = 0;
            int primeiraSeq = 0;
            int numPagInicio = 0;
            int numPagTermino = 0;
            string numPag = "";

            List<DataRow> eventosDeletar = new();
            if (owner.cancellationToken.IsCancellationRequested) return;
            owner.AtualizarProgresso(6);

            // PASSO 1 - Junta os eventos caso o intervalo entre eles seja menor do que o esperado
            int totalEtapas = 4;
            int tamanhoEtapa = (100 - 6) / totalEtapas;
            int progressoAtual = 6;
            int loc = 0;
            int ultimoValor = -1;
            int total = DtMovimentoPnPlm.Rows.Count;

            foreach (DataRow rw in DtMovimentoPnPlm.Rows)
            {
                loc++;
                int progressoCalculado = progressoAtual + (int)(((double)loc / total) * tamanhoEtapa);

                if (progressoCalculado != ultimoValor)
                {
                    owner.AtualizarProgresso(progressoCalculado);
                    ultimoValor = progressoCalculado;
                }

                if (owner.cancellationToken.IsCancellationRequested) return;
                // Extração dos dados da linha
                int codEvento = Convert.ToInt32(rw["CodEvento"]);
                int codCanal = Convert.ToInt32(rw["CodCanal1"]);
                int inicio = Convert.ToInt32(rw["Inicio"]);
                int duracao = Convert.ToInt32(rw["Duracao"]);
                int seq = Convert.ToInt32(rw["Seq"]);
                numPag = rw["NumPag"].ToString();

                // Separa a string "NumPag", que deve estar no formato "numInicio -- numTermino"
                string[] partes = numPag.Split(new[] { "--" }, StringSplitOptions.None);
                if (partes.Length < 2)
                    continue; // Se não estiver no formato esperado, pula a linha

                int pagInicio = int.Parse(partes[0].Trim());
                int pagTermino = int.Parse(partes[1].Trim());

                if (lastIni < 0)
                {
                    // Primeira linha: inicializa os valores de controle
                    lastIni = inicio;
                    lastFim = duracao;
                    lasCodEvento = codEvento;
                    lasCodCanal = codCanal;
                    primeiraSeq = seq;
                    numPagInicio = pagInicio;

                    eventosDeletar.Add(rw);
                }
                else
                {
                    // Verifica se o evento atual se junta com o grupo anterior
                    if (lastFim + IntervaloMinEntreEv > inicio &&
                        lasCodCanal == codCanal &&
                        lasCodEvento == codEvento)
                    {
                        // Atualiza o número da página final com base na linha atual
                        numPagTermino = pagTermino;
                        string novoNumPag = $"{numPagInicio} -- {numPagTermino}";
                        // Atualiza o final do evento para o grupo atual
                        lastFim = duracao;

                        // Cria uma nova linha consolidada com os dados do grupo
                        DataRow novaLinha = DtMovimentoPnPlm.NewRow();
                        novaLinha["Seq"] = seq;
                        novaLinha["NumPag"] = novoNumPag;
                        novaLinha["CodEvento"] = lasCodEvento;
                        novaLinha["CodCanal1"] = lasCodCanal;
                        novaLinha["Inicio"] = lastIni;
                        novaLinha["Duracao"] = lastFim;
                        novaLinha["MenorSat"] = Convert.ToInt32(rw["MenorSat"]);
                        novaLinha["Posicao"] = rw["Posicao"].ToString();

                        //DtMovimentoPnPlm.Rows.Add(novaLinha);
                        eventosAdicionar.Add(novaLinha);
                        // Adiciona a linha atual na lista de deleção e remove todas as linhas do grupo
                        eventosDeletar.Add(rw);
                        foreach (DataRow linha in eventosDeletar)
                        {
                            // Agora que estamos iterando sobre uma cópia, a remoção é segura
                            eventosExcluir.Add(linha);
                        }
                        eventosDeletar.Clear();

                        // Reinicia o grupo com os dados da linha atual
                        lastIni = inicio;
                        lastFim = duracao;
                        numPagInicio = pagInicio;
                        lasCodEvento = codEvento;
                        lasCodCanal = codCanal;
                        primeiraSeq = seq;
                        eventosDeletar.Add(rw);
                    }
                    else
                    {
                        // Se o evento não se encaixa no grupo, reinicia o grupo com os dados da linha atual
                        eventosDeletar.Clear();

                        numPag = rw["NumPag"].ToString();
                        partes = numPag.Split(new[] { "--" }, StringSplitOptions.None);
                        numPagInicio = int.Parse(partes[0].Trim());

                        lasCodEvento = codEvento;
                        lasCodCanal = codCanal;
                        lastIni = inicio;
                        lastFim = duracao;
                        primeiraSeq = seq;
                        eventosDeletar.Add(rw);
                    }
                }
            }
            progressoAtual += tamanhoEtapa;
            eventosExcluir = eventosExcluir
                                        .GroupBy(r => r["Seq"])
                                        .Select(g => g.First())
                                        .ToList();

            foreach (DataRow lin in eventosExcluir)
            {
                if (owner.cancellationToken.IsCancellationRequested) return;
                if (lin.Table != null)
                {
                    DeleteRow(Convert.ToInt32(lin["Seq"]));
                }
            }
            foreach (DataRow lin in eventosAdicionar)
            {
                if (owner.cancellationToken.IsCancellationRequested) return;
                DtMovimentoPnPlm.Rows.Add(lin);
            }

            eventosDeletar.Clear();
            loc = 0; ultimoValor = -1; progressoAtual += tamanhoEtapa;
            //PASSO 2 - Deleta o evento caso o tamanho dele seja menor ou maior que o esperado
            foreach (DataRow rw in DtMovimentoPnPlm.Rows)
            {
                loc++;
                int progressoCalculado = progressoAtual + (int)(((double)loc / total) * tamanhoEtapa);
                if (progressoCalculado != ultimoValor)
                {
                    owner.AtualizarProgresso(progressoCalculado);
                    ultimoValor = progressoCalculado;
                }
                if (owner.cancellationToken.IsCancellationRequested) return;
                int ini = Convert.ToInt32(rw["Inicio"]);
                int fim = Convert.ToInt32(rw["Duracao"]);
                int tamanho = fim - ini;

                if (tamanho < DurMinEv || tamanho > DurMaxEv)
                {
                    eventosDeletar.Add(rw);
                }
            }
            foreach (var lin in eventosDeletar)
            {
                if (owner.cancellationToken.IsCancellationRequested) return;
                DtMovimentoPnPlm.Rows.Remove(lin);
            }
            lastIni = -1;
            lastFim = -1;
            lasCodCanal = 0;
            primeiraSeq = 0;
            int eventosProximos = 0;
            eventosAdicionar.Clear();
            eventosDeletar.Clear();
            eventosExcluir.Clear();
            DtMovimentoPnPlm = DtMovimentoPnPlm.AsEnumerable().OrderBy(row => row.Field<int>("Inicio")).CopyToDataTable();
            loc = 0; ultimoValor = -1; progressoAtual += tamanhoEtapa;

            //PASSO 3 - verifica se   PLM ou apenas Movimento de Perna
            foreach (DataRow rw in DtMovimentoPnPlm.Rows)
            {
                loc++;
                int progressoCalculado = progressoAtual + (int)(((double)loc / total) * tamanhoEtapa);
                if (progressoCalculado != ultimoValor)
                {
                    owner.AtualizarProgresso(progressoCalculado);
                    ultimoValor = progressoCalculado;
                }
                if (owner.cancellationToken.IsCancellationRequested) return;
                if (lastIni < 0)
                {
                    eventosProximos++;
                    numPag = rw["NumPag"].ToString(); // exemplo
                    string[] partes = numPag.Split(new[] { "--" }, StringSplitOptions.None);

                    numPagInicio = int.Parse(partes[0].Trim());

                    lasCodEvento = Convert.ToInt32(rw["CodEvento"]);
                    lasCodCanal = Convert.ToInt32(rw["CodCanal1"]);
                    lastIni = Convert.ToInt32(rw["Inicio"]);
                    lastFim = Convert.ToInt32(rw["Duracao"]);
                    primeiraSeq = Convert.ToInt32(rw["Seq"]);

                    eventosDeletar.Add(rw);
                }
                else
                {
                    if (Math.Abs(lastFim - Convert.ToInt32(rw["Inicio"])) > DistanciaMin && Math.Abs(lastFim - Convert.ToInt32(rw["Inicio"])) < DistanciaMax && lasCodCanal == Convert.ToInt32(rw["CodCanal1"]))
                    {
                        eventosProximos++;
                        numPag = rw["NumPag"].ToString(); // exemplo
                        string[] partes = numPag.Split(new[] { "--" }, StringSplitOptions.None);

                        numPagTermino = int.Parse(partes[1].Trim());
                        string NumPag = $"{numPagInicio} -- {numPagTermino}";
                        lastFim = Convert.ToInt32(rw["Duracao"]);
                        int CodCanal = Convert.ToInt32(rw["CodCanal1"]);
                        int CodEvento = Convert.ToInt32(rw["CodEvento"]);
                        int seq = Convert.ToInt32(rw["Seq"]);
                        //Maneira de colocar no meu DataTable, cada valor na sua coluna correspondente 
                        eventosDeletar.Add(rw);
                    }
                    else if (eventosProximos >= QtdMinParaSerPLM)
                    {
                        string NumPag = $"{numPagInicio} -- {numPagTermino}";
                        //Maneira de colocar no meu DataTable, cada valor na sua coluna correspondente 

                        DataRow novaLinha = DtMovimentoPnPlm.NewRow();
                        novaLinha["Seq"] = primeiraSeq; // ou outro valor de sequência que você quer manter
                        novaLinha["NumPag"] = NumPag;
                        novaLinha["CodEvento"] = lasCodEvento;
                        novaLinha["CodCanal1"] = lasCodCanal;
                        novaLinha["Inicio"] = lastIni;
                        novaLinha["Duracao"] = lastFim;
                        novaLinha["MenorSat"] = Convert.ToInt32(rw["MenorSat"]); // ou algum valor que você tenha, se for calculado
                        novaLinha["Posicao"] = rw["Posicao"].ToString(); // ou algum valor válido, caso tenha

                        eventosAdicionar.Add(novaLinha);

                        eventosDeletar.Add(rw);
                        foreach (var ln in eventosDeletar)
                        {
                            eventosExcluir.Add(ln);
                        }
                        eventosDeletar.Clear();
                        numPag = rw["NumPag"].ToString(); // exemplo
                        string[] partes = numPag.Split(new[] { "--" }, StringSplitOptions.None);
                        eventosProximos = 1;

                        numPagInicio = int.Parse(partes[0].Trim());

                        lasCodEvento = Convert.ToInt32(rw["CodEvento"]);
                        lasCodCanal = Convert.ToInt32(rw["CodCanal1"]);
                        lastIni = Convert.ToInt32(rw["Inicio"]);
                        lastFim = Convert.ToInt32(rw["Duracao"]);
                        primeiraSeq = Convert.ToInt32(rw["Seq"]);
                        lastFim = Convert.ToInt32(rw["Duracao"]);
                        eventosDeletar.Add(rw);

                    }
                    else
                    {
                        eventosDeletar.Clear();
                        eventosProximos = 1;
                        numPag = rw["NumPag"].ToString(); // exemplo
                        string[] partes = numPag.Split(new[] { "--" }, StringSplitOptions.None);

                        numPagInicio = int.Parse(partes[0].Trim());

                        lasCodEvento = Convert.ToInt32(rw["CodEvento"]);
                        lasCodCanal = Convert.ToInt32(rw["CodCanal1"]);
                        lastIni = Convert.ToInt32(rw["Inicio"]);
                        lastFim = Convert.ToInt32(rw["Duracao"]);
                        primeiraSeq = Convert.ToInt32(rw["Seq"]);
                        lastFim = Convert.ToInt32(rw["Duracao"]);
                        eventosDeletar.Add(rw);
                    }
                }
            }

            eventosExcluir = eventosExcluir
                            .GroupBy(r => r["Seq"])
                            .Select(g => g.First())
                            .ToList();

            foreach (DataRow lin in eventosExcluir)
            {
                if (owner.cancellationToken.IsCancellationRequested) return;
                if (lin.Table != null)
                {
                    DeleteRow(Convert.ToInt32(lin["Seq"]));
                }
            }
            foreach (DataRow lin in eventosAdicionar)
            {
                if (owner.cancellationToken.IsCancellationRequested) return;
                DtMovimentoPnPlm.Rows.Add(lin);
            }

            total = eventosAdicionar.Count; loc = 0; ultimoValor = -1; progressoAtual += tamanhoEtapa;

            //PASSO 4 - Salvar alteracao
            // Inserir todos os eventos no banco de uma vez
            int aaa = 0;
            // Inserir todos os eventos no banco de uma vez
            foreach (DataRow ln in eventosExcluir)
            {
                if (owner.cancellationToken.IsCancellationRequested) return;
                if (ln.Table != null)
                {
                    ExcluiEventoSeq(Convert.ToInt32(ln["Seq"])); // verificar errro
                }
            }
            foreach (var ev in eventosAdicionar)
            {
                loc++;
                int progressoCalculado = progressoAtual + (int)(((double)loc / total) * tamanhoEtapa);
                if (progressoCalculado != ultimoValor)
                {
                    owner.AtualizarProgresso(progressoCalculado);
                    ultimoValor = progressoCalculado;
                }

                if (owner.cancellationToken.IsCancellationRequested) return;
                AdicionarEventoAoDataTable(Convert.ToInt32(ev["Inicio"]), Convert.ToInt32(ev["Duracao"]), Convert.ToInt32(ev["CodEvento"]), Convert.ToInt32(ev["CodCanal1"]), taxa);
            }

            string query = "SELECT * FROM tbl_Eventos";
            using var command = new OleDbCommand(query, GlobVar.ConnectionBDdat);
            using var adapterEventosDtNormal = new OleDbDataAdapter(command);
            GlobVar.eventos.Clear();
            adapterEventosDtNormal.Fill(GlobVar.eventos);
            owner.AtualizarProgresso(99);

        }
        public static void AdicionarEventoAoDataTable(int inicio, int termino, int codEvento, int codcanal1, int taxa)
        {
            try
            {
                if (GlobVar.lastEvent != null)
                {
                    DataTable eventos = GlobVar.eventosUpdate;

                    //int loc = EncontrarValorMaisProximo(desenhoLoc, startY);

                    // Adicionar colunas ao DataTable se não existirem
                    if (eventos.Columns.Count == 0)
                    {
                        eventos.Columns.Add("Seq", typeof(int));
                        eventos.Columns.Add("NumPag", typeof(string));
                        eventos.Columns.Add("CodEvento", typeof(int));
                        eventos.Columns.Add("CodCanal1", typeof(int));
                        eventos.Columns.Add("Inicio", typeof(int));
                        eventos.Columns.Add("Duracao", typeof(int));
                        eventos.Columns.Add("MenorSat", typeof(int));
                        eventos.Columns.Add("Posicao", typeof(string));

                    }

                    // Calcular NumPag para início e término
                    int numPagInicio = inicio / taxa;//GlobVar.txPorCanal[GlobVar.grafSelected[YAdjusted]];
                    int numPagTermino = termino / taxa;//GlobVar.txPorCanal[GlobVar.grafSelected[YAdjusted]];
                    string numPag = $"{numPagInicio} -- {numPagTermino}";
                    // Obter o próximo valor de Seq
                    int seq = plotComentatios.AtualizarProxSeqEvento();

                    plotEventos.minSaturacao(numPagInicio, numPagTermino);
                    int minSat = GlobVar.minSat.Min();
                    string posi = plotEventos.Posicao(numPagInicio, numPagTermino);

                    // Adicionar dados ao DataTable
                    GlobVar.eventosUpdate.Rows.Add(seq, numPag, codEvento, codcanal1, inicio, termino, minSat, posi);
                    GravaEvento(seq, numPagInicio, codEvento, codcanal1, -1, inicio, termino, GlobVar.namos, numPagTermino, minSat, posi);
                    // Exportar DataTable para Excel
                    string excelFilePath = @"C:\Teste\Teste";
                    //CreateCSVFile(GlobVar.eventosUpdate, excelFilePath);
                    eventos.Dispose();
                }
            }
            catch { }
        }
        public static long GravaEvento(int seq, int NumPag, int CodEvento, int CodCanal1, int CodCanal2, int Inicio, int duracao, int sizepag, int LasPag, int MenorSat = 0, string Posicao = ".")
        {
            try
            {
                long seq_aux;
                int codret = 0;
                //string Posicao = ".";
                duracao = (duracao - Inicio);// / LasPag;
                int auxInicio = Inicio / sizepag;
                Inicio = Inicio - (auxInicio * sizepag);

                string strSQL = $"SELECT * FROM tbl_Eventos WHERE Seq = {seq}";

                // Cria e abre o DataAdapter
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(strSQL, GlobVar.ConnectionBDdat))
                {
                    DataTable rs = new DataTable();
                    adapter.Fill(rs);

                    if (seq == -1)
                    {
                        // Buscar o próximo sequencial de evento
                        strSQL = "SELECT * FROM tbl_SeqEvento";
                        DataTable rs_seq = new DataTable();
                        using (OleDbDataAdapter seqAdapter = new OleDbDataAdapter(strSQL, GlobVar.ConnectionBDdat))
                        {
                            seqAdapter.Fill(rs_seq);
                            if (rs_seq.Rows.Count == 0)
                            {
                                seq_aux = 1;
                                using (OleDbCommand cmdInsert = new OleDbCommand("INSERT INTO tbl_SeqEvento (ProxSeqEvento) VALUES (2)", GlobVar.ConnectionBDdat))
                                {
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                seq_aux = (long)rs_seq.Rows[0]["ProxSeqEvento"];
                                using (OleDbCommand cmdUpdate = new OleDbCommand("UPDATE tbl_SeqEvento SET ProxSeqEvento = ProxSeqEvento + 1", GlobVar.ConnectionBDdat))
                                {
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                        }

                        // Verifica se não existe um evento idêntico ao que está sendo incluído
                        strSQL = $"SELECT * FROM tbl_Eventos WHERE CodEvento = {CodEvento} AND CodCanal1 = {CodCanal1} AND CodCanal2 = {CodCanal2} AND NumPag = {NumPag} AND Inicio = {Inicio}";
                        DataTable rs_aux = new DataTable();
                        using (OleDbDataAdapter auxAdapter = new OleDbDataAdapter(strSQL, GlobVar.ConnectionBDdat))
                        {
                            auxAdapter.Fill(rs_aux);
                            if (rs_aux.Rows.Count > 0)
                            {
                                codret = ExcluiEventoSeq((int)rs_aux.Rows[0]["Seq"]);
                            }
                        }
                    }
                    else
                    {
                        seq_aux = seq;
                        if (rs.Rows.Count > 0)
                        {
                            if (MenorSat != null)
                            {
                                MenorSat = Convert.ToInt32(rs.Rows[0]["MenorSat"]);
                            }
                            Posicao = rs.Rows[0]["Posicao"].ToString();
                        }

                        ExcluiEventoSeq(seq);
                    }

                    // Inserção de novos registros
                    while (duracao > 0)
                    {
                        DataRow newRow = rs.NewRow();
                        newRow["Seq"] = seq_aux;
                        newRow["NumPag"] = NumPag;
                        newRow["CodEvento"] = CodEvento;
                        newRow["CodCanal1"] = CodCanal1;
                        newRow["CodCanal2"] = CodCanal2;
                        newRow["Inicio"] = Inicio;
                        if (Inicio + duracao > sizepag)
                        {
                            newRow["Duracao"] = sizepag - Inicio;
                        }
                        else
                        {
                            newRow["Duracao"] = duracao;
                        }
                        if (MenorSat != 0) newRow["MenorSat"] = MenorSat;
                        newRow["Posicao"] = Posicao;

                        rs.Rows.Add(newRow);

                        Inicio = 0;
                        duracao -= (int)newRow["Duracao"];
                        NumPag++;
                    }

                    // Atualiza o DataTable com as alterações
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter);
                    adapter.Update(rs);
                    /*
                    string connectionStringDatBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";
                    using var connectionDatBd = new OdbcConnection(connectionStringDatBd);
                    string query = "SELECT * FROM tbl_Eventos";
                    using var command = new OdbcCommand(query, connectionDatBd);
                    using var adapterEventosDtNormal = new OdbcDataAdapter(command);
                    GlobVar.eventos.Clear();
                    adapterEventosDtNormal.Fill(GlobVar.eventos);
                    connectionDatBd.Close();
                    */
                }

                return seq_aux;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return -1;
            }
        }
        public static int ExcluiEventoSeq(int Seq)
        {
            try
            {
                int i = -1;

                string queryDelete = $"DELETE FROM tbl_Eventos WHERE Seq = {Seq};";

                using var DeleteCommand = new OleDbCommand(queryDelete, GlobVar.ConnectionBDdat);

                DeleteCommand.ExecuteNonQuery();

                return i;
            }
            catch { int i = 0; return i; }
        }

        private void DeleteRow(int seq)
        {
            DataView view = new DataView(DtMovimentoPnPlm);
            view.RowFilter = $"Seq = {seq}";

            if(view.Count > 0)
            {
                DataRow rowToRemove = view[0].Row;
                DtMovimentoPnPlm.Rows.Remove(rowToRemove);
            }
            DtMovimentoPnPlm.AcceptChanges();
        }


    }
}
