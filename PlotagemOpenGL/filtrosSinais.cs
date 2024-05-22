using System;
using Accord.Audio.Filters;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;
using System.Text;
using DSP;
using PlotagemOpenGL.auxi;
using Accord.Audio;

namespace PlotagemOpenGL
{
    public class filtrosSinais
    {
        const double w0 = 0.5;
        const double w1 = -0.5;
        const double s0 = 0.5;
        const double s1 = 0.5;
        public static double[] valorout = new double[GlobVar.canalA.Length];//tamanho do vetor de entrada
        public static double[] alterado;

        

        public static void FWT(double[] valorout)
        {
            alterado = new double[valorout.Length];
            double[] temp = new double[valorout.Length];


            for (int itrt = 0; itrt <= 1; itrt++)
            {
                int h = valorout.Length >> 1;
                for (int i = 0; i < h; i++)
                {
                    int k = (i << 1);
                    temp[i] = valorout[k] * s0 + valorout[k + 1] * s1;
                    temp[i + h] = valorout[k] * w0 + valorout[k + 1] * w1;
                }

            }
            alterado = temp;
        }

    }
    public class LowPassFilter
    {
        private double _alpha;
        private double _prevOutput;
        public static Accord.Audio.Filters.LowPassFilter lowFilt;

        public LowPassFilter(double alpha)
        {
            _alpha = alpha;
            _prevOutput = 0;
        }

        public double Apply(double input)
        {
            double output = _alpha * input + (1 - _alpha) * _prevOutput;
            _prevOutput = output;
            return output;
        }
        public static double[] ApplyLowPassFilter(double[] input, double alpha)
        {
            double[] output = new double[input.Length];
            lowFilt = new Accord.Audio.Filters.LowPassFilter(15, 512);
            Signal sig = Signal.FromArray(input, 512);
            lowFilt.Apply(sig);
            
            LowPassFilter lowPassFilter = new LowPassFilter(alpha);
            
            output = sig.ToDouble();

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = lowPassFilter.Apply(input[i]);
            }
            return output;
        }
    }
    public class HighPassFilter
    {
        private double alpha;
        private double prevInput = 0;
        private double prevOutput = 0;

        public HighPassFilter(double alpha)
        {
            this.alpha = alpha;
            prevOutput = 0;
        }

        public double Apply(double input)
        {
            // Calcula a saída do filtro usando a equação de diferença
            double output = input - (alpha * input + (1 - alpha) * prevOutput);
            prevOutput = output;
            return output;
        }
        public static double[] ApplyHighPassFilter(double[] input, double alpha)
        {
            HighPassFilter highPassFilter = new HighPassFilter(alpha);

            double[] output = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {

                output[i] = highPassFilter.Apply(input[i]);
            }
            return output;
        }
    }
    public class BandPassFilter
    {
        private LowPassFilter _lowPassFilter;
        private HighPassFilter _highPassFilter;

        public BandPassFilter(double lowAlpha, double highAlpha)
        {
            _lowPassFilter = new LowPassFilter(lowAlpha);
            _highPassFilter = new HighPassFilter(highAlpha);
        }

        public double Apply(double input)
        {
            double lowPassOutput = _lowPassFilter.Apply(input);
            return _highPassFilter.Apply(lowPassOutput);
        }
        public static double[] ApplyBandPassFilter(double[] input, double lowAlpha, double highAlpha)
        {
            BandPassFilter bandPassFilter = new BandPassFilter(lowAlpha, highAlpha);
            double[] output = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = bandPassFilter.Apply(input[i]);
            }
            return output;
        }

    }/*
    public class BandRejectFilter
    {
        private LowPassFilter _lowPassFilter;
        private HighPassFilter _highPassFilter;
        private double _alpha;

        public BandRejectFilter(double alpha)
        {
            _alpha = alpha;
            _lowPassFilter = new LowPassFilter(alpha);
            _highPassFilter = new HighPassFilter(alpha);
        }

        public double Apply(double input, double alpha)
        {
            double notchOutput = _lowPassFilter.Apply(input) + _highPassFilter.Apply(input);
            return input - (alpha * notchOutput);
        }
        public static double[] ApplyBandRejectFilter(double[] input,double alpha)
        {
            BandRejectFilter bandRejectFilter = new BandRejectFilter(alpha);
            double[] output = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = bandRejectFilter.Apply(input[i], alpha);
            }
            return output;
        }
    }*/
    
}
