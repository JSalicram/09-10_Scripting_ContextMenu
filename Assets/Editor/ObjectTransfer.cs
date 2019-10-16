using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectTransfer : ScriptableWizard
{
    public GameObject replacementObject;

    public GameObject[] selectedObjects;

    [MenuItem("Tools/Object transfer")]
    public static void objectTransfer()
    {
        ScriptableWizard.DisplayWizard<ObjectTransfer>("Object Transfer", "Change Selected Object", "Apply");
    }

    private void OnWizardCreate()
    {
        //GameObject go = new GameObject()
        DoReplaceAll();
    }

    void DoReplaceAll()
    {
        //Debug.Log("Replace All");

        selectedObjects = Selection.gameObjects;

        //Debug.Log(selectedObjects);

        
        foreach (GameObject go in selectedObjects)
        {
            //GameObject obj = go;

            //Transform obj = go.transform;

            Vector3 objTrans = go.transform.position;
            Quaternion objRotate = go.transform.rotation;
            Vector3 objScale = go.transform.localScale;

            replacementObject.transform.localScale = objScale;

            Instantiate(replacementObject, objTrans, objRotate);

            DestroyImmediate(go);
        }
    }
}
