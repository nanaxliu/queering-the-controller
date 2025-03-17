using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour
{
	public AudioSource winSound;
    public CharacterController checkPN;
    public GameObject menacingP1;
    public GameObject menacingP2;

    public Animator p1Anim;
    public Animator p2Anim;

    IEnumerator displayEndScreenP1()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("endScreenP1");
    }

    IEnumerator displayEndScreenP2()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("endScreenP2");
    }
	
	void OnTriggerEnter2D(Collider2D other)
    {
        checkPN = other.gameObject.GetComponent<CharacterController>();

        if (other.CompareTag("Player") && checkPN.PlayerNumber == 1)
        {
            winSound.Play();
            p1Anim.SetBool("won", true);
            p2Anim.SetBool("p2lose", true);
            menacingP1.gameObject.SetActive(true);
            StartCoroutine(displayEndScreenP1());
        }

        if (other.CompareTag("Player") && checkPN.PlayerNumber == 2)
        {
            winSound.Play();
            p1Anim.SetBool("lose", true);
            p2Anim.SetBool("p2win", true);
            menacingP2.gameObject.SetActive(true);
            StartCoroutine(displayEndScreenP2());
        }
    }
	
}
