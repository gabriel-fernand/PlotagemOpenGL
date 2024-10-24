using PlotagemOpenGL.auxi;
using System;

namespace PlotagemOpenGL.Filtros
{
    public class BandPass
    {
        private PaissaBaixa _lowPassFilter;
        private PaissaAlta _highPassFilter;

        public BandPass(float lowCutoffFrequency, float highCutoffFrequency, float samplingRate = 512f)
        {
            _lowPassFilter = new PaissaBaixa(lowCutoffFrequency, samplingRate);
            _highPassFilter = new PaissaAlta(highCutoffFrequency, samplingRate);
        }

        // Aplica o filtro em uma amostra individual
        public float Apply(float input)
        {
            float lowPassOutput = _lowPassFilter.Apply(input);
            return _highPassFilter.Apply(lowPassOutput);
        }

        // Aplica o filtro em um bloco de dados de uma só vez, melhorando a eficiência
        public float[] ApplyFilter(float[] input)
        {
            float[] output = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = Apply(input[i]);
            }
            return output;
        }

        // Método estático otimizado para aplicação do filtro passa-banda
        public static float[] ApplyFilter(float[] input, float lowCutoffFrequency, float highCutoffFrequency, float samplingRate)
        {
            BandPass bandPassFilter = new BandPass(lowCutoffFrequency, highCutoffFrequency, samplingRate);
            return bandPassFilter.ApplyFilter(input);
        }
    }
}
