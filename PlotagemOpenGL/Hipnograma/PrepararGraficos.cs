using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using PlotagemOpenGL.auxi;
using SharpGL;


namespace PlotagemOpenGL.Hipnograma
{
    internal class PrepararGraficos
    {
        public OpenGL gl;

        public PrepararGraficos(OpenGL gl)
        {
            this.gl = gl;
        }

        public void Desenha()
        {
            int tamanho = GlobVar.matrizCanal.GetLength(1) / GlobVar.namos;
            float red = GlobVar.FundoColor[0] / 255.0f;
            float green = GlobVar.FundoColor[1] / 255.0f;
            float blue = GlobVar.FundoColor[2] / 255.0f;
            float alpha = GlobVar.FundoColor[3] / 255.0f;

            // Defina a cor de fundo com os valores RGB normalizados
            gl.ClearColor(red, green, blue, alpha); // A última variável é o alpha (opacidade), 1.0f para opaco

            // Continue com as configurações normais do OpenGL
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Viewport(0, 0, (int)HipnogramaForm.openglHipno.Width, (int)HipnogramaForm.openglHipno.Height);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            gl.Ortho(0, tamanho, 0, HipnogramaForm.openglHipno.Height, -2, 2);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, 1);
            gl.PointSize(3.0f);
            gl.Color(0.0f, 0.0f, 0.0f);
            gl.Scale(1, 1, 1);

            gl.Color(0.1f, 0.1f, 0.1f);

        }
    }
}
