using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Audio.OpenAL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace UninaGame
{
    internal class Shader
    {
        public int ShaderHandle { get; private set; }

        public Shader() { }

        public void LoadShader()
        {
            ShaderHandle = GL.CreateProgram();
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, LoadShaderSource("shader.vert"));
            GL.CompileShader(vertexShader);

            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int success1);
            if (success1 == 0)
            {
                string infoLog = GL.GetShaderInfoLog(vertexShader);
                Console.WriteLine(infoLog);
            }

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSource("shader.frag"));
            GL.CompileShader(fragmentShader);

            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int success2);
            if (success2 == 0)
            {
                string infoLog = GL.GetShaderInfoLog(fragmentShader);
                Console.WriteLine(infoLog);
            }

            GL.AttachShader(ShaderHandle, vertexShader);
            GL.AttachShader(ShaderHandle, fragmentShader);
            GL.LinkProgram(ShaderHandle);
        }

        public static string LoadShaderSource(string filepath)
        {
            try
            {
                using var reader = new StreamReader($"../../../Shaders/{filepath}");
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load shader source file: {ex.Message}");
                return string.Empty;
            }
        }

        public void UseShader() => GL.UseProgram(ShaderHandle);
        public void DeleteShader() => GL.DeleteProgram(ShaderHandle);
    }
}
