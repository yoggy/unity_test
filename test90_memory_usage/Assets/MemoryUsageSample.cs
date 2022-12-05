using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class MemoryUsageSample : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 
        Debug.Log($"Profiler.GetTotalReservedMemoryLong()={Profiler.GetTotalReservedMemoryLong()}");
        Debug.Log($"Profiler.GetTotalAllocatedMemoryLong()={Profiler.GetTotalAllocatedMemoryLong()}");
        Debug.Log($"Profiler.GetTotalUnusedReservedMemoryLong()={Profiler.GetTotalUnusedReservedMemoryLong()}");

    }
}
