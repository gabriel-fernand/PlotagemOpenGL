using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotagemOpenGL.Filtros
{
    internal class BandPass
    {
        private PaissaBaixa _lowPassFilter;
        private PaissaAlta _highPassFilter;

        public BandPass(float lowCutoffFrequency, float highCutoffFrequency, float samplingRate = 512f)
        {
            _lowPassFilter = new PaissaBaixa(lowCutoffFrequency, samplingRate);
            _highPassFilter = new PaissaAlta(highCutoffFrequency, samplingRate);
        }

        public float Apply(float input)
        {
            float lowPassOutput = _lowPassFilter.Apply(input);
            return _highPassFilter.Apply(lowPassOutput);
        }

        public static float[] ApplyFilter(float[] input, float lowCutoffFrequency, float highCutoffFrequency, float samplingRate)
        {
            Tela_Plotagem.cronometroBand.Start();

            //BandPass bandPassFilter = new BandPass(lowCutoffFrequency, highCutoffFrequency, samplingRate);

            //calcula o passa baixa ---
            float baixaAlpha = PaissaBaixa.CalculateAlpha(lowCutoffFrequency, samplingRate);
            float baixaPrevOut = 0;
            //calcula o passa alta -----
            float altaAlpha = PaissaAlta.CalculateAlpha(highCutoffFrequency, samplingRate);
            float altaPrevOut = 0;
            float altaPrevIn = 0;

            float[] output = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = baixaAlpha * input[i] + (1 - baixaAlpha) * baixaPrevOut;
                baixaPrevOut = input[i];

                output[i] = altaAlpha * (altaPrevOut + input[i] - altaPrevIn);
                altaPrevOut = output[i];
                altaPrevIn = input[i];

            }
            Tela_Plotagem.cronometroBand.Stop();
            return output;
        }
    }
}
