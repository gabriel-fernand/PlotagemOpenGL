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
using System.Data;

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
            int ponteiro = GlobVar.codCanal.IndexOf(select);

            for (int linhaComp = 0; linhaComp < GlobVar.matrizCompleta.GetLength(0); linhaComp++)
            {
                // Percorre as colunas da matrizCompleta no intervalo especificado por pontI e pontF 
                for (int colunaComp = GlobVar.ponteiroI[ponteiro]; colunaComp < GlobVar.ponteiroF[ponteiro]; colunaComp++)
                {
                    // Certifique-se de não exceder os limites da matrizCanais
                    if (colunaCanalIndex < GlobVar.matrizCanal.GetLength(1))
                    {
                        // Copia o valor de matrizCompleta para matrizCanal
                        GlobVar.matrizCanal[GlobVar.codSelected.IndexOf(select), colunaCanalIndex] = Convert.ToInt16(GlobVar.matrizCompleta[linhaComp, colunaComp]);
                        colunaCanalIndex++;
                    }
                    else
                    {
                        // Se exceder os limites da matrizCanal, saia do loop
                        break;
                    }
                }
            }
            /*
            foreach (var row in GlobVar.tbl_MontagemSelecionada.AsEnumerable())
            {
                int codCanal1 = select;
                int codCanal2 = row.Field<int>("CodCanal2");
                if (codCanal2 != -1)
                {
                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(codCanal1), LeituraEmMatrizTeste.SetReferencia(codCanal1, codCanal2));
                }
            }
            
            */
            /*for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                if (GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_Montagem.Rows[i]["CodCanal1"]))] < 512)
                {
                    int aux = 512 / GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_Montagem.Rows[i]["CodCanal1"]))];
                    GlobVar.matrizCanal.SetRow<short>(i, LeituraEmMatrizTeste.RemoverMetadeParaFrente(GlobVar.matrizCanal.GetRow<short>(i), aux));
                }
            }*/

        }

    }
    public class LowPassFilter
    {
        public static int auxLow;
        private float _alpha;
        private float _prevOutput;

        public LowPassFilter(float cutoffFrequency, float samplingRate)
        {
            _alpha = CalculateAlpha(cutoffFrequency, samplingRate);
            _prevOutput = 0;
        }

        private float CalculateAlpha(float cutoffFrequency, float samplingRate)
        {
            float omega = 2 * (float)Math.PI * cutoffFrequency;
            return omega / (omega + samplingRate);
        }

        public float Apply(float input)
        {
            float output = _alpha * input + (1 - _alpha) * _prevOutput;
            _prevOutput = output;
            return output;
        }

        public static float[] ApplyLowPassFilter(float[] input, float cutoffFrequency, float samplingRate)
        {
            LowPassFilter lowPassFilter = new LowPassFilter(cutoffFrequency, samplingRate);

            float[] output = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = lowPassFilter.Apply(input[i]);
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

        public HighPassFilter(float cutoffFrequency, float samplingRate)
        {
            _alpha = CalculateAlpha(cutoffFrequency, samplingRate);
            _prevInput = 0;
            _prevOutput = 0;
        }

        private float CalculateAlpha(float cutoffFrequency, float samplingRate)
        {
            float omega = 2 * (float)Math.PI * cutoffFrequency;
            return omega / (omega + samplingRate);
        }

        public float Apply(float input)
        {
            float output = _alpha * (_prevOutput + input - _prevInput);
            _prevInput = input;
            _prevOutput = output;
            return output;
        }

        public static float[] ApplyHighPassFilter(float[] input, float cutoffFrequency, float samplingRate)
        {
            HighPassFilter highPassFilter = new HighPassFilter(cutoffFrequency, samplingRate);

            float[] output = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = highPassFilter.Apply(input[i]);
            }
            return output;
        }
    }
    public class BandPassFilter
    {
        private LowPassFilter _lowPassFilter;
        private HighPassFilter _highPassFilter;

        public BandPassFilter(float lowCutoffFrequency, float highCutoffFrequency, float samplingRate = 512f)
        {
            _lowPassFilter = new LowPassFilter(lowCutoffFrequency, samplingRate);
            _highPassFilter = new HighPassFilter(highCutoffFrequency, samplingRate);
        }

        public float Apply(float input)
        {
            float lowPassOutput = _lowPassFilter.Apply(input);
            return _highPassFilter.Apply(lowPassOutput);
        }

        public static float[] ApplyBandPassFilter(float[] input, float lowCutoffFrequency, float highCutoffFrequency, float samplingRate)
        {
            BandPassFilter bandPassFilter = new BandPassFilter(lowCutoffFrequency, highCutoffFrequency, samplingRate);
            float[] output = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = bandPassFilter.Apply(input[i]);
            }
            return output;
        }

    }
    public class NotchFilter
    {
        private LowPassFilter _lowPassFilter;
        private HighPassFilter _highPassFilter;
        private bool _hasLowCutoff;
        private bool _hasHighCutoff;

        public NotchFilter(float lowCutoffFrequency, float highCutoffFrequency, float samplingRate = 512f)
        {
            _hasLowCutoff = lowCutoffFrequency > 0f;
            _hasHighCutoff = highCutoffFrequency > 0f;

            if (_hasLowCutoff)
                _lowPassFilter = new LowPassFilter(lowCutoffFrequency, samplingRate);

            if (_hasHighCutoff)
                _highPassFilter = new HighPassFilter(highCutoffFrequency, samplingRate);
        }

        public float Apply(float input)
        {
            if (_hasLowCutoff && _hasHighCutoff)
            {
                float lowPassOutput = _lowPassFilter.Apply(input);
                return _highPassFilter.Apply(lowPassOutput);
            }
            else if (_hasLowCutoff)
            {
                return _lowPassFilter.Apply(input);
            }
            else if (_hasHighCutoff)
            {
                return _highPassFilter.Apply(input);
            }
            else
            {
                return input; // Sem filtro aplicado
            }
        }

        public static float[] ApplyNotchFilter(float[] input, float lowCutoffFrequency, float highCutoffFrequency, float samplingRate)
        {
            NotchFilter notchFilter = new NotchFilter(lowCutoffFrequency, highCutoffFrequency, samplingRate);
            float[] output = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = notchFilter.Apply(input[i]);
            }
            return output;
        }
    }
}
