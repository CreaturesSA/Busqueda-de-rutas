using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Piece : MonoBehaviour
{
    public int x, y;
    public bool bomb;


    private void OnMouseDown()
    {
        if (bomb)
            GetComponent<SpriteRenderer>().material.color = Color.red;
        //else
        //    transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text =
        //        Generator.gen.GetBombsAround(x, y).ToString();


    }

}
