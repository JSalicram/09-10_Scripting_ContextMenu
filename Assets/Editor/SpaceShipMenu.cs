using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpaceShipMenu : MonoBehaviour
{
    [MenuItem("CONTEXT/Spaceship/Quick Rotate")]
    public static void QuickRotate(MenuCommand command)
    {
        Spaceship ship = (Spaceship)command.context;
        ship.maxTurnRate = 1000f;
    }
}
