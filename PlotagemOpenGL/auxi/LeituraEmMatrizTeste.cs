using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using Accord.Math;
using System.Data;
using System.Linq;
using PlotagemOpenGL.Filtros;
using System.Threading.Tasks;
using System.Threading;


namespace PlotagemOpenGL.auxi
{
    internal class LeituraEmMatrizTeste
    {
        public static void LeituraDat()
        {
            int ntotal = 0;
            string[] dadoscanal = new string[47];
            byte[] buffer0 = new byte[395];
            byte[] buffer1 = new byte[8];
            byte[] buffer2 = new byte[8];
            byte[] buffer3 = new byte[47];
            GlobVar.nomeCanais = new string[GlobVar.qtdCanais.Length];
            GlobVar.txPorCanal = new int[GlobVar.qtdCanais.Length];
            GlobVar.ponteiroI = new int[GlobVar.qtdCanais.Length];
            GlobVar.ponteiroF = new int[GlobVar.qtdCanais.Length];
            GlobVar.scale = new double[GlobVar.qtdCanais.Length];
            GlobVar.codCanal = new int[GlobVar.qtdCanais.Length];
            GlobVar.grafSelected = new int[GlobVar.tbl_MontagemSelecionada.Rows.Count];
            GlobVar.codSelected = new int[GlobVar.tbl_MontagemSelecionada.Rows.Count];
            GlobVar.SomenteNums = new bool[2];
            int jj = 0;


            using (FileStream fs = new FileStream(GlobVar.textFile, FileMode.Open, FileAccess.Read))
            {
                fs.Read(buffer0, 0, buffer0.Length);

                GlobVar.cabecalho = (Encoding.UTF8.GetString(buffer0));

                fs.Read(buffer1, 0, buffer1.Length);

                string npag1 = (Encoding.UTF8.GetString(buffer1));

                GlobVar.npagin = Convert.ToInt32(npag1);

                GlobVar.npag = npag1.Replace(" ", "");

                fs.Read(buffer2, 0, buffer2.Length);

                string tipocanais1 = (Encoding.UTF8.GetString(buffer2));

                GlobVar.tipocanais = tipocanais1.Replace(" ", "");

                int ncanint = Convert.ToInt16(tipocanais1.Replace(" ", "")); //Int16.Parse(tipocanais, System.Globalization.NumberStyles.HexNumber);
                GlobVar.qtdCanais = new string[ncanint];
                int txPorSeg = 0;
                for (int ich = 0; ich < ncanint; ich++)
                {
                    fs.Read(buffer3, 0, buffer3.Length);

                    dadoscanal[ich] = (Encoding.UTF8.GetString(buffer3));

                    string cod = dadoscanal[ich].Substring(0, 2);
                    GlobVar.qtdCanais[ich] = cod;
                    string phrase = dadoscanal[ich];
                    GlobVar.codCanal[ich] = Convert.ToInt16(phrase.Substring(0, 3).Trim()); //Faz a leitura do codigo do canal
                    GlobVar.nomeCanais[ich] = phrase.Substring(11, 10).Trim(); //Faz a leitura dos nomes de cada canal e armazena em um array
                    GlobVar.amos = Convert.ToInt16(phrase.Substring(8, 4));
                    string sizesample3 = phrase.Substring(8, 4);
                    string aux = sizesample3.Replace(" ", "");
                    int auxx = Convert.ToInt16(aux);
                    GlobVar.txPorCanal[ich] = auxx;

                    GlobVar.startpos = Convert.ToInt32(ntotal);
                    string sizesample1 = phrase.Substring(8, 4);
                    string banana = sizesample1.Replace(" ", "");

                    GlobVar.sizesample = (Convert.ToInt16(banana) * 2);
                    ntotal = ntotal + (GlobVar.amos * 2);

                    int ponteirostr = Convert.ToInt16(fs.Position);
                    GlobVar.ponteiroI[ich] = txPorSeg;
                    txPorSeg += auxx;
                    GlobVar.ponteiroF[ich] = txPorSeg;

                }

                GlobVar.matrizCompleta = new short[GlobVar.npagin, txPorSeg];

                GlobVar.size = Convert.ToInt32(GlobVar.npag);

                int recntotal = ntotal;

                fs.Position = fs.Position - 1;

                for (Int16 ich1 = 0; ich1 < GlobVar.size; ich1++) //leitura de 1 segundo size e igual a quantos segundos tem no arquivo dat, trazendo a Matriz completa de todos canais juntos
                {

                    byte[] buffer4 = new byte[ntotal];

                    fs.Read(buffer4, 0, buffer4.Length);

                    short[] bitstring = new short[GlobVar.sizesample / 2];

                    var limite = GlobVar.startpos + GlobVar.sizesample;

                    for (Int16 i = 0, j = 0; i < ntotal; i += 2, j++)
                    {
                        GlobVar.matrizCompleta[ich1, j] = BitConverter.ToInt16(buffer4, i);

                    }
                }
                fs.Close();

                GlobVar.indiceDat = GlobVar.npagin * GlobVar.amos * 2;
                //int[] pontF = GlobVar.ponteiroF;
                GlobVar.matrizCanal = new short[GlobVar.tbl_MontagemSelecionada.Rows.Count, GlobVar.indiceDat];
                /*
                //Separa os valores de cada canal para a MatrizCanal, para poder desenhar eles separadamente
                for (int linhaCanais = 0; linhaCanais < GlobVar.tbl_MontagemSelecionada.Rows.Count; linhaCanais++)
                {
                    int colunaCanalIndex = 0;

                    // Percorre as linhas da matrizCompleta
                    for (int linhaComp = 0; linhaComp < GlobVar.matrizCompleta.GetLength(0); linhaComp++)
                    {
                        // Percorre as colunas da matrizCompleta no intervalo especificado por pontI e pontF 
                        for (int colunaComp = GlobVar.ponteiroI[GlobVar.codCanal.IndexOf((int)(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal1"]))]; colunaComp < GlobVar.ponteiroF[GlobVar.codCanal.IndexOf((int)(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal1"]))]; colunaComp++)
                        {
                            // Certifique-se de não exceder os limites da matrizCanais
                            if (colunaCanalIndex < GlobVar.matrizCanal.GetLength(1))
                            {
                                    // Copia o valor de matrizCompleta para matrizCanal
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(GlobVar.matrizCompleta[linhaComp, colunaComp]);
                                colunaCanalIndex++;

                            }
                            //else
                            //{
                                // Se exceder os limites da matrizCanal, saia do loop
                            //    break;
                            //}
                        }
                    }
                }*/
                int rowCount = GlobVar.tbl_MontagemSelecionada.Rows.Count;
                int segmentLength = GlobVar.matrizCanal.GetLength(1);

                int linhaCanais = 0;
                foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows){
                    int canalIndex = GlobVar.codCanal.IndexOf(Convert.ToInt16(row["CodCanal1"]));
                    int canal2Index = GlobVar.codCanal.IndexOf(Convert.ToInt16(row["CodCanal2"]));
                    if (canalIndex == -1) return;

                    int ponteiroI = GlobVar.ponteiroI[canalIndex];
                    int ponteiroF = GlobVar.ponteiroF[canalIndex];
                    int colunaCanalIndex = 0;

                        if (canal2Index == -1)
                        {
                            // Caso sem segundo canal
                            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                            {
                                int colunaComp = ponteiroI;
                                while (colunaComp < ponteiroF)
                                {
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                    colunaComp++;
                                    colunaCanalIndex++;
                                }
                            }
                        }
                        else
                        {
                            // Caso com segundo canal
                            int ponteiroI2 = GlobVar.ponteiroI[canal2Index];
                            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                            {
                                int colunaComp = ponteiroI;
                                int colunaCan2 = ponteiroI2;
                                while (colunaComp < ponteiroF)
                                {
                                    // Calcular a diferença entre valores das colunas de canais
                                    short valorColunaComp = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                    short valorColunaCan2 = (short)GlobVar.matrizCompleta[linha, colunaCan2];

                                    // Atribuir a diferença para a matriz de destino
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(valorColunaComp - valorColunaCan2);

                                    colunaComp++;
                                    colunaCan2++;
                                    colunaCanalIndex++;
                                }
                            }
                        }
                    linhaCanais++;
                }
                    reorganize();
                foreach (var row in GlobVar.tbl_MontagemSelecionada.AsEnumerable())
                {
                    if (row["CodCanal2"] == DBNull.Value)
                    {
                        row["CodCanal2"] = -1;
                    }
                }
                /*//Verifica se o canal da montagem tem referencia a outro canal do exame
                foreach (var row in GlobVar.tbl_MontagemSelecionada.AsEnumerable())
                {
                    int codCanal1 = row.Field<int>("CodCanal1");
                    int codCanal2 = Convert.ToInt32(row.Field<int>("CodCanal2"));
                    if (codCanal2 != -1)
                    {
                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(codCanal1), SetReferencia(codCanal1, codCanal2));
                    }
                }
                */
                /*//Verificacao se a filtro no canal
                int ind = 0;
                foreach (var row in GlobVar.tbl_MontagemSelecionada.AsEnumerable())
                {
                    
                    double? lowHertz = null;
                    double? highHertz = null;
                    double? notchHertz = null;

                    if (!row.IsNull("PassaBaixa"))
                    {
                        double passaBaixaValue = row.Field<double>("PassaBaixa");
                        if (passaBaixaValue != 0)
                        {
                            lowHertz = passaBaixaValue;
                        }
                    }

                    if (!row.IsNull("PassaAlta"))
                    {
                        double passaAltaValue = row.Field<double>("PassaAlta");
                        if (passaAltaValue != 0)
                        {
                            highHertz = passaAltaValue;
                        }
                    }

                    if (!row.IsNull("Notch"))
                    {
                        double notchValue = row.Field<double>("Notch");
                        if (notchValue != 0)
                        {
                            notchHertz = notchValue;
                        }
                    }

                    if (lowHertz != null)
                    {
                        if (highHertz != null)
                        {
                            GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])),
                            ShortToFloat(BandPass.ApplyFilter(FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])))),
                            (float)lowHertz,
                            (float)highHertz,
                            GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[ind])]
                            )));
                        }
                        else
                        {
                            GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])),
                                ShortToFloat(PaissaBaixa.ApplyFilter(FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])))), (float)lowHertz, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[ind])] )));
                        }
                    }
                    if (lowHertz == null && highHertz != null)
                    {
                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])),
                                    ShortToFloat(PaissaAlta.ApplyFilter(FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])))), (float)highHertz, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[ind])])));
                        
                    }

                    if (notchHertz != null)
                    {
                        if(lowHertz == null) { lowHertz = 0; }
                        if(highHertz == null) {  highHertz = 0; }

                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])),
                                ShortToFloat(Notch.ApplyFilter(FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])))), (float)notchHertz, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[ind])])));
                         
                    }
                    if (notchHertz != null || highHertz != null || lowHertz != null)
                    {
                        if (lowHertz == null) { lowHertz = 0; }
                        if (highHertz == null) { highHertz = 0; }
                        if (notchHertz == null) { notchHertz = 0; }
                        Tela_Plotagem.UpdateBeforeLoad(ind + 1, (double)lowHertz, (double)highHertz, (double)notchHertz);
                    }
                    ind++;
                }
                */
                foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
                {

                    int codCanal1 = Convert.ToInt16(row["CodCanal1"]);
                    int canalIndex = GlobVar.codCanal.IndexOf(codCanal1);
                    if (canalIndex == -1) return;

                    int selectedIndex = GlobVar.codSelected.IndexOf(codCanal1);
                    if (selectedIndex == -1) return;

                    double? lowHertz = row.IsNull("PassaBaixa") ? (double?)null : row.Field<double>("PassaBaixa");
                    double? highHertz = row.IsNull("PassaAlta") ? (double?)null : row.Field<double>("PassaAlta");
                    double? notchHertz = row.IsNull("Notch") ? (double?)null : row.Field<double>("Notch");

                    if (lowHertz.HasValue || highHertz.HasValue || notchHertz.HasValue)
                    {
                        var canalData = GlobVar.matrizCanal.GetRow(selectedIndex);
                        int txPorCanal = GlobVar.txPorCanal[canalIndex];
                        var dataToFilter = canalData;

                        if (lowHertz.HasValue && lowHertz.Value != 0)
                        {
                            if (highHertz.HasValue && highHertz.Value != 0)
                            {
                                dataToFilter = ShortToFloat(
                                    BandPass.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, (float)highHertz.Value, txPorCanal)
                                );
                            }
                            else
                            {
                                dataToFilter = ShortToFloat(
                                    PaissaBaixa.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, txPorCanal)
                                );
                            }
                        }
                        else if (highHertz.HasValue && highHertz.Value != 0)
                        {
                            dataToFilter = ShortToFloat(
                                PaissaAlta.ApplyFilter(FloatToShort(dataToFilter), (float)highHertz.Value, txPorCanal)
                            );
                        }

                        if (notchHertz.HasValue && notchHertz.Value != 0)
                        {
                            dataToFilter = ShortToFloat(
                                Notch.ApplyFilter(FloatToShort(dataToFilter), (float)notchHertz.Value, 10, txPorCanal)
                            );
                        }

                        Array.Copy(dataToFilter, 0, canalData, 0, dataToFilter.Length);
                        GlobVar.matrizCanal.SetRow(selectedIndex, canalData);
                    }
                }


