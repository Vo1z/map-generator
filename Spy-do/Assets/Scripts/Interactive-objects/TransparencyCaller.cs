/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */

/*
 * SCRIPT REQUIREMENTS
 * GameObject of this script and other Collider2D that will be interacted with current GameObject
 * must contain Collider2D and Rigitbody2D. {Body Types} of these Rigitbodies must be set to "Kinematic"
 * and "Simulated". Otherwise script will not work.
 */

using UnityEngine;

public class TransparencyCaller : MonoBehaviour
{
    public float transparencyIndex = 0.8f;
    
    private Renderer renderer_;
    private Color transparentColor_;
    private Color orignalColor_;
    
    private void Awake()
    {
        Debug.Log("Awake");
        this.renderer_ = this.GetComponent<Renderer>();
        this.orignalColor_ = this.GetComponent<SpriteRenderer>().color;
        this.transparentColor_ = new Color(1.0f, 1.0f, 1.0f, transparencyIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter");
        if (this.renderer_.sortingOrder <= other.GetComponent<Renderer>().sortingOrder && other.gameObject.CompareTag("Environment"))
        {
            other.GetComponent<SpriteRenderer>().color = transparentColor_;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit");
        if (this.renderer_.sortingOrder <= other.GetComponent<Renderer>().sortingOrder && other.gameObject.CompareTag("Environment"))
        {
            other.GetComponent<SpriteRenderer>().color = orignalColor_;
        }
    }
}