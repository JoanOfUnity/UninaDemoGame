using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UninaGame
{
    internal class Camera
    {
        private const float DefaultSpeed = 15f;
        private const float DefaultSensitivity = 100f;
        private float speed = DefaultSpeed;
        private int screenWidth;
        private int screenHeight;
        private float sensitivity = DefaultSensitivity;

        public Vector3 Position;

        private Vector3 up = Vector3.UnitY;
        private Vector3 front = -Vector3.UnitZ;
        private Vector3 right = Vector3.UnitX;

        private float pitch;
        private float yaw = -270.0f;
        private bool firstMove = true;
        private Vector2 lastPos;

        public Camera(int width, int height, Vector3 position)
        {
            screenWidth = width;
            screenHeight = height;
            Position = position;
        }
        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + front, up);
        }
        public Matrix4 GetProjection()
        {
            return
           Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60f), screenWidth / screenHeight, 0.1f, 2000f);
        }
        public void InputController(KeyboardState input, MouseState mouse, FrameEventArgs e)
        {
            
            if (input.IsKeyDown(Keys.W))
            {
                Position += front * speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.A))
            {
                Position -= right * speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.S))
            {
                Position -= front * speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.D))
            {
                Position += right * speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.Space))
            {
                Position += up * speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.LeftControl))
            {
                Position += -up * speed * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                speed = 80f;
            }
            else if (input.IsKeyReleased(Keys.LeftShift))
            {
                speed = DefaultSpeed;
            }
            if(Position.Y < 0.00001f)
            {
                Position.Y = 0.00001f;
            }
            if (input.IsKeyDown(Keys.LeftAlt))
            {
                firstMove = true;
            }
            else
            {
                if (firstMove)
                {
                    lastPos = new Vector2(Position.X, Position.Y);
                    lastPos = new Vector2(mouse.X, mouse.Y);
                    firstMove = false;
                }
                else
                {
                    var deltaX = mouse.X - lastPos.X;
                    var deltaY = mouse.Y - lastPos.Y;
                    lastPos = new Vector2(mouse.X, mouse.Y);
                    yaw += deltaX * sensitivity * (float)e.Time;
                    pitch -= deltaY * sensitivity * (float)e.Time;
                } 
            }
            UpdateVectors();
        }
        public void Update(KeyboardState input, MouseState mouse,
        FrameEventArgs e)
        {
            InputController(input, mouse, e);
        }
        private void UpdateVectors()
        {
            if (pitch > 89.0f)
            {
                pitch = 89.0f;
            }
            if (pitch < -89.0f)
            {
                pitch = -89.0f;
            }
            front.X = MathF.Cos(MathHelper.DegreesToRadians(pitch)) *
            MathF.Cos(MathHelper.DegreesToRadians(yaw));
            front.Y = MathF.Sin(MathHelper.DegreesToRadians(pitch));
            front.Z = MathF.Cos(MathHelper.DegreesToRadians(pitch)) *
            MathF.Sin(MathHelper.DegreesToRadians(yaw));
            front = Vector3.Normalize(front);
            right = Vector3.Normalize(Vector3.Cross(front,
            Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));
        }

    }
}
