using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PlotagemOpenGL.auxi
{
    internal class GLU
    {
        [DllImport("opengl32.dll", EntryPoint = "gluUnProject")]
        public static extern int gluUnProject(
       double winX, double winY, double winZ,
       double[] model, double[] proj, int[] view,
       out double objX, out double objY, out double objZ);
    }
}
