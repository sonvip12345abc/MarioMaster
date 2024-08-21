using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioChet : MonoBehaviour
{
    Vector2 VitriChet;
    public float TocDoNay = 20.5f;
    public float DoNayCao = 120f;
    private void Update()
    {
        StartCoroutine(HMarioChet());
        if (transform.localPosition.y <= -69f)
        {
            SceneManager.LoadScene(3);
        }
        IEnumerator HMarioChet()
        {

            while (true)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + TocDoNay * Time.deltaTime);
                if (transform.localPosition.y >= VitriChet.y + DoNayCao + 1)
                    break;
                yield return null;
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - TocDoNay * Time.deltaTime);
                if (transform.localPosition.y <= -70f)
                {

                    Destroy(gameObject);

                    break;

                }
                yield return null;
            }

        }
    }
}
