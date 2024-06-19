using Accord.Audio.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotagemOpenGL.Filtros
{
    public class Notch
    {
        private double[] a;
        private double[] b;
        private double[] x;
        private double[] y;

        public Notch(double notchFrequency, double bandwidth, double samplingRate)
        {
            double omega = 2 * Math.PI * notchFrequency / samplingRate;
            double bw = 2 * Math.PI * bandwidth / samplingRate;
            double cosOmega = Math.Cos(omega);
            double alpha = Math.Sin(omega) * Math.Sinh(Math.Log(2) / 2 * bw * omega / Math.Sin(omega));

            b = new double[3];
            a = new double[3];

            b[0] = 1;
            b[1] = -2 * cosOmega;
            b[2] = 1;

            a[0] = 1 + alpha;
            a[1] = -2 * cosOmega;
            a[2] = 1 - alpha;

            // Normalize the coefficients
            for (int i = 0; i < b.Length; i++)
            {
                b[i] /= a[0];
            }
            for (int i = 0; i < a.Length; i++)
            {
                a[i] /= a[0];
            }

            x = new double[2];
            y = new double[2];
        }

        public float Apply(float input)
        {
            double output = b[0] * input + b[1] * x[0] + b[2] * x[1] - a[1] * y[0] - a[2] * y[1];
            x[1] = x[0];
            x[0] = input;
            y[1] = y[0];
            y[0] = output;

            return (float)output;
        }

        public static float[] ApplyFilter(float[] input, float notchFrequency, float bandwidth, float samplingRate)
        {
            Notch notchFilter = new Notch(notchFrequency, 2, samplingRate);
            float[] output = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = notchFilter.Apply(input[i]);
            }
            return output;
        }
    }
}
