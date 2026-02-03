using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class CoroutineExamples_Sequence : MonoBehaviour
{
    AudioSource _audioSource;

    [SerializeField]
    Transform _doorTransform;

    [SerializeField]
    float _doorOpenSpeed, _delayAfterKnockSoundStarts, _delayBetweenDoorOpenAndClose;



    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>(); 

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            
            StartCoroutine(PlayDoorKnockSequence());
        }
    }

    IEnumerator PlayDoorKnockSequence()
    {
        _audioSource.Stop(); // just in case it is already playing.
        _audioSource.Play();

        _doorTransform.transform.rotation = Quaternion.Euler(0, 0, 0);

        yield return new WaitForSeconds(_delayAfterKnockSoundStarts);

        float doorAngle = 0;

        // open the door
        while(doorAngle < 90)
        {
            _doorTransform.rotation = Quaternion.Euler(0, doorAngle, 0);
            doorAngle += Time.deltaTime * _doorOpenSpeed;
            yield return null;

        }

        yield return new WaitForSeconds(_delayBetweenDoorOpenAndClose);


        //close the door
        while (doorAngle > 0)
        {
            _doorTransform.rotation = Quaternion.Euler(0, doorAngle, 0);
            doorAngle -= Time.deltaTime * _doorOpenSpeed;
            yield return null;

        }







    }



}
