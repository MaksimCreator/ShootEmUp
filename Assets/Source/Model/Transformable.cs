using UnityEngine;

namespace ShootEmUp.Model
{
    public class Transformable : Control
    {
        public Vector3 Position { get; protected set; }
        public new bool isEnable => base.isEnable;

        public Transformable(Vector3 position)
        {
            Position = position;
        }

        public void MoveX(float deltaDirectionX)
        { 
            if(isEnable)
                Position += deltaDirectionX * Vector3.right;
        }

        public void MoveY(float delatDirectionY)
        {
            if (isEnable)
                Position += delatDirectionY * Vector3.up;
        }
    }
}