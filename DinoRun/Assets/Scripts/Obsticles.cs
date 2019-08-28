using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticles : MonoBehaviour
{
    protected GameObject m_RefToChild = null;
    protected Vector3 m_vOffSet = new Vector3(0.0f, 0.0f, 0.0f);
    protected Quaternion m_rRotationOffset = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    protected Vector3 m_vScale = new Vector3(1.35f, 1.35f, 1.35f);


    // Start is called before the first frame update
    void Start()
    {
        SetCurrentTheme(GetComponentInParent<Spawner>().GetCurrentTheme());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void SetCurrentTheme(ECurrentTheme _eTheme)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        print("Hit");
        if("Player" == other.tag)
        {
            GameManager.s_bIsRunning = false;
        }
    }
}
