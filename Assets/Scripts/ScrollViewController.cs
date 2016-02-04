using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollViewController : MonoBehaviour {

    public GameObject prefabBt;

	// Use this for initialization
	void Start ()
    {
        for (int i=0; i<5; i++)
        {
            GameObject button = Instantiate(prefabBt);
            button.transform.SetParent(GetComponent<RectTransform>(),false);
            button.transform.localScale = new Vector3(1, 1, 1);

            Button tempBt = button.GetComponent<Button>();
            int tempInt = i;
            tempBt.onClick.AddListener(() => ButtonClicked(tempInt));

        }
	}

    void ButtonClicked(int buttonNo)
    {
        Debug.Log("Button clicked = " + buttonNo);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
