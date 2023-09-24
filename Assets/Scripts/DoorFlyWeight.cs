using UnityEngine;

[System.Serializable]
public class DoorFlyWeight: ScriptableObject
{
    /*public float radius = 5f; // Radio del área sensible.
    public LayerMask playerLayer; // La capa en la que se encuentra el jugador.
    public Material buyMaterial; // Material para cuando el jugador puede comprar.
    public Material notBuyMaterial; // Material para cuando el jugador no puede comprar.*/
    [SerializeField]
    private float radius;
    public float Radius => radius;

    [SerializeField]
    private LayerMask playerLayer;
    public LayerMask PlayerLayer => playerLayer;


    [SerializeField]
    private Material buyMaterial;
    public Material BuyMaterial => buyMaterial;


    [SerializeField]
    private Material notBuyMaterial;
    public Material NotBuyMaterial => notBuyMaterial;


}