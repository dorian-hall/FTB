using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float Range;

    IEnumerator[] Shoots = new IEnumerator[7];

    public Barrel FLBarrel;
    public Barrel FRBarrel;

    public Barrel RLBarrel;
    public Barrel RRBarrel;

    public Barrel LLBarrel;
    public Barrel LRBarrel;

    public Barrel BLBarrel;
    public Barrel BRBarrel;

    private Transform CurrentTarget = null; 
    private void Awake()
    {
        if (FLBarrel == null) { transform.GetChild(2).gameObject.SetActive(false);  }
        else { Shoots[0] = Shoot(FLBarrel, transform.GetChild(2)); }
        if (FRBarrel == null) { transform.GetChild(3).gameObject.SetActive(false);  }
        else { Shoots[1] = Shoot(FRBarrel, transform.GetChild(3)); }

        if (RLBarrel == null) { transform.GetChild(4).gameObject.SetActive(false);  }
        else { Shoots[2] = Shoot(FRBarrel, transform.GetChild(4)); }
        if (RRBarrel == null) { transform.GetChild(5).gameObject.SetActive(false);  }
        else { Shoots[3] = Shoot(FRBarrel, transform.GetChild(5)); }

        if (LLBarrel == null) { transform.GetChild(6).gameObject.SetActive(false);  }
        else { Shoots[4] = Shoot(FRBarrel, transform.GetChild(6)); } 
        if (LRBarrel == null) { transform.GetChild(7).gameObject.SetActive(false);  }
        else { Shoots[5] = Shoot(FRBarrel, transform.GetChild(7)); }
        
        if (BLBarrel == null) { transform.GetChild(8).gameObject.SetActive(false);  }
        else { Shoots[6] = Shoot(FRBarrel, transform.GetChild(8)); }
        if (BRBarrel == null) { transform.GetChild(9).gameObject.SetActive(false);  }
        else { Shoots[7] = Shoot(FRBarrel, transform.GetChild(9)); }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Enemy"&& CurrentTarget == null) { CurrentTarget = other.transform; }
        foreach (var enumerator in Shoots){StartCoroutine(enumerator);}
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Enemy" ) { return; }
        CurrentTarget = null;
        foreach (var enumerator in Shoots) { StopCoroutine(enumerator); }
    }

    IEnumerator Shoot(Barrel barrel, Transform barrelTrans)
    {
        Debug.Log("Shoot");
        yield return new WaitForSeconds(barrel.relaodSpeed);
        Shoot(barrel, barrelTrans);
    }

    private void Update()
    {
        if (CurrentTarget == null) { return; }
        transform.forward = transform.position - CurrentTarget.position;
    }

}