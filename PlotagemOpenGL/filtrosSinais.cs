using System;
using Accord.Audio.Filters;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;
using System.Text;
using DSP;
using PlotagemOpenGL.auxi;
using Accord.Audio;
using static OpenTK.Graphics.OpenGL.GL;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Accord.Math;

namespace PlotagemOpenGL
{
    public class filtrosSinais
    {
        const double w0 = 0.5;
        const double w1 = -0.5;
        const double s0 = 0.5;
        const double s1 = 0.5;
        public static double[] alterado;

        

        public static void FWT(double[] valorout)
        {
            alterado = new double[valorout.Length];
            double[] temp = new double[valorout.Length];


            for (int itrt = 0; itrt <= 1; itrt++)
            {
                int h = valorout.Length >> 1;
                for (int i = 0; i < h; i++)
                {
                    int k = (i << 1);
                    temp[i] = valorout[k] * s0 + valorout[k + 1] * s1;
                    temp[i + h] = valorout[k] * w0 + valorout[k + 1] * w1;
                }

            }
            alterado = temp;
        }
        public static void VoltaMatriz(Int16 select)
        {
            int colunaCanalIndex = 0;

            for (int linhaComp = 0; linhaComp < GlobVar.matrizCompleta.GetLength(0); linhaComp++)
            {
                // Percorre as colunas da matrizCompleta no intervalo especificado por pontI e pontF 
                for (int colunaComp = GlobVar.ponteiroI[select]; colunaComp < GlobVar.ponteiroF[select]; colunaComp++)
                {
                    // Certifique-se de não exceder os limites da matrizCanais
                    if (colunaCanalIndex < GlobVar.matrizCanal.GetLength(1))
                    {
                        // Copia o valor de matrizCompleta para matrizCanal
                        GlobVar.matrizCanal[select, colunaCanalIndex] = Convert.ToInt16(GlobVar.matrizCompleta[linhaComp, colunaComp]);
                        colunaCanalIndex++;
                    }
                    else
                    {
                        // Se exceder os limites da matrizCanal, saia do loop
                        break;
                    }
                }
            }
            /*for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                if (GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_Montagem.Rows[i]["CodCanal1"]))] < 512)
                {
                    int aux = 512 / GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_Montagem.Rows[i]["CodCanal1"]))];
                    GlobVar.matrizCanal.SetRow<short>(i, LeituraEmMatrizTeste.RemoverMetadeParaFrente(GlobVar.matrizCanal.GetRow<short>(i), aux));
                }
            }*/
            for (int i = 0; i < GlobVar.scale.Length; i++)
            {
                GlobVar.scale[i] = 0.01f;
            }


        }
        public static void FiltraTodoSinal(float[] input, float alpha, int escolha)
        {

        }

    }
    public class LowPassFilter
    {
        public static int auxLow;
        private float _alpha;
        private float _prevOutput;
        public static Accord.Audio.Filters.LowPassFilter lowFilt;

        public LowPassFilter(float alpha)
        {
            _alpha = alpha;
            _prevOutput = 0;
        }

        public float Apply(float input)
        {
            float output = _alpha * input + (1 - _alpha) * _prevOutput;
            _prevOutput = output;
            return output;
        }

        public static short[] ApplyLowPassFilter(short[] input, float alpha, int rate)
        {
            short[] output = new short[input.Length];
            lowFilt = new Accord.Audio.Filters.LowPassFilter(alpha * 1000, rate);
            Signal sig = Signal.FromArray(input, rate);
            lowFilt.Apply(sig);
            float[] filteredSignal = sig.ToFloat();

            LowPassFilter lowPassFilter = new LowPassFilter(alpha);

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (short)lowPassFilter.Apply(filteredSignal[i]);
            }
            return output;
        }
    }
    public class HighPassFilter
    {
        public static int auxHigh;
        private float _alpha;
        private float _prevInput;
        private float _prevOutput;
        public static Accord.Audio.Filters.HighPassFilter highFilt;

        public HighPassFilter(float alpha)
        {
            _alpha = alpha;
            _prevInput = 0;
            _prevOutput = 0;
        }

        public float Apply(float input)
        {
            float output = _alpha * (_prevOutput + input - _prevInput);
            _prevInput = input;
            _prevOutput = output;
            return output;
        }

        public static float[] ApplyHighPassFilter(float[] input, float alpha, int rate)
        {
            HighPassFilter highPassFilter = new HighPassFilter(alpha);

            float[] output = new float[input.Length];
            highFilt = new Accord.Audio.Filters.HighPassFilter(alpha * 1000, rate);
            Signal sig = Signal.FromArray(input, rate);
            highFilt.Apply(sig);
            float[] filteredSignal = sig.ToFloat();

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = highPassFilter.Apply(filteredSignal[i]);
            }
            return output;
        }
    }
    public class BandPassFilter
    {
        private LowPassFilter _lowPassFilter;
        private HighPassFilter _highPassFilter;

        public BandPassFilter(float lowAlpha, float highAlpha)
        {
            _lowPassFilter = new LowPassFilter(lowAlpha);
            _highPassFilter = new HighPassFilter(highAlpha);
        }

        public float Apply(float input)
        {
            float lowPassOutput = _lowPassFilter.Apply(input);
            return _highPassFilter.Apply(lowPassOutput);
        }
        public static short[] ApplyBandPassFilter(short[] input, float lowAlpha, float highAlpha)
        {
            BandPassFilter bandPassFilter = new BandPassFilter(lowAlpha, highAlpha);
            short[] output = new short[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (short)bandPassFilter.Apply(input[i]);
            }
            return output;
        }

    }

}
