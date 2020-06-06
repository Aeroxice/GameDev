using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyAI : BaseAI
{
    public override IEnumerator RunAI()
    {
        yield return BoatSpeed(100.0f);
        yield return Health(800.0f);

        while (true)
        {

            yield return Ahead(200.0f);
            //yield return FireFront(1);
            //yield return TurnLookoutLeft(90);
            //yield return TurnLeft(360);
            //yield return FireLeft(1);
            //yield return TurnLookoutRight(180);
            //yield return Back(200.0f);
            //yield return FireRight(1);
            //yield return TurnLookoutLeft(90);
            //yield return TurnRight(90);
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}

