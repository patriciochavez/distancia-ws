﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RESTFactory : MonoBehaviour
{
    private string results;

    public String Results
    {
        get
        {
            return results;
        }
    }

    public WWW GET(string url, System.Action onComplete)
    {

        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, onComplete));        
        return www;
    }

    internal string GET(string v1, object v2)
    {
        throw new NotImplementedException();
    }

    public WWW POST(string url, Dictionary<string, string> post, System.Action onComplete)
    {
        WWWForm form = new WWWForm();

        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }

        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www, onComplete));
        return www;
    }

    private IEnumerator WaitForRequest(WWW www, System.Action onComplete)
    {        
        yield return www;        
        
        // check for errors
        if (www.error == null)
        {
            results = www.text;
            onComplete();
        }
        else
        {
            Debug.Log(www.error);
        }
    }


}
