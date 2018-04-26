using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speed = 10f;
    public float fireRange = 300f;
    public float damage = 10f;

    private Transform tr;
    private Vector3 spawnPoint;

	void Start ()
    {
        tr = GetComponent<Transform>();//선언한 트랜스폼의 변수 tr에 이 스크립트가 달려있는 오브젝트의 트랜스폼을 집어넣는다.
        spawnPoint = tr.position;//벡터3값으로 선언한 spawPoint에 따로 선언한 tr의 포지션값을 넣는다.
	}
	
	
	void Update ()
    {
        tr.Translate(Vector3.forward * Time.deltaTime * speed);//벡터3값의 forward(0,1,0)으로 변수로선언한 speed값만큼 계속 이동한다.
        if((spawnPoint-tr.position).sqrMagnitude>fireRange)
        {
            Destroy(gameObject);
        }
	}
}
