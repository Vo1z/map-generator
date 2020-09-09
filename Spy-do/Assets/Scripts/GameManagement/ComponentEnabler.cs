/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: ROB1N (Ivan Fomenko)
 * Email: iv_fomenko@bk.ru
 * Twitter: @ROB1N21806
 */

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
