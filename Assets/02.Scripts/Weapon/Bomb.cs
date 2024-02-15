using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject BoomPrefab;

    
    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.collider.tag != "Player")
        {
            Instantiate(BoomPrefab, transform.position, transform.rotation);

            /* GameObject effect = Instantiate(BoomPrefab);
             effect.transform.position = this.gameObject.transform.position;*/

            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
