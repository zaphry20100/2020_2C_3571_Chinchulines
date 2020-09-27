using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Chinchulines.Utilities
{
    public class Camera
    {
        public Vector3 Position;
        public Matrix View { get; set; }
        private readonly InputActions _actions;
        
        private float _angle = 0;
        public Camera(Vector3 position)
        {
            Position = position;
            _actions = new InputActions
            {
                Up = Keys.Up,
                Down = Keys.Down,
                Left = Keys.Left,
                Right = Keys.Right
            };
            View = Matrix.CreateLookAt(Position, Vector3.Zero, Vector3.Up);
        }
        
        public void Move()
        {

            var state = Keyboard.GetState();
            
            if (state.IsKeyDown(_actions.Up))
            {
                // TODO
                _angle += 0.01f;
                Position = new Vector3(0, (float) Math.Sin(_angle), (float) Math.Cos(_angle));
                
                // x = r.sin(fi)*cos(tita)
                // y = r.sin(fi)*sin(tita)
                // z = r.cos(fi)

                CreateLookAt();
            }
            if (state.IsKeyDown(_actions.Down))
            {
                // TODO
                _angle -= 0.01f;
                Position = new Vector3(0, (float) Math.Sin(_angle), (float) Math.Cos(_angle));
                
                CreateLookAt();
            }
            if (state.IsKeyDown(_actions.Left))
            {
                _angle += 0.01f;
                Position = new Vector3((float) Math.Cos(_angle), 0, (float) Math.Sin(_angle));
                    
                CreateLookAt();
            }
            if (state.IsKeyDown(_actions.Right))
            {
                _angle += 0.01f;
                Position = new Vector3((float) Math.Sin(_angle), 0, (float) Math.Cos(_angle));
                
                CreateLookAt();
            }
        }

        public void CreateLookAt()
        {
            View = Matrix.CreateLookAt(-20 * Position,Vector3.Zero, Vector3.Up);
        }
        
    }
}