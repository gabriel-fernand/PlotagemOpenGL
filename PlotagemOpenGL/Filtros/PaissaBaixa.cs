using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotagemOpenGL.Filtros
{
    public class PaissaBaixa
    {
        public static int auxLow;
        public static float _alpha;
        public static float _prevOutput;

        public PaissaBaixa()
        {

        }

        public PaissaBaixa(float cutoffFrequency, float samplingRate)
        {
            _alpha = CalculateAlpha(cutoffFrequency, samplingRate);
            _prevOutput = 0;
        }

        public static float CalculateAlpha(float cutoffFrequency, float samplingRate)
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
            //PaissaBaixa lowPassFilter = new PaissaBaixa(cutoffFrequency, samplingRate);

            _alpha = CalculateAlpha(cutoffFrequency, samplingRate);
            _prevOutput = 0;


            float[] output = new float[100  * 512];
            for (int i = 0; i < input.Length; i++)
            {
                if (i >= 100 * 512) { break; }

                float outputaaa = _alpha * input[i] + (1 - _alpha) * _prevOutput;
                _prevOutput = outputaaa;

                output[i] = outputaaa;

            }
            return output;
        }

    }
}
