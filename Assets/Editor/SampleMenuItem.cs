using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SampleMenuItem : MonoBehaviour
{
    //09/10/19

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

    //10/10/19

    [MenuItem("Tools/No of Selected Objects")]
    public static void SelectedObjects()
    {
        Debug.Log("Selected Objects: " + Selection.objects.Length);
    }

    [MenuItem("Tools/No of Selected Objects",true)]
    public static bool ShowObjectValidator()
    {
        if(Selection.objects.Length < 1)
        {
            return(false);
        }
        else
        {
            return(true);
        }
    }

    [MenuItem("Tools/Deselect Objects")]
    public static void DeslectObjects()
    {
        //DO NOT USE DESELECTOBJECTS IT CLOSES UNITY
        //DeslectObjects();

        Selection.activeObject = null;
    }


}
