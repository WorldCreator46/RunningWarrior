using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    Transform Platform;
    void Start()
    {
        Platform = GetComponent<Transform>();
    }
    void Update()
    {
        Platform.position.Set(Platform.position.x-1f,Platform.position.y,Platform.position.z);
    }
}
