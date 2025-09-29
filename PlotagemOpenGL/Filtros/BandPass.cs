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
        /*
        public float Apply(float input)
        {
            //float lowPassOutput = _lowPassFilter.Apply(input);
            //return _highPassFilter.Apply(lowPassOutput);
        }*/

        public static float[] ApplyFilter(float[] input, float lowCutoffFrequency, float highCutoffFrequency, float samplingRate)
        {
            // Calcula os coeficientes para os filtros
            float baixaAlpha = PaissaBaixa.CalculateAlpha(lowCutoffFrequency, samplingRate);
            float altaAlpha = PaissaAlta.CalculateAlpha(highCutoffFrequency, samplingRate);

            // Variáveis de estado para os filtros
            float baixaPrevOut = 0;
            float altaPrevOut = 0;
            float altaPrevIn = 0;

            //float[] output = new float[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                // Aplica o filtro passa-baixa primeiro
                float passaBaixa = baixaAlpha * input[i] + (1 - baixaAlpha) * baixaPrevOut;
                baixaPrevOut = passaBaixa; // Atualiza a saída anterior

                // Aplica o filtro passa-alta sobre a saída do passa-baixa
                input[i] = altaAlpha * (altaPrevOut + passaBaixa - altaPrevIn);
                altaPrevOut = input[i]; // Atualiza saída anterior
                altaPrevIn = passaBaixa; // Atualiza entrada anterior
            }
            return input;
        }
    }
}
