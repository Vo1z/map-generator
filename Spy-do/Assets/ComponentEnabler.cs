using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("EnableAStar");
    }

    IEnumerator EnableAStar()
    {
        yield return new WaitForSeconds(1f);

        FindObjectOfType<Pathfinding>().enabled = true;
        FindObjectOfType<Grid>().enabled = true;
    }


}
