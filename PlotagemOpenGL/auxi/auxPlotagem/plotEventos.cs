using Accord.Math;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace PlotagemOpenGL.auxi.auxPlotagem
{
    public static class plotEventos
    {
        public static void DesenhaEventos(int qtdGraf, OpenGL gl, float[] desenhoLoc)
        {
            
            DataTable eventos = GlobVar.eventosUpdate;

            var filteredRows = eventos.AsEnumerable()
                          .OrderBy(row => row.Field<int>("Seq"));

            // Agrupar por Seq e processar cada grupo
            var groupedRows = filteredRows.GroupBy(row => row.Field<int>("Seq"));
            int des = qtdGraf - 1;
            foreach (var group in groupedRows)
            {
                var firstRow = group.First();
                int codCanal1First = firstRow.Field<int>("CodCanal1");
                int inicio = firstRow.Field<int>("Inicio");
                int termino = firstRow.Field<int>("Duracao");
                if (GlobVar.codSelected.Contains(codCanal1First)) {
                    int YAdjusted = EncontrarValorMaisProximo(desenhoLoc, desenhoLoc[GlobVar.codSelected.IndexOf(codCanal1First)]);

                    gl.Begin(OpenGL.GL_QUADS);
                    gl.PointSize(3.0f); // Define o tamanho dos pontos
                    gl.Color(GlobVar.colors[GlobVar.codSelected.IndexOf(codCanal1First)].X, GlobVar.colors[GlobVar.codSelected.IndexOf(codCanal1First)].Y, GlobVar.colors[GlobVar.codSelected.IndexOf(codCanal1First)].Z, 0.001);
                    //gl.ColorMask(3, 6, 7, alpha);
                    gl.Vertex(inicio, Plotagem.StartY[YAdjusted] + 5, -1.9f);
                    gl.Vertex(termino, Plotagem.StartY[YAdjusted] + 5, -1.9f);
                    gl.Vertex(termino, Plotagem.EndY[YAdjusted] - 5, -1.9f);
                    gl.Vertex(inicio, Plotagem.EndY[YAdjusted] - 5, -1.9f);                    
                    gl.End();
                    des--;
                }
            }
        }
        public static int EncontrarValorMaisProximo(float[] valores, float y)
        {
            float valorMaisProximo = valores[0];
            float menorDiferenca = Math.Abs(valores[0] - y);

            foreach (float valor in valores)
            {
                float diferencaAtual = Math.Abs(valor - y);

                if (diferencaAtual < menorDiferenca)
                {
                    menorDiferenca = diferencaAtual;
                    valorMaisProximo = valor;
                }
            }
            int index = Array.IndexOf(valores, valorMaisProximo);
            return index;
        }

    }
}
