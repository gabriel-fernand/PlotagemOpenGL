using System;
using MathNet.Numerics;
using MathNet.Filtering;
using MathNet.Filtering.IIR;
using MathNet.Numerics;

namespace PlotagemOpenGL.Filtros
{
    internal class PaissaAlta
    {
        public static float cutoffFrequency;
        public static float sampleRate;
        public static float alpha;
        public static float previousInput;
        public static float previousOutput;

        public PaissaAlta(float cutoffFrequency, float sampleRate)
        {
            PaissaAlta.cutoffFrequency = cutoffFrequency;
            PaissaAlta.sampleRate = sampleRate;
            PaissaAlta.alpha = CalculateAlpha(cutoffFrequency, sampleRate);
            PaissaAlta.previousInput = 0.0f;
            PaissaAlta.previousOutput = 0.0f;
        }

        private static float CalculateAlpha(float cutoffFrequency, float sampleRate)
        {
            double dt = 1.0 / sampleRate;
            double rc = 1.0 / (2 * Math.PI * cutoffFrequency);
            double ret = rc / (rc + dt);
            return (float)ret;
        }

        public float Apply(float input)
        {
            float output = alpha * (previousOutput + input - previousInput);
            previousInput = input;
            previousOutput = output;
            return output;
        }

        public static float[] ApplyFilter(float[] data, float cutoffFrequency, float sampleRate)
        {
            PaissaAlta alta = new PaissaAlta(cutoffFrequency, sampleRate);
            float[] outputData = new float[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                outputData[i] = alta.Apply(data[i]);
            }
            return outputData;
        }
    }
}
