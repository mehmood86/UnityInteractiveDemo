using UnityEngine;

public class ItemProperties : MonoBehaviour
{
    public float price;
    public Material originalMaterial;
    public Material highlightMaterial;    

    void OnMouseEnter()
    {        
        GetComponent<Renderer>().material = highlightMaterial;        
    }

    private void OnMouseExit()
    {     
        GetComponent<Renderer>().material = originalMaterial;
    }
}
