using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class photonIint : MonoBehaviour {

    public Text logText;

	void Awake ()
    {
        //포톤네트웍에 버전별로 분리하여 접속
        PhotonNetwork.ConnectUsingSettings("Photon 1.0");
	}
	
	void OnJoinedLobby()
    {
        //로비에 입장하였을때 호출되는 콜백함수
        Debug.Log("Joined Lobby!!!!!");
        PhotonNetwork.JoinRandomRoom();
    }
    
	void OnPhotonRandomJoinFailed()
    {
        //랜덤룸입장에 실패하엿을때 호출되는 콜백함수
        Debug.Log("No Room");
        PhotonNetwork.CreateRoom("MyRoon");
    }
    void OnCreatedRoom()
    {
        //룸을 생성완료 하였을때 호출되는 콜백함수
        Debug.Log("Finish make a room");
    }
    
    void OnJoinedRoom()
    {
        //룸에입장했을때 호출되는 콜백함수
        Debug.Log("Joined Room");
        StartCoroutine(CreatPlayer());
    }

    IEnumerator CreatPlayer()
    {
        PhotonNetwork.Instantiate("MyPlayer", new Vector3(0, 1, 0), transform.rotation, 0);
        yield return null;
    }
    
    void Update()
    {
        logText.text = PhotonNetwork.connectionStateDetailed.ToString();//포톤네트웍에 현재나의 커넥션 상황을 텏스트로 매프레임마다 받는다.
    }
}
