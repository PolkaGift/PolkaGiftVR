using LostThoughtStudios.DemterGift.DataManager;
using LostThoughtStudios.DemterGift.PhysicsTriggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class BidNft : MonoBehaviour
{
    [SerializeField]
    private GameObject BidPriceAmount;

    [SerializeField]
    private GameObject NFTBidPrice;

    [SerializeField]
    private PrivateToken MyToken;

    [SerializeField]
    private DirectionTrigger directionTriggerObject;

    public static string BidAMount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (tag)
            {
                case "0": case "1" : case "2" : case "3" : case "4" : case "5" : case "6" : case "7" : case "8" : case "9":
                    BidPriceAmount.GetComponent<TextMeshProUGUI>().text += tag;
                    break;
                case "Dot":
                    BidPriceAmount.GetComponent<TextMeshProUGUI>().text += ".";
                    break;
                case "Erase":
                    BidPriceAmount.GetComponent<TextMeshProUGUI>().text = BidPriceAmount.GetComponent<TextMeshProUGUI>().text.Remove(BidPriceAmount.GetComponent<TextMeshProUGUI>().text.Length - 1);
                    break;
                case "Execute":
                    BidAMount = BidPriceAmount.GetComponent<TextMeshProUGUI>().text;
                    ExecuteBidAction();
                    break;
                case "Refresh":
                    RefreshData();
                    break;
            }
        }
    }

    private async void ExecuteBidAction()
    {
        string PostURL = "https://demetercoin-tron.onrender.com/api/BidNFT/" + MyToken.currentNFTID.ToString();

        WWWForm form = new WWWForm();

        form.AddField("privatekey", "a9cba477f6702e7b175af9914501919d3a3e5a751f08ac3f9b752e56e78d0988");
        form.AddField("BidPrice", BidPriceAmount.GetComponent<TextMeshProUGUI>().text);

        BidPriceAmount.GetComponent<TextMeshProUGUI>().text = "Processing...";

        using (UnityWebRequest www = UnityWebRequest.Post(PostURL, form))
        {

            var operation = www.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();


            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!" + www.result);

                BidPriceAmount.GetComponent<TextMeshProUGUI>().text = "Success";

                await Task.Delay(2000);

                BidPriceAmount.GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
    private void RefreshData()
    {
        //Can be improved in the future by requesting the data again from server
        DataSyncer.Instance.EventData[directionTriggerObject.index - 1].NftDonatedPerEvent[MyToken.currentNFTID].BidPrice = BidAMount;

        NFTBidPrice.GetComponent<TextMeshProUGUI>().text = BidAMount;
    }
}
