using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class PlayerControl : MonoBehaviour {

    public float speed = 5f;
    public float rotSpeed = 120f;
    private Transform tr;
    private PhotonView pv;
    private Vector3 curPos;
    private Quaternion curRot;
    public Material[] _material;

    void Start ()
    {
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();
        if(pv.isMine)
        {
            GameObject.Find("Main Camera").GetComponent<SmoothFollow>().target = tr;
            this.GetComponent<Renderer>().material = _material[0];
        }
        else
        {
            this.GetComponent<Renderer>().material = _material[1];
        }
        pv.ObservedComponents[0] = this;
	}
	
	
	void Update ()
    {
        if(pv.isMine)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            tr.Translate(Vector3.forward * v * Time.deltaTime * speed);
            tr.Rotate(Vector3.up * h * Time.deltaTime * rotSpeed);
        }
        else
        {
            tr.position = Vector3.Lerp(tr.position, curPos, Time.deltaTime * 10f);
            tr.rotation = Quaternion.Lerp(tr.rotation, curRot, Time.deltaTime * 10f);
        }
	}

    void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
