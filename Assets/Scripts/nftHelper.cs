using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nftHelper : MonoBehaviour
{

    public GameObject ContentPanel;
    public GameObject TextContent;

    // Start is called before the first frame update
    void Start()
    {

        TextContent.SetActive(false);

        GameObject nftGenObj1 = (GameObject)Resources.Load("NFTGen");
        nftGenObj1.GetComponent<sellPageNFTGenerator>().receiveIndices(0,0,0,0,0);
        // nftGenObj1 = (GameObject)Instantiate(nftGenObj1, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
        // nftGenObj1 = (GameObject)Instantiate(nftGenObj1);
        // nftGenObj1.transform.localScale = Vector3.one;
        
        nftGenObj1.transform.parent = ContentPanel.transform;

        GameObject nftGenObj2 = (GameObject)Resources.Load("NFTGen");
        nftGenObj2.GetComponent<sellPageNFTGenerator>().receiveIndices(1,1,1,1,1);
        // nftGenObj2 = (GameObject)Instantiate(nftGenObj2, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        
        nftGenObj2 = (GameObject)Instantiate(nftGenObj2);
        // nftGenObj2.transform.localScale = new Vector3(2, 2, 0);

        nftGenObj2.transform.parent = ContentPanel.transform;

        GameObject nftGenObj3 = (GameObject)Resources.Load("NFTGen");
        nftGenObj3.GetComponent<sellPageNFTGenerator>().receiveIndices(2,2,2,2,2);
        // nftGenObj3 = (GameObject)Instantiate(nftGenObj3, new Vector3(0.0f, -2.0f, 0.0f), Quaternion.identity);
        
        nftGenObj3 = (GameObject)Instantiate(nftGenObj3);
        // nftGenObj3.transform.localScale = new Vector3(3, 3, 0);
        nftGenObj3.transform.parent = ContentPanel.transform;

        // GameObject nftRow1 = (GameObject)Resources.Load("sellNFTRow");
        // nftRow1.GetComponentInChildren<Transform>().find("Price").text = "$444";
        // Transform nftGenObj = nftRow1.GetComponentInChildren<Transform>().find("SellNFTGen");
        // GameObject nftGenObj = nftRow1.transform.GetChild(0).GetChild(0);
        
        // nftRow1.transform.GetChild(0).GetChild(0).GetComponent<sellPageNFTGenerator>().receiveIndices(2,2,2,2,2);
        // nftRow1 = (GameObject)Instantiate(nftRow1, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);

        // GameObject nftRow2 = (GameObject)Resources.Load("sellNFTRow");
        // nftRow2.transform.GetChild(0).GetChild(0).GetComponent<sellPageNFTGenerator>().receiveIndices(3,3,3,3,3);
        // nftRow2 = (GameObject)Instantiate(nftRow2, new Vector3(0.0f, -2.0f, 0.0f), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
