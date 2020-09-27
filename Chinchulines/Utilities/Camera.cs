using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Chinchulines.Utilities
{
    public class Camera
    {
        public Vector3 Position;
        public Vector3 TargetPosition;
        public Matrix View { get; set; }
        private readonly InputActions _actions;
        
        private float _angle = 0;
        public Camera(Vector3 position, Vector3 targetPosition)
        {
            Position = new Vector3(position.X, position.Y + 5, position.Z - 20);
            TargetPosition = targetPosition;
            _actions = new InputActions
            {
                Up = Keys.Up,
                Down = Keys.Down,
                Left = Keys.Left,
                Right = Keys.Right
            };
            View = Matrix.CreateLookAt(Position, TargetPosition, Vector3.Up);
        }
        
        public void Move()
        {
            
        }

        public void UpdateLookAt(Vector3 position, Vector3 targetPosition)
        {
            Position = new Vector3(position.X, position.Y + 5, position.Z - 20);
            
            View = Matrix.CreateLookAt(Position, targetPosition, Vector3.Up);
        }
        
    }
}