using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    /// <summary>
    /// Called when the object is retrieved from the pool.
    /// </summary>
    void OnObjectSpawn();

    /// <summary>
    /// Called when the object is returned to the pool.
    /// </summary>
    void OnObjectReturn();

    void OnObjectDestroy();
}
