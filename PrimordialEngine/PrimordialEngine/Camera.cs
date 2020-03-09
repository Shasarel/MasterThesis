using SharpDX;
using System;

namespace PrimordialEngine
{
    class Camera
    {
        public float Pitch
        {
            get => _pitch;
            set
            {
                _pitch = value % 180;
                UpdateViewProjectionMatrix();
            }
        }
        public float Yaw
        {
            get => _yaw;
            set
            {
                _yaw = value % 180;
                UpdateViewProjectionMatrix();
            }
        }
        public float FieldOfView
        {
            get => _fieldOfView;
            set
            {
                _fieldOfView = value % 360;
                UpdateProjectionMatrix();
            }
        }
        public float Aspect
        {
            get => _aspect;
            set
            {
                _aspect = value;
                UpdateProjectionMatrix();
            }
        }
        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                UpdateViewProjectionMatrix();
            }
        }
        public float PositionX
        {
            get => _position.X;
            set
            {
                _position.X = value;
                UpdateViewProjectionMatrix();
            }
        }
        public float PositionY
        {
            get => _position.Y;
            set
            {
                _position.Y = value;
                UpdateViewProjectionMatrix();
            }
        }
        public float PositionZ
        {
            get => _position.Z;
            set
            {
                _position.Z = value;
                UpdateViewProjectionMatrix();
            }
        }
        public Matrix ViewProjectionMatrix => _viewProjectionMatrix;
        public float Speed { get; set; }

        private Matrix _projectionMatrix, _viewProjectionMatrix;
        private float _pitch, _yaw, _fieldOfView, _aspect;
        private Vector3 _position;

        public Camera(float fieldOfView, float aspect)
        {
            _fieldOfView = fieldOfView;
            _aspect = aspect;
            _pitch = _yaw = 0;
            _position = Vector3.Zero;
            Speed = 100.0f;
            UpdateProjectionMatrix();
        }
        private void UpdateProjectionMatrix()
        {
            float rads = (FieldOfView / 360.0f) * (float)Math.PI * 2.0f;
            _projectionMatrix = Matrix.PerspectiveFovLH(rads, Aspect, 0.1f, 1000.0f);
            UpdateViewProjectionMatrix();
        }

        private void UpdateViewProjectionMatrix()
        {
            var lookX = Math.Cos(_yaw) * Math.Cos(_pitch);
            var lookY = Math.Cos(_yaw) * Math.Sin(_pitch);
            var lookZ = Math.Sin(_yaw);
            var translationMatrix = Matrix.Translation(Position);
            var rotationMatrix = Matrix.RotationY(_yaw) * Matrix.RotationX(_pitch);
            var viewMatrix = translationMatrix * rotationMatrix;
            _viewProjectionMatrix = Matrix.Multiply(viewMatrix, _projectionMatrix);
        }

        public void goForward(float dt)
        {
            _position.Z -= Speed * (float)Math.Cos(_yaw) * dt;
		    _position.X += Speed * (float)Math.Sin(_yaw) * dt;
		    _position.Y -= Speed * (float)Math.Sin(_pitch) * dt;
        }
        public void goBack(float dt)
        {
            _position.Y -= Speed * (float)Math.Cos(_yaw) * dt;
            _position.X -= Speed * (float)Math.Sin(_yaw) * dt;
            _position.Z += Speed * (float)Math.Cos(_pitch) * dt;
        }
        public void goLeft(float dt)
        {
            _position.Y += Speed * (float)Math.Cos((_yaw - 90) / 180.0f * Math.PI) * dt;
            _position.X += Speed * (float)Math.Sin((_yaw - 90) / 180.0f * Math.PI) * dt;
        }
        public void goRight(float dt)
        {
            _position.Y -= Speed * (float)Math.Sin(_yaw / 180.0f * Math.PI) * dt;
            _position.X += Speed * (float)Math.Cos(_yaw / 180.0f * Math.PI) * dt;
        }
        public void goDown(float dt)
        {
            _position.Y += Speed * (float)Math.Cos(_yaw / 180.0f * Math.PI) * dt;
            _position.X += Speed * (float)Math.Sin(_yaw / 180.0f * Math.PI) * dt;
            _position.Z -= Speed * (float)Math.Cos(_pitch / 180.0f * Math.PI) * dt;
        }
        public void goUp(float dt)
        {
            _position.Y += Speed * (float)Math.Cos(_yaw / 180.0f * Math.PI) * dt;
            _position.X += Speed * (float)Math.Sin(_yaw / 180.0f * Math.PI) * dt;
            _position.Z -= Speed * (float)Math.Cos(_pitch / 180.0f * Math.PI) * dt;
        }
    }
}
