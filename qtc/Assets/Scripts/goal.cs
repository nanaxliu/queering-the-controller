using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour
{
    public GameObject rainbow;
	bool won = false;

	public AudioSource winSound;

    IEnumerator displayEndScreen()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("end");
        SceneManager.LoadScene("endScreen");
    }
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && won == false)
        {
            rainbow.gameObject.SetActive(true);
            winSound.Play();
            StartCoroutine(displayEndScreen());
            //won = true;
        }
    }
	
}
