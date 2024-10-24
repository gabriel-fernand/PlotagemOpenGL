using System;
using MathNet.Numerics;
using MathNet.Filtering;
using MathNet.Filtering.IIR;
using MathNet.Numerics;
using UnityEngine;

namespace PlotagemOpenGL.Filtros
{
    public class PaissaAlta
    {
        public static float cutoffFrequency;
        public static float sampleRate;
        public static float alpha;
        public static float previousInput;
        public static float previousOutput;

        public PaissaAlta()
        {

        }
        public PaissaAlta(float cutoffFrequency, float sampleRate)
        {
            PaissaAlta.cutoffFrequency = cutoffFrequency;
            PaissaAlta.sampleRate = sampleRate;
            PaissaAlta.alpha = CalculateAlpha(cutoffFrequency, sampleRate);
            PaissaAlta.previousInput = 0.0f;
            PaissaAlta.previousOutput = 0.0f;
        }

        public static float CalculateAlpha(float cutoffFrequency, float sampleRate)
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
            //PaissaAlta alta = new PaissaAlta(cutoffFrequency, sampleRate);

            cutoffFrequency = cutoffFrequency;
            sampleRate = sampleRate;
            alpha = CalculateAlpha(cutoffFrequency, sampleRate);
            previousInput = 0.0f;
            previousOutput = 0.0f;


            float[] outputData = new float[100 * 512];
            for (int i = 0; i < data.Length; i++)
            {
                if (i >= 100 * 512) { break; }

                float output = alpha * (previousOutput + data[i] - previousInput);
                previousInput = data[i];
                previousOutput = output;


                outputData[i] = output;

            }
            return outputData;
        }
    }
}
