using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SampleMenuItem : MonoBehaviour
{
    [MenuItem ("Tools/Sample Menu Item")]
    public static void SampeMenuItem()
    {
        Debug.Log("Sample Menu Item");
    }

    [MenuItem("CONTEXT/Spaceship/Spaceship Menu Item")]
    public static void SpaceshipMenuItem()
    {
        Debug.Log("Context menu Spaceship Menu Item");
    }
}
