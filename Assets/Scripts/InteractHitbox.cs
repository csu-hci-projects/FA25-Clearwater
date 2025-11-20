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
    public LayerMask mask;
    public Animator playerAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse.leftButton.wasPressedThisFrame)
        {
            playerAnimator.SetBool("Interact", true);
            hitDetect = Physics.Raycast(transform.position, transform.forward, out hit, 2.5f, mask);
            if (hitDetect)
            {
                hittable = hit.collider.gameObject.GetComponent<IsHittable>();
                if (hittable != null)
                {
                    hittable.OnHit();
                    //Debug.DrawLine(transform.position, hit.collider.gameObject.transform.position, Color.red, 0.5f);
                }
            }
        }
    }
}
