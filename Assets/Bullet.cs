using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1000f);
        Destroy(this.gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var contact = collision.GetContact(0);
        var obj = Instantiate(effect, contact.point, Quaternion.LookRotation(-contact.normal)); 
        //normal 법선벡터 총알이 벽에 부딪히면 이펙트가 반대로 나오게
        Destroy(obj, 2f);
        Destroy(this.gameObject);
    }

}
