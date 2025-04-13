using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitScript : MonoBehaviour
{
    [SerializeField] private float healingHP = 15f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
        }
    }
}
