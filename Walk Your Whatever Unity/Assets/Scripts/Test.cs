using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Test : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            var testObject = new GameObject("Test");
            testObject.AddComponent<Test>();
        }
    }
}
