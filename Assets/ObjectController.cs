using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public void SetActive(GameObject g)
    {
        g.SetActive(true);
    }

    public void SetInactive(GameObject g)
    {
        g.SetActive(false);
    }
}