                for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
                {
                    float scala = (float)( Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[i]["AmplitudeMin"]) / Ampli(CodTipo(i))) ;
                    GlobVar.scale[i] = scala;
                }
                
            }
        }
        public static void montagemSelecionadaAlterada()
        {
            int rowCount = GlobVar.tbl_MontagemSelecionada.Rows.Count;
            int startLength = GlobVar.indice;
            int segmentLength = 512 * 600; // GlobVar.matrizCanal.GetLength(1);
            int ln = startLength / GlobVar.namos;


            Tela_Plotagem.cronometro1.Reset();
            Tela_Plotagem.cronometro1.Start();

            // Paralelizar a cópia de dados para GlobVar.matrizCanal
            Parallel.For(0, rowCount, linhaCanais =>
            {
                int canalIndex = GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal1"]));
                int canal2Index = GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal2"]));
                if (canalIndex == -1) return;

                int ponteiroI = GlobVar.ponteiroI[canalIndex];
                int ponteiroF = GlobVar.ponteiroF[canalIndex];

                int txPorCanal = GlobVar.txPorCanal[canalIndex];
                segmentLength = txPorCanal * 600;

                int Start = ((startLength / GlobVar.namos)) * (int)txPorCanal;
                int colunaCanalIndex = Start;


                if (canal2Index == -1)
                {
                    // Caso sem segundo canal
                    for (int linha = ln; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                    {
                        int colunaComp = ponteiroI;
                        while (colunaComp < ponteiroF)
                        {
                            GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                            colunaComp++;
                            colunaCanalIndex++;
                            if(colunaCanalIndex > startLength + segmentLength) break;
                        }
                    }
                }
                else
                {
                    // Caso com segundo canal
                    int ponteiroI2 = GlobVar.ponteiroI[canal2Index];
                    for (int linha = ln; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                    {
                        int colunaComp = ponteiroI;
                        int colunaCan2 = ponteiroI2;

                        while (colunaComp < ponteiroF)
                        {
                            // Calcular a diferença entre valores das colunas de canais
                            short valorColunaComp = (short)GlobVar.matrizCompleta[linha, colunaComp];
                            short valorColunaCan2 = (short)GlobVar.matrizCompleta[linha, colunaCan2];

                            // Atribuir a diferença para a matriz de destino
                            GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(valorColunaComp - valorColunaCan2);
                            //DoEvents em vb
                            colunaComp++;
                            colunaCan2++;
                            colunaCanalIndex++;
                            if (colunaCanalIndex > startLength + segmentLength) break;

                        }
                    }
                }
            });
            Tela_Plotagem.cronometro1.Stop();


            reorganize();

            Tela_Plotagem.cronometro3.Reset();
            Tela_Plotagem.cronometro3.Start();

            // Atualizar CodCanal2 em paralelo
            Parallel.ForEach(GlobVar.tbl_MontagemSelecionada.AsEnumerable(), row =>
            {
                if (row["CodCanal2"] == DBNull.Value)
                {
                    row["CodCanal2"] = -1;
                }
            });


            Tela_Plotagem.cronometroBand.Reset();
            Tela_Plotagem.cronometroBaixa.Reset();
            Tela_Plotagem.cronometroAlta.Reset();
            Tela_Plotagem.cronometroNotch.Reset();

            // Aplicar filtros paralelamente
            Parallel.For(0, rowCount, ind =>
            {
                var row = GlobVar.tbl_MontagemSelecionada.Rows[ind];
                int codCanal1 = Convert.ToInt16(row["CodCanal1"]);
                int canalIndex = GlobVar.codCanal.IndexOf(codCanal1);

                if (canalIndex == -1) return;

                int selectedIndex = GlobVar.codSelected.IndexOf(codCanal1);
                if (selectedIndex == -1) return;

                double? lowHertz = row.IsNull("PassaBaixa") ? (double?)null : row.Field<double>("PassaBaixa");
                double? highHertz = row.IsNull("PassaAlta") ? (double?)null : row.Field<double>("PassaAlta");
                double? notchHertz = row.IsNull("Notch") ? (double?)null : row.Field<double>("Notch");

                if (lowHertz.HasValue || highHertz.HasValue || notchHertz.HasValue)
                {
                    

                    var canalData = GlobVar.matrizCanal.GetRow(selectedIndex);
                    int txPorCanal = GlobVar.txPorCanal[canalIndex];
                    segmentLength = txPorCanal * 600;

                    int skipLength = ((startLength / GlobVar.namos ) / 30) * (int)txPorCanal;
                    int startFiltLength = ((startLength / GlobVar.namos) / 30) * (int)txPorCanal;

                    int endIndex = Math.Min(segmentLength, canalData.Length);
                    var dataToFilter = canalData.Skip(skipLength).Take(endIndex - startFiltLength).ToArray();

                    if (lowHertz.HasValue && lowHertz.Value != 0)
                    {
                        if (highHertz.HasValue && highHertz.Value != 0)
                        {
                            dataToFilter = ShortToFloat(
                                BandPass.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, (float)highHertz.Value, txPorCanal)
                            );
                        }
                        else
                        {
                            dataToFilter = ShortToFloat(
                                PaissaBaixa.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, txPorCanal)
                            );
                        }
                    }
                    else if (highHertz.HasValue && highHertz.Value != 0)
                    {
                        dataToFilter = ShortToFloat(
                            PaissaAlta.ApplyFilter(FloatToShort(dataToFilter), (float)highHertz.Value, txPorCanal)
                        );
                    }

                    if (notchHertz.HasValue && notchHertz.Value != 0)
                    {
                        dataToFilter = ShortToFloat(
                            Notch.ApplyFilter(FloatToShort(dataToFilter), (float)notchHertz.Value, 10, txPorCanal)
                        );
                    }

                    // Atualizando apenas a parte específica em canalData
                    Array.Copy(dataToFilter, 0, canalData, startFiltLength, dataToFilter.Length);

                    // Agora, usamos SetRow para definir a linha inteira novamente
                    GlobVar.matrizCanal.SetRow(selectedIndex, canalData);

                    if (lowHertz.HasValue || highHertz.HasValue || notchHertz.HasValue)
                    {
                        Tela_Plotagem.UpdateBeforeLoad(ind + 1, lowHertz ?? 0, highHertz ?? 0, notchHertz ?? 0);
                    }
                }
            });
            Tela_Plotagem.cronometro3.Stop();

            // Paralelizar a atualização do array GlobVar.scale
            GlobVar.scale = new double[rowCount];
            Parallel.For(0, rowCount, i =>
            {
                float scala = (float)(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[i]["AmplitudeMin"]) / Ampli(CodTipo(i)));
                GlobVar.scale[i] = scala;
            });
        }

        public static void montagemSelecionadaAlteradaTudo(CancellationToken token)
        {
            try {
            int rowCount = GlobVar.tbl_MontagemSelecionada.Rows.Count;
            int segmentLength = GlobVar.matrizCanal.GetLength(1);

            Tela_Plotagem.cronometro1.Reset();
            Tela_Plotagem.cronometro1.Start();

            // Paralelizar a cópia de dados para GlobVar.matrizCanal
            Parallel.For(0, rowCount, (linhaCanais, state) =>
            {
                // Verificar o token no início do loop paralelo
                if (token.IsCancellationRequested)
                {
                    state.Stop();
                    return;
                }

                int canalIndex = GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal1"]));
                int canal2Index = GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal2"]));
                if (canalIndex == -1) return;

                int ponteiroI = GlobVar.ponteiroI[canalIndex];
                int ponteiroF = GlobVar.ponteiroF[canalIndex];
                int colunaCanalIndex = 0;

                if (canal2Index == -1)
                {
                    // Caso sem segundo canal
                    for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                    {
                        // Verificar o token dentro do loop de processamento mais intenso
                        if (token.IsCancellationRequested)
                            return;

                        int colunaComp = ponteiroI;
                        while (colunaComp < ponteiroF)
                        {
                            GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                            colunaComp++;
                            colunaCanalIndex++;
                        }
                    }
                }
                else
                {
                    // Caso com segundo canal
                    int ponteiroI2 = GlobVar.ponteiroI[canal2Index];
                    for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                    {
                        // Verificar o token dentro do loop de processamento mais intenso
                        if (token.IsCancellationRequested)
                            return;

                        int colunaComp = ponteiroI;
                        int colunaCan2 = ponteiroI2;
                        while (colunaComp < ponteiroF)
                        {
                            // Calcular a diferença entre valores das colunas de canais
                            short valorColunaComp = (short)GlobVar.matrizCompleta[linha, colunaComp];
                            short valorColunaCan2 = (short)GlobVar.matrizCompleta[linha, colunaCan2];

                            // Atribuir a diferença para a matriz de destino
                            GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(valorColunaComp - valorColunaCan2);

                            colunaComp++;
                            colunaCan2++;
                            colunaCanalIndex++;
                        }
                    }
                }
            });

            // Mais uma verificação de cancelamento após a cópia
            if (token.IsCancellationRequested)
                return;

            Tela_Plotagem.cronometro1.Stop();
            reorganize();

            Tela_Plotagem.cronometro3.Reset();
            Tela_Plotagem.cronometro3.Start();

            // Atualizar CodCanal2 em paralelo
            Parallel.ForEach(GlobVar.tbl_MontagemSelecionada.AsEnumerable(), (row, state) =>
            {
                if (token.IsCancellationRequested)
                {
                    state.Stop();
                    return;
                }

                if (row["CodCanal2"] == DBNull.Value)
                {
                    row["CodCanal2"] = -1;
                }
            });

            Tela_Plotagem.cronometro3.Stop();

            if (token.IsCancellationRequested)
                return;

                // Aplicar filtros paralelamente
                foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
                {
                    if (token.IsCancellationRequested)
                        return;

                    int codCanal1 = Convert.ToInt16(row["CodCanal1"]);
                    int canalIndex = GlobVar.codCanal.IndexOf(codCanal1);
                    if (canalIndex == -1) return;

                    int selectedIndex = GlobVar.codSelected.IndexOf(codCanal1);
                    if (selectedIndex == -1) return;

                    double? lowHertz = row.IsNull("PassaBaixa") ? (double?)null : row.Field<double>("PassaBaixa");
                    double? highHertz = row.IsNull("PassaAlta") ? (double?)null : row.Field<double>("PassaAlta");
                    double? notchHertz = row.IsNull("Notch") ? (double?)null : row.Field<double>("Notch");

                    if (lowHertz.HasValue || highHertz.HasValue || notchHertz.HasValue)
                    {
                        var canalData = GlobVar.matrizCanal.GetRow(selectedIndex);
                        int txPorCanal = GlobVar.txPorCanal[canalIndex];
                        var dataToFilter = canalData;

                        if (lowHertz.HasValue && lowHertz.Value != 0)
                        {
                            if (highHertz.HasValue && highHertz.Value != 0)
                            {
                                dataToFilter = ShortToFloat(
                                    BandPass.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, (float)highHertz.Value, txPorCanal)
                                );
                            }
                            else
                            {
                                dataToFilter = ShortToFloat(
                                    PaissaBaixa.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, txPorCanal)
                                );
                            }
                        }
                        else if (highHertz.HasValue && highHertz.Value != 0)
                        {
                            dataToFilter = ShortToFloat(
                                PaissaAlta.ApplyFilter(FloatToShort(dataToFilter), (float)highHertz.Value, txPorCanal)
                            );
                        }

                        if (notchHertz.HasValue && notchHertz.Value != 0)
                        {
                            dataToFilter = ShortToFloat(
                                Notch.ApplyFilter(FloatToShort(dataToFilter), (float)notchHertz.Value, 10, txPorCanal)
                            );
                        }

                        Array.Copy(dataToFilter, 0, canalData, 0, dataToFilter.Length);
                        GlobVar.matrizCanal.SetRow(selectedIndex, canalData);
                    }
                }
            }
            catch { return; }
        }

        public static short[] SetReferencia(int principal, int referencia)
        {
            int numColunas = GlobVar.matrizCanal.GetLength(1);
            short[] novoArray = new short[numColunas];
            short[] sla = Referencia(referencia);

            // Se o primeiro elemento for zero, retorna o novoArray vazio
            if (sla[0] == 0)
            {
                return novoArray;
            }

            // Obtem o índice do canal principal uma vez, em vez de chamá-lo repetidamente dentro do loop
            int indexPrincipal = GlobVar.codSelected.IndexOf(principal);

            // Calcula o novoArray com a referência
            for (int i = 0; i < numColunas && i < novoArray.Length; i++)
            {
                novoArray[i] = (short)(GlobVar.matrizCanal[indexPrincipal, i] - sla[i]);
            }

            return novoArray;
        }

        public static short[] Referencia(int codReferencia)
        {
            int numColunas = GlobVar.matrizCanal.GetLength(1);
            short[] referencia = new short[numColunas];

            // Verifica se o código de referência existe
            int indexCodReferencia = GlobVar.codCanal.IndexOf(codReferencia);
            if (indexCodReferencia == -1)
            {
                return referencia;
            }

            // Pega os índices de início e fim uma vez
            int startCol = GlobVar.ponteiroI[indexCodReferencia];
            int endCol = GlobVar.ponteiroF[indexCodReferencia];

            // Copia os valores de matrizCompleta para o array referencia
            int pontRef = 0;
            for (int linhaComp = 0; linhaComp < GlobVar.matrizCompleta.GetLength(0) && pontRef < numColunas; linhaComp++)
            {
                for (int colunaComp = startCol; colunaComp < endCol && pontRef < numColunas; colunaComp++)
                {
                    referencia[pontRef++] = (short)GlobVar.matrizCompleta[linhaComp, colunaComp];
                }
            }

            return referencia;
        }
        public static short[] RemoverMetadeParaFrente(short[] array, int vezes)
        {
            int novaTamanho = array.Length / vezes;
            short[] novoArray = new short[novaTamanho];

            for (int i = 0; i < novaTamanho; i++)
            {
                novoArray[i] = array[i];
            }
            novoArray = DuplicarArray(novoArray, vezes);
            return novoArray;
        }
        public static short[] DuplicarArray(short[] array, int multiplicacao)
        {
            // Cria um novo array com o tamanho necessário
            short[] novoArray = new short[array.Length * multiplicacao];

            // Itera sobre os elementos do array original
            for (int i = 0; i < array.Length; i++)
            {
                // Duplica o valor do elemento atual no novo array
                for (int j = 0; j < multiplicacao; j++)
                {
                    novoArray[i * multiplicacao + j] = array[i];
                }
            }

            return novoArray;
        }
        public static void reorganize()
        {
            GlobVar.grafSelected.Clear();
            GlobVar.codSelected.Clear();

            GlobVar.grafSelected = new int[GlobVar.tbl_MontagemSelecionada.Rows.Count];
            GlobVar.codSelected = new int[GlobVar.tbl_MontagemSelecionada.Rows.Count];

            for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                GlobVar.grafSelected[i] = i;
                GlobVar.codSelected[i] = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[i]["CodCanal1"]);
            }
        }
        public static float[] FloatToShort(short[] input)
        {
            float[] output = new float[input.Length];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = input[i];
            }

            return output;
        }
        public static short[] ShortToFloat(float[] input)
        {
            short[] output = new short[input.Length];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (short)input[i];
            }

            return output;
            
        }
        public static string CodTipo(int Index)
        {
            string output = "";
            int codTipo = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[Index]["CodTipoCanal"]);
            var row = GlobVar.tbl_CadTipoCanal.AsEnumerable().Where(r => r.Field<int>("CodTipo") == codTipo).FirstOrDefault();
            string codSigla = row.Field<string>("Sigla");
            

            if (codSigla.Equals("AIRFLOW"))
                    output = "g_airflow";
                else if (codSigla.Equals("CANULA")) 
            output = "g_canula";
                else if (codSigla.Equals("BODYPOS")) 
            output = "g_bodypos";
                else if (codSigla.Equals("SAO2")) 
            output = "g_sao2";
                else if (codSigla.Equals("CINTA_ABDOM")) 
            output = "g_abdom";
               else if (codSigla.Equals("CINTA_TORAX")) 
            output = "g_torax";
               else if (codSigla.Equals("PES")) 
            output = "g_pes";
               else if (codSigla.Equals("MICROFONE")) 
            output = "g_microf";
               else if (codSigla.Equals("FOTO_ESTIM")) 
            output = "g_foto";
               else if (codSigla.Equals("CPAP")) 
            output = "g_cpap";
               else if (codSigla.Equals("BPAP")) 
            output = "g_epap";
               else if (codSigla.Equals("CAP_RR")) 
            output = "g_cap_RR";
              else if (codSigla.Equals("CAP_CO2")) 
            output = "g_cap_CO2";
              else if (codSigla.Equals("CAP_ETCO2")) 
            output = "g_cap_EtCO2";
              else if (codSigla.Equals("SAO2_SERIAL")) 
            output = "g_sao2_ser";
             else if (codSigla.Equals("FC_SERIAL")) 
            output = "g_fc_ser";
              else if (codSigla.Equals("EMG_PERNA")) 
            output = "g_perna";
              else if (codSigla.Equals("PA_MAX")) 
            output = "g_pamax";
              else if (codSigla.Equals("PA_MIN")) 
            output = "g_pamin";
             else if (codSigla.Equals("PH_1")) 
            output = "g_ph1";
              else if (codSigla.Equals("PH_2")) 
            output = "g_ph2";
             else if (codSigla.Equals("PH_3")) 
            output = "g_ph3";
              else if (codSigla.Equals("CPAP_VAZ")) 
            output = "g_cpap_vaz";
              else if (codSigla.Equals("CO2_EXALADADO")) 
            output = "g_co2_exal";
              else if (codSigla.Equals("CO2_INSPIRADO")) 
            output = "g_co2_insp";
              else if (codSigla.Equals("FREQ_RESP")) 
            output = "g_fresp";
             else if (codSigla.Equals("PLET_OXIM")) 
            output = "g_plet";
             else if (codSigla.Equals("EEG")) 
            output = "g_eeg";
              else if (codSigla.Equals("ECG")) 
            output = "g_ecg";
              else if (codSigla.Equals("EMG_QUEIXO")) 
            output = "g_queixo";
              else if (codSigla.Equals("OCULOGRAMA")) 
           output = "g_olhos"; 
              else if (codSigla.Equals("CAN_RONCO")) 
            output = "g_can_ronco";
              else if (codSigla.Equals("PTT")) 
            output = "g_PTT";

            return output;
        }
        public static float Ampli(string codTipo)
        {
            float output = 1;
            try
            {
                float g_amp_eeg = 1.4f;
                float g_amp_emg = 5.5f;
                float g_amp_ecg = 1f;
                float g_amp_ronco = 7f;
                float g_amp_fx = 20f;
                float g_amp_af = 45f;
                float g_amp_canulaFL = 10f;
                float g_amp_canulaRC = 2.2f;

                

                if (codTipo.Equals("g_eeg") || codTipo.Equals("g_olhos"))
                {
                    output = g_amp_eeg;
                }
                else if (codTipo.Equals("g_ecg"))
                    output = g_amp_ecg;
                else if (codTipo.Equals("g_perna") || codTipo.Equals("g_queixo"))
                    output = g_amp_emg;
                else if (codTipo.Equals("g_airflow"))
                    output = g_amp_af;
                else if (codTipo.Equals("g_abdom") || codTipo.Equals("g_torax"))
                    output = g_amp_fx;
                else if (codTipo.Equals("g_canula"))
                    output = g_amp_canulaFL;
                else if (codTipo.Equals("g_microf"))
                    output = g_amp_ronco;
                else if (codTipo.Equals("g_can_ronco"))
                    output = g_amp_canulaRC;
                else if (codTipo.Equals("g_plet"))
                    output = 12f;
                else if (codTipo.Equals("g_sao2_ser"))
                    output = output;

                
            }
            catch { }
            return output;
        }

        public static void referencias()
        {
            // Usa um HashSet para garantir que os valores sejam únicos
            HashSet<int> referenciasUnicas = new HashSet<int>();

            // Itera pelas linhas do DataTable
            for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                // Converte o valor da coluna "CodCanal2" para int
                int valor = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["CodCanal2"]);

                // Apenas adiciona valores que não sejam -1
                if (valor != -1)
                {
                    referenciasUnicas.Add(valor);
                }
            }

            // Converte o HashSet para um array e armazena em GlobVar.canaisReferencia
            GlobVar.canaisReferencia = referenciasUnicas.ToArray();
            GlobVar.nomeReferencia = new string[GlobVar.canaisReferencia.Length];

            for(int i = 0; i < GlobVar.nomeReferencia.Length ; i++)
            {
                var row = GlobVar.tbl_CadCanal.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodCanal") == GlobVar.canaisReferencia[i]);
                GlobVar.nomeReferencia[i] = row["NomeCanal"].ToString();
            }

        }
    }
}

