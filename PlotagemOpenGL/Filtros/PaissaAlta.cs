using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotagemOpenGL.Filtros
{
    internal class PaissaAlta
    {
        public static int auxHigh;
        private float _alpha;
        private float _prevInput;
        private float _prevOutput;

        public PaissaAlta(float cutoffFrequency, float samplingRate)
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

        public static float[] ApplyFilter(float[] input, float cutoffFrequency, float samplingRate)
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
}
