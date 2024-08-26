using System.Collections.Generic;
using UnityEngine;

public class UpgradeFruitWheel : MonoBehaviour
{
    public List<GameObject> FruitParticles;

    private void Start() => Unlock(0); 

    public void Unlock(int index)
    {
        for (int i = 0; i <= index; i++)
            FruitParticles[i].SetActive(true);
    }
}