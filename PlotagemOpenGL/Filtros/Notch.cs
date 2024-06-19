using Accord.Audio.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotagemOpenGL.Filtros
{
    internal class Notch
    {
        private PaissaBaixa _lowPassFilter;
        private PaissaAlta _highPassFilter;

        public Notch(float notchFrequency, float bandwidth, float samplingRate)
        {
            // Calcula as frequências de corte
            float lowCutoffFrequency = notchFrequency - bandwidth / 2;
            float highCutoffFrequency = notchFrequency + bandwidth / 2;

            _lowPassFilter = new PaissaBaixa(lowCutoffFrequency, samplingRate);
            _highPassFilter = new PaissaAlta(highCutoffFrequency, samplingRate);
        }

        public float Apply(float input)
        {
            // Aplica o filtro passa-baixa seguido pelo filtro passa-alta
            float lowPassOutput = _lowPassFilter.Apply(input);
            return _highPassFilter.Apply(lowPassOutput);
        }

        public static float[] ApplyFilter(float[] input, float notchFrequency, float bandwidth, float samplingRate)
        {
            Notch notchFilter = new Notch(notchFrequency, bandwidth, samplingRate);
            float[] output = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = notchFilter.Apply(input[i]);
            }
            return output;
        }
    }
}
