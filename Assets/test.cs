using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject BidPriceAmount;

    private string abcd;
    void Start()
    {
        abcd = BidPriceAmount.GetComponent<TextMeshProUGUI>().text;

        Debug.Log(abcd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
