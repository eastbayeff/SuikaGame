using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAfterDuration : MonoBehaviour
{
    [SerializeField] private float _duration = 2.75f;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(_duration);
        GetComponent<AudioSource>().Play();
    }
}