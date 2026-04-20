using UnityEngine;
using TMPro;

public class GenereEquation : MonoBehaviour
{   
    public TMP_Text EquationTexte;
    public TMP_Text texteTemps;
    public float tempsPasse;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        int GenererNombreX = Random.Range(0, 11);
        Debug.Log("Nombre Générer : "+ GenererNombreX);

        int GenererNombreY = Random.Range(0, 11);
        Debug.Log("Nombre Générer : "+ GenererNombreY);

      
        

        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        tempsPasse += Time.deltaTime;
        texteTemps.text = $"{tempsPasse:F1} S";
    }
}
