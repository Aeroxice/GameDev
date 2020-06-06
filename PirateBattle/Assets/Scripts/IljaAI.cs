using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IljaAI : BaseAI
{
    public override IEnumerator RunAI() {

        yield return BoatSpeed(75.0f);
        

        for (int i = 0; i < 10; i++)
        {
            yield return Ahead(200);
            yield return FireFront(1);
            yield return TurnRight(90);
            yield return TurnLeft(90);
            yield return FireFront(1);
            /*yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);

            yield return Back(200);
            yield return FireRight(1);
            yield return TurnLookoutLeft(90);
            yield return TurnRight(90);*/
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}
