using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractHitbox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Mouse mouse = Mouse.current;
    bool hitDetect;
    RaycastHit hit;
    IsHittable hittable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.leftButton.wasPressedThisFrame)
        {
            hitDetect = Physics.BoxCast(transform.position, transform.localScale * 0.5f, transform.forward, out hit, transform.rotation, 1f);
            if (hitDetect)
            {
                hittable = hit.collider.gameObject.GetComponent<IsHittable>();
                if (hittable != null)
                {
                    hittable.OnHit();
                    Debug.DrawLine(transform.position, hit.collider.gameObject.transform.position, Color.red, 0.5f);
                }
            }
        }
    }
}
