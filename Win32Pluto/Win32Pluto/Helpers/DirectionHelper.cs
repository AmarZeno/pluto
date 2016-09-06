using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32Pluto.Helpers
{
    class DirectionHelper
    {
        // Rotates one object to face another object (or position)
        public static double FaceObject(Vector2 position, Vector2 target)
        {
            return (Math.Atan2(position.Y - target.Y, position.X - target.X) * (180 / Math.PI));
        }

        // Creates a Vector2 to use when moving object from position to a target, with a given speed
        public static Vector2 MoveTowards(Vector2 position, Vector2 target, float speed)
        {
            double direction = (float)(Math.Atan2(target.Y - position.Y, target.X - position.X) * 180 / Math.PI);

            Vector2 move = new Vector2(0, 0);

            move.X = (float)Math.Cos(direction * Math.PI / 180) * speed;
            move.Y = (float)Math.Sin(direction * Math.PI / 180) * speed;

            return move;
        }
    }
}
