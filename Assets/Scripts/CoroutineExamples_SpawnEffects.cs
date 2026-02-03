using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExamples_SpawnEffects : MonoBehaviour
{
    // Start is called before the first frame update

    // Note: instead of spawning this every time, we SHOULD use object pooling -- which we haven't covered yet. 
    [SerializeField]
    GameObject _effectPrefab;

    int _particlesToSpawn = 6;


    bool _canStartSpawning = true;


    private void Awake()
    {
        if (_effectPrefab == null) Debug.LogError("No prefab assigned to CoroutineExamples_SpawnEffects");
    }

    // Update is called once per frame
    void Update()
    {

        if (_canStartSpawning == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(SpawnParticleSystems());
            }
        }

       

        if(Input.GetKey(KeyCode.C))
        {
            StopAllCoroutines();
            _canStartSpawning = true;
        }

    }

    IEnumerator SpawnParticleSystems()
    {
        _canStartSpawning = false;

        int spawnedParticleSystems = 0;

        while(spawnedParticleSystems < _particlesToSpawn)
        {
            SpawnParticleSystem(new Vector3(spawnedParticleSystems, 0, 0));

            spawnedParticleSystems++;
            yield return new WaitForSeconds(0.5f);

        }

        _canStartSpawning = true;

                
    }

    void SpawnParticleSystem(Vector3 spawnPosition)
    {
        GameObject GO = GameObject.Instantiate(_effectPrefab);
        GO.transform.position = spawnPosition;
    
    }


}
