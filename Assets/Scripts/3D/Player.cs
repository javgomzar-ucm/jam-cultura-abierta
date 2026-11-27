using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ITriggerTarget>()?.HitByPlayer(this);
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<ITriggerExitable>()?.ExitedByPlayer();
    }
}
