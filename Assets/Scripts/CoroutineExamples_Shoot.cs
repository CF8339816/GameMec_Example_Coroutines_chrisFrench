using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoroutineExamples_Shoot : MonoBehaviour
{

    bool _canShoot = true;
    float _timeBetweenShots = 2f;    
  

    // Update is called once per frame
    void Update()
    {
        if (_canShoot == false) return;


        if(Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Shoot());
        }

    }

    
    IEnumerator Shoot()
    {
        Debug.Log("You shot a bullet. Starting the cooldown!");

        _canShoot = false;

        yield return new WaitForSeconds(_timeBetweenShots);

        _canShoot = true;

        Debug.Log("Cooldown complete, you may shoot again");

    }

}
