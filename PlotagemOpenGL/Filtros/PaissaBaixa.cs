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
            //versao 24/02/2025
        }

        public PaissaBaixa(float cutoffFrequency, float samplingRate)
        {
            _alpha = CalculateAlpha(cutoffFrequency, samplingRate);
            _prevOutput = 0;
        }


        public static float CalculateAlpha(float cutoffFrequency, float samplingRate)
        {
            float dt = 1.0f / samplingRate;
            float rc = 1.0f / (2.0f * (float)Math.PI * cutoffFrequency);
            return dt / (dt + rc);
        }

        public static float Apply(float input)
        {
            _prevOutput = _alpha * input + (1 - _alpha) * _prevOutput;
            return _prevOutput;
        }

        public static float[] ApplyFilter(float[] input, float cutoffFrequency, float samplingRate)
        {
            Tela_Plotagem.cronometroBaixa.Start();            

            _alpha = CalculateAlpha(cutoffFrequency, samplingRate);
            _prevOutput = 0;

            float[] outputa = new float[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                outputa[i] = Apply(input[i]);
            }
            float[] outputs = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                outputs[i] = Apply(outputa[i]);
            }
            float[] outpute = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                outpute[i] = Apply(outputs[i]);
            }

            float[] output = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = Apply(outpute[i]);
            }

            Tela_Plotagem.cronometroBaixa.Stop();

            return output;
        }

    }
}
