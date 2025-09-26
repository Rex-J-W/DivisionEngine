﻿using ComputeSharp;
using DivisionEngine.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Diagnostics.CodeAnalysis;
using Window = Silk.NET.Windowing.Window;

namespace DivisionEngine.Rendering
{

    /// <summary>
    /// SDF Render pipeline for Division Engine.
    /// </summary>
    public class RenderPipeline
    {
        // Special variables
        public readonly object SyncLock = new object(); // Synchronization lock for thread safety

        // OpenGL variables
        private GL? gl;
        private GraphicsDevice? device; // Graphics device for ComputeSharp operations
        private uint glTexture, shaderProgram;

        // Rendering variables
        public IWindow? RendererWindow;
        public bool InputReady { get; private set; } = false; // Indicates if the renderer is ready to process input
        public event Action? Close; // Event to handle window close actions

        // Other variables
        public float Time;

        /// <summary>
        /// Initializes and runs the render window.
        /// </summary>
        /// <remarks>This method creates a render window with default options, sets up event handlers for
        /// loading and rendering.</remarks>
        public void Run(bool editorMode)
        {
            WindowOptions options = WindowOptions.Default;
            if (editorMode)
            {
                options.TopMost = true;
                options.WindowBorder = WindowBorder.Hidden;
            }

            options.Title = "SDF Scene";
            options.IsVisible = true;
            options.VSync = true;
            options.ShouldSwapAutomatically = true;
            
            options.UpdatesPerSecond = 60; // Sets the update rate to 60 FPS
            RendererWindow = Window.Create(options);

            Debug.Info("Renderer: Created Render Window");
            RendererWindow.Load += OnLoad;
            RendererWindow.Render += OnRender;
            RendererWindow.Closing += OnClosing;
            Debug.Info("Renderer: Running Renderer");
            RendererWindow.Run();
            Debug.Info("Renderer: Terminated");
        }

        /// <summary>
        /// Called when the renderer window is closing.
        /// </summary>
        private void OnClosing()
        {
            Close?.Invoke(); // Invoke the close event if there are any subscribers
        }

        [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private void OnLoad()
        {
            gl = GL.GetApi(RendererWindow);

            // Initialize OpenGL context
            Debug.Info("Renderer: Initialize OpenGL Context");
            gl.GenTextures(1, out glTexture);
            gl.BindTexture(TextureTarget.Texture2D, glTexture);
            gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.Linear);
            gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);

            // Load graphics device
            device = GraphicsDevice.GetDefault();

            Debug.Info("Renderer: Compiling OpenGL Shader Program");
            shaderProgram = CompileShaders();
            gl!.GenVertexArrays(1, out uint vao);
            gl.BindVertexArray(vao); // Bind the Vertex Array Object (VAO)
            Debug.Info("Renderer: VAO Bound");

            InputReady = true; // Set input ready to true after OpenGL context is initialized
        }

