using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32Pluto.Utilities
{
    class CommonUtilities
    {
        internal static Vector2 RotateAboutOrigin(Vector2 origin, float rotation, float radius)
        {
            // return Vector2.Transform(point - origin, Matrix.CreateRotationZ(rotation)) + origin;
            return origin + Vector2.Transform(new Vector2(radius, 0),
                                                           Matrix.CreateRotationZ(rotation));
        }
    }
}
