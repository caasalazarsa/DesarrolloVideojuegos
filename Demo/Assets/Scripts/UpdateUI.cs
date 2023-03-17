using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
	[SerializeField] private GameObject objectPrefab;
	private TextMeshProUGUI UIText;
	private string ObjectID;

	void Awake(){
		UIText = GetComponent<TextMeshProUGUI>();
		ObjectID = objectPrefab.GetComponent<Object>().ID;

	}


    // Start is called before the first frame update
    void Start()
    {
    	PlayerPrefs.SetInt(ObjectID,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate(){
    	UIText.text = PlayerPrefs.GetInt(ObjectID).ToString();
    }
}
