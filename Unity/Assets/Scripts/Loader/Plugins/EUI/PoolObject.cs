﻿using UnityEngine;

namespace ET.Client
{
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    public class PoolObject : MonoBehaviour
    {
        public string poolName;
        //defines whether the object is waiting in pool or is in use
        public bool isPooled;
    }
}