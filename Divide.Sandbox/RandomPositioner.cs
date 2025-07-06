using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.Sandbox
{
    public class RandomPositioner : MonoBehaviour
    {
        void LateUpdate()
        {
            transform.position = new Vector3(
                UnityEngine.Random.Range(-1000f, 1000f),
                UnityEngine.Random.Range(-1000f, 1000f),
                UnityEngine.Random.Range(-1000f, 1000f)
            );
        }
    }
}
