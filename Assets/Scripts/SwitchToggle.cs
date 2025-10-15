using UnityEngine;

public class SwitchToggle : MonoBehaviour, IsHittable
{

    public GameObject toggleObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void OnHit()
    {
        IsToggleable toggleScript = toggleObject.GetComponent<IsToggleable>();
        toggleScript.OnToggle();
    }
}