        [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        private void OnRender(double delta)
        {
            // Variable setup (variables modified outside of renderer must be locked)
            int texWidth = RendererWindow!.Size.X, texHeight = RendererWindow.Size.Y;

            float time;
            lock (SyncLock)
            {
                time = Time;
            }

            if (InputSystem.IsPressed(KeyCode.J))
            {
                time = 0.5f;
            }
            else if (InputSystem.IsPressed(KeyCode.K))
            {
                time = 0.2f;
            }

            if (texWidth < 1 || texHeight < 1) return; // Ensure valid texture dimensions
            ReadWriteTexture2D<float4> renderTex = device!.AllocateReadWriteTexture2D<float4>(texWidth, texHeight);

            // Dispatch SDF compute shader
            float4x4 camToWorld = new float4x4(); // Replace with actual camera to world matrix
            float4x4 camInverseProj = new float4x4(); // Replace with actual camera inverse projection matrix

            SDFShader shader = new SDFShader(renderTex, time, texWidth, texHeight, camToWorld, camInverseProj);
            device!.For(texWidth, texHeight, shader);

            float4[] pixels = new float4[texWidth * texHeight];
            renderTex.CopyTo(pixels);
            renderTex.Dispose(); // Dispose of the texture after use

            unsafe
            {
                fixed (float4* dataPtr = pixels)
                {
                    gl!.BindTexture(TextureTarget.Texture2D, glTexture);
                    gl.TexImage2D(
                        TextureTarget.Texture2D,
                        0,
                        (int)InternalFormat.Rgba32f,
                        (uint)texWidth,
                        (uint)texHeight,
                        0,
                        PixelFormat.Rgba,
                        PixelType.Float,
                        dataPtr);
                }
            }

            gl.Viewport(0, 0, (uint)texWidth, (uint)texHeight);
            gl.ClearColor(0f, 0f, 0f, 1f);
            gl.Clear((uint)ClearBufferMask.ColorBufferBit);
            gl.UseProgram(shaderProgram);

            gl.ActiveTexture(TextureUnit.Texture0);
            gl.BindTexture(TextureTarget.Texture2D, glTexture);
            int loc = gl.GetUniformLocation(shaderProgram, "tex");
            gl.Uniform1(loc, 0);

            gl.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            gl.Finish();
        }

        /// <summary>
        /// Compiles and links vertex and fragment shaders into a shader program.
        /// </summary>
        /// <returns>The handle of the compiled and linked shader program.</returns>
        private uint CompileShaders()
        {
            string vertexSrc = """
            #version 330 core
            out vec2 uv;

            void main() {
                const vec2 positions[4] = vec2[](
                    vec2(-1, -1),
                    vec2( 1, -1),
                    vec2(-1,  1),
                    vec2( 1,  1)
                );
                const vec2 uvs[4] = vec2[](
                    vec2(0, 0),
                    vec2(1, 0),
                    vec2(0, 1),
                    vec2(1, 1)
                );
                gl_Position = vec4(positions[gl_VertexID], 0, 1);
                uv = uvs[gl_VertexID];
            }
            """;

            string fragSrc = """
            #version 330 core
            in vec2 uv;
            out vec4 fragColor;
            uniform sampler2D tex;

            void main() {
                fragColor = texture(tex, uv);
            }
            """;

            uint vs = gl!.CreateShader(ShaderType.VertexShader);
            gl.ShaderSource(vs, vertexSrc);
            gl.CompileShader(vs);
            CheckShaderCompileStatus(vs, "Vertex Shader");

            uint fs = gl.CreateShader(ShaderType.FragmentShader);
            gl.ShaderSource(fs, fragSrc);
            gl.CompileShader(fs);
            CheckShaderCompileStatus(fs, "Fragment Shader");

            uint shader = gl.CreateProgram();
            gl.AttachShader(shader, vs);
            gl.AttachShader(shader, fs);
            gl.LinkProgram(shader);
            CheckProgramLinkStatus(shader);

            gl.DeleteShader(vs);
            gl.DeleteShader(fs);

            return shader;
        }

        /// <summary>
        /// Checks the compile status of a shader and debugs the result.
        /// </summary>
        /// <remarks>If the shader compilation fails, the method retrieves the error log and writes it to
        /// the debug output. If the compilation succeeds, a success message is written to the debug output.</remarks>
        /// <param name="shader">The identifier of the shader to check.</param>
        /// <param name="name">The name of the shader, used for logging purposes.</param>
        private void CheckShaderCompileStatus(uint shader, string name)
        {
            gl!.GetShader(shader, ShaderParameterName.CompileStatus, out int success);
            if (success == 0)
            {
                string info = gl.GetShaderInfoLog(shader);
                Debug.Error($"{name} Compile Error: {info}");
            }
            else
                Debug.Info($"{name} Compiled Successfully");
        }

        /// <summary>
        /// Checks the link status of the OpenGL shader program and debugs the result.
        /// </summary>
        /// <remarks>If the shader program fails to link, the method retrieves and debugs the program's
        /// error information. If the shader program links successfully, a success message is displayed.</remarks>
        /// <param name="program">The identifier of the shader program to check.</param>
        private void CheckProgramLinkStatus(uint program)
        {
            gl!.GetProgram(program, ProgramPropertyARB.LinkStatus, out int success);
            if (success == 0)
            {
                string info = gl.GetProgramInfoLog(program);
                Debug.Error($"Shader Program Link Error: {info}");
            }
            else
                Debug.Info("Shader Program Linked Successfully");
        }
    }

}
