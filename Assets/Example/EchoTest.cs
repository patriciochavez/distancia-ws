using UnityEngine;
using System.Collections;
using System;

public class EchoTest : MonoBehaviour {

    Mensaje_Arduino mensaje_arduino;
    public string ws_url = "ws://192.168.1.210";
    private WebSocket w;
    public float distancia;
    
    [Serializable]
    public class Mensaje_Arduino
    {
        public float distancia;               
    }

    IEnumerator Start()
    {
        w = new WebSocket(new Uri(ws_url));
        yield return StartCoroutine(w.Connect());
        if (w.error != null)
        {
            Debug.LogError("Error: " + w.error);
        }
    }

    void Update()
    {
        string recibido = w.RecvString();
        if (JsonUtility.FromJson<Mensaje_Arduino>(recibido) != null)
        {
            mensaje_arduino = JsonUtility.FromJson<Mensaje_Arduino>(recibido);
            distancia = mensaje_arduino.distancia / 10;
            transform.position = new Vector3(0,0, -distancia);
        }
    }    
}
