using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomScripts.Fundamentals
{
    public static class Vector3Extensions
    {
        public static Vector3 Set(this Vector3 @this, float? x = null, float? y = null, float? z = null)
            => new Vector3(x ?? @this.x, y ?? @this.y, z ?? @this.z);
    }
}
