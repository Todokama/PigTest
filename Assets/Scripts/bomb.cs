using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public GameObject bom;
    private bool canPlant=true;
    public Animator dog;
    public Animator fermer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnBombButton()
    {
        if (canPlant)   
        {
            canPlant = false;
            StartCoroutine(Reload());

        }
    }
   
    IEnumerator Reload()
    {
       var obj = Instantiate(bom, transform.position, transform.rotation);
        
       yield return new WaitForSeconds(3);


        Destroy(obj);

        yield return new WaitForSeconds(2);
        
       canPlant = true;
    }

    

}

