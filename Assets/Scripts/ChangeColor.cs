using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

public Color defaultcolor;
public Color newcolor;
public Renderer render;

private void OnMouseOver()
{
render  = GetComponent<Renderer>();
render.material.color  = newcolor ;

}
    // Start is called before the first frame update
   

   private void OnMouseExit()
   {
render = GetComponent<Renderer>();
render.material.color = defaultcolor ;

}
}