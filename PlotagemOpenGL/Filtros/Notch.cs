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
        private bool _hasLowCutoff;
        private bool _hasHighCutoff;

        public Notch(float lowCutoffFrequency, float highCutoffFrequency, float samplingRate = 512f)
        {
            _hasLowCutoff = lowCutoffFrequency > 0f;
            _hasHighCutoff = highCutoffFrequency > 0f;

            if (_hasLowCutoff)
                _lowPassFilter = new PaissaBaixa(lowCutoffFrequency, samplingRate);

            if (_hasHighCutoff)
                _highPassFilter = new PaissaAlta(highCutoffFrequency, samplingRate);
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

        public static float[] ApplyFilter(float[] input, float lowCutoffFrequency, float highCutoffFrequency, float samplingRate)
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
