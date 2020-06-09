using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyAI0 : BaseAI
{
    public override IEnumerator RunAI()
    {
        yield return BoatSpeed(140.0f);
        yield return Health(400.0f);

        yield return Ahead(400);
        yield return TurnRight(90);
        yield return Ahead(800);
        yield return TurnLookoutRight(360);

        while (true)
        {
            yield return TurnLeft(180);
            yield return Ahead(400);
            //yield return FireFront(1);
            yield return TurnRight(90);
            yield return Ahead(400);
            //yield return Back(200.0f);
            //yield return FireRight(1);
            //yield return TurnLookoutLeft(90);
            //yield return TurnRight(90);
        }       
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
            //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}
