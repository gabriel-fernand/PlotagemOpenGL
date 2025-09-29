using System;

namespace PlotagemOpenGL.Filtros
{
    public class PaissaBaixa
    {
        public static float _alpha;
        public static float _prevOutput1;
        public static float _prevOutput2;
        public static float _prevOutput3;
        public static float _prevOutput4;
        public static float _prevOutput5;
        public static float _prevOutput6;
        public static float _prevOutput7;
        public static float _prevOutput8;
        public static float _prevOutput9;
        public static float _prevOutput10;

        public PaissaBaixa(float cutoffFrequency, float samplingRate)
        {
            _alpha = CalculateAlpha(cutoffFrequency, samplingRate);
            _prevOutput1 = _prevOutput2 = _prevOutput3 = 0;
            _prevOutput4 = _prevOutput5 = _prevOutput6 = 0;
            _prevOutput7 = _prevOutput8 = _prevOutput9 = 0;
            _prevOutput10 = 0;
        }

        public static float CalculateAlpha(float cutoffFrequency, float samplingRate)
        {
            float dt = 1.0f / samplingRate;
            float rc = 1.0f / (2.0f * (float)Math.PI * cutoffFrequency);
            return dt / (dt + rc);
        }

        public static float Apply(float input)
        {
            _prevOutput1 = _alpha * input + (1 - _alpha) * _prevOutput1;
            _prevOutput2 = _alpha * _prevOutput1 + (1 - _alpha) * _prevOutput2;
            _prevOutput3 = _alpha * _prevOutput2 + (1 - _alpha) * _prevOutput3;
            _prevOutput4 = _alpha * _prevOutput3 + (1 - _alpha) * _prevOutput4;
            _prevOutput5 = _alpha * _prevOutput4 + (1 - _alpha) * _prevOutput5;
            _prevOutput6 = _alpha * _prevOutput5 + (1 - _alpha) * _prevOutput6;

            _prevOutput7 = _alpha * _prevOutput6 + (1 - _alpha) * _prevOutput7;
            _prevOutput8 = _alpha * _prevOutput7 + (1 - _alpha) * _prevOutput8;
            _prevOutput9 = _alpha * _prevOutput8 + (1 - _alpha) * _prevOutput9;
            _prevOutput10 = _alpha * _prevOutput9 + (1 - _alpha) * _prevOutput10;

            return _prevOutput10;
        }

        public static float[] ApplyFilter(float[] input, float cutoffFrequency, float samplingRate)
        {
            Tela_Plotagem.cronometroBaixa.Start();
            _alpha = CalculateAlpha(cutoffFrequency, samplingRate);
            _prevOutput1 = _prevOutput2 = _prevOutput3 = 0;
            _prevOutput4 = _prevOutput5 = _prevOutput6 = 0;
            _prevOutput7 = _prevOutput8 = _prevOutput9 = 0;
            _prevOutput10 = 0;

            //float[] output = new float[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                input[i] = Apply(input[i]);
            }

            Tela_Plotagem.cronometroBaixa.Stop();
            return input;
        }
    }
}
