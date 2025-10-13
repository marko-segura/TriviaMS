using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class UIButtonSfx : MonoBehaviour, IPointerDownHandler
{
    public AudioClip clickClip;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!clickClip) return;
        if (SfxPlayer.I != null) SfxPlayer.I.PlayOneShot(clickClip, 1f);
        else Debug.LogWarning("No hay SfxPlayer en escena. Agrega SfxPlayer en una escena inicial.");
    }
}
