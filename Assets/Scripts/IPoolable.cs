using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    string OriginTag { get; }
    void SetOriginTag(string tag);
    void OnSpawn();   
    void OnDespawn(); 
}
