using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputField inputField=transform.GetComponent<InputField>();
        inputField.onValueChanged.AddListener((value)=>Debug.Log(value));
        inputField.onEndEdit.AddListener((value)=>Debug.Log(value+1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
