//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectClicker : MonoBehaviour
{    
    [SerializeField] public TextMeshPro priceBilboard;
    public Animator anim;
    public int countObject = 0;
    public Transform referencePosition;
    GameObject objectClone;
    Vector3 offset = new Vector3(0, 0, 0);
    Vector3 rePosition = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                if (hit.transform && countObject < 5)
                {                    
                    if (hit.transform.gameObject.tag == "Selectable")
                    {
                        countObject += 1;                        
                    }
                    CloneObject(hit.transform.gameObject);
                }

                if (hit.collider.gameObject.tag == "Destroyable")
                {
                    Destroy (hit.collider.gameObject);
                    countObject -= 1;
                    offset += new Vector3(0.55f, 0, 0);
                    CloneObjectRearrange();                    
                }

                if (hit.transform)
                {
                    WaveModel(hit.transform.gameObject);
                    ShowPrice(hit.transform.gameObject);
                }
            }
        }
    }    

    void WaveModel(GameObject go)
    {
        if (go.tag == "ModelWavingTag")
        {
            anim.Play("Waving");
            print("hello there...!");
        }
        else
        {
            print("no action ...!");
        }
    }

    private void ShowPrice(GameObject go)
    {
        
        if (go.tag == "CashCounterTag")
        {
            anim.Play("Waving");
            float price = 0.0f;
            float totalPrice = 0.0f;
            string statement = "Items \t price\n\n";
            var objects = GameObject.FindGameObjectsWithTag("Destroyable");
            foreach (var obj in objects)
            {
                price = obj.GetComponent<ItemProperties>().price;                
                statement += obj.name + " \t " + price + "\n";
                totalPrice += price; 
            }
            statement += "\nTotal price:\t " + totalPrice + " euro";
            if (priceBilboard)
            {
                priceBilboard.SetText(statement);
            }
            else
            {
                print("no price bilboard found!");
            }
            
        }
    }
    
    private void CloneObject(GameObject go)
    {
        if (go.tag == "Selectable")
        {            
            objectClone = Instantiate(go, referencePosition.position + offset, Quaternion.identity) as GameObject;
            objectClone.tag = "Destroyable";
            objectClone.name = go.name;
            objectClone.GetComponent<Renderer>().material =  go.GetComponent<ItemProperties>().originalMaterial;
            offset -= new Vector3(0.55f, 0, 0);
        }
    }

    private void CloneObjectRearrange()
    {
        var objects = GameObject.FindGameObjectsWithTag("Destroyable");
        rePosition = referencePosition.position;
        foreach (var obj in objects)
        {
            obj.transform.position = rePosition;
            rePosition -= new Vector3(0.55f, 0, 0);
        }
    }
}
