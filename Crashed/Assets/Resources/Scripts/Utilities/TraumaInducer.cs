using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraumaInducer : MonoBehaviour
{
    [Tooltip("Seconds to wait before trigerring the explosion particles and the trauma effect")]
    public float Delay = 1;
    [Tooltip("Maximum stress the effect can inflict upon objects Range([0,1])")]
    public float MaximumStress = 0.6f;
    [Tooltip("Maximum distance in which objects are affected by this TraumaInducer")]
    public float Range = 45;

    public void StartInduceStress()
    {
        StressReceiver receiver = Camera.main.gameObject.GetComponent<StressReceiver>();
        if (receiver == null)
        {
            return;
        }
        float distance = Vector3.Distance(transform.position, Camera.main.gameObject.transform.position);
        /* Apply stress to the object, adjusted for the distance */

        //if (distance > Range) return;
        float distance01 = Mathf.Clamp01(distance / Range);
        float stress = (1 - Mathf.Pow(distance01, 2)) * MaximumStress;
        receiver.InduceStress(stress);
    }

  
}
