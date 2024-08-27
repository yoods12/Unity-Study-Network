using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    int initHp = 100;
    public int currHp = 100;

    CharacterController cc;

    // Start is called before the first frame update
    void Awake()
    {
        cc = GetComponent<CharacterController>();
        currHp = initHp;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (currHp > 0 && collision.collider.CompareTag("BULLET"))
        {
            currHp -= 20;
            if(currHp <= 0)
            {
                print("Die");
            }
        }
    }
}
