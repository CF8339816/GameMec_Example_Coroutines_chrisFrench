using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDuration : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    float _lifeTime;

    IEnumerator WaitAndDestroySelf()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }

}
