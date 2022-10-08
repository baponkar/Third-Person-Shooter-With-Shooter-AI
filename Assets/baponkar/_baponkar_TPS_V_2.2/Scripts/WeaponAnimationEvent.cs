using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : UnityEvent<string>
{

}

public class WeaponAnimationEvent : MonoBehaviour
{
    public AnimationEvent weaponAnimationEvent = new AnimationEvent();
    public void OnAnimationEvent(string eventName)
    {
        weaponAnimationEvent.Invoke(eventName);
    }
}
