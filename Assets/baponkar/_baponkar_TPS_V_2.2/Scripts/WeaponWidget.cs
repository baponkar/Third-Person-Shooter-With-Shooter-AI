using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWidget : MonoBehaviour
{
    public Text text;

   public void Refresh(int ammoCount)
   {
         text.text = ammoCount.ToString();
   }
}
