using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    Animator _doorAnim;

    public void OnTriggerEnter(Collider other)
    {
        _doorAnim.SetBool("isOpening", true);
    }
    // Start is called before the first frame update
    void Start()
    {
        _doorAnim = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
