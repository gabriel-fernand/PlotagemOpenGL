using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotagemOpenGL.Filtros
{
    internal class PaissaBaixa
    {
        public static int auxLow;
        private float _alpha;
        private float _prevOutput;

        public PaissaBaixa(float cutoffFrequency, float samplingRate)
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

        public static float[] ApplyFilter(float[] input, float cutoffFrequency, float samplingRate)
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
}
