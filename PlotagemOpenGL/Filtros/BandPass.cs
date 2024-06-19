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
            BandPass bandPassFilter = new BandPass(lowCutoffFrequency, highCutoffFrequency, samplingRate);
            float[] output = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = bandPassFilter.Apply(input[i]);
            }
            return output;
        }
    }
}
