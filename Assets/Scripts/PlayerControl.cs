using UnityEngine;
using UnityEngine.Playables;


public class PlayerControl: MonoBehaviour
{
    [SerializeField, Header("How far player can rewind")] private float rewindPower;

    [SerializeField, Header("Button to pause")] private KeyCode pauseButton;
    [Space, SerializeField, Header("Playable Director")] private PlayableDirector director;

    [SerializeField] private Canvas canvas;



    private bool isPaused = false;
    

    private void CheckPause()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            director.Pause();
            canvas.gameObject.SetActive(true);
        }
        else
        {
            director.Resume();
            canvas.gameObject.SetActive(false);
        }

    }

    private void Rewind()
    {
        if (Input.GetButtonDown("Horizontal") && !isPaused)
        {
            float dir = Input.GetAxis("Horizontal");

            if (dir > 0) dir = 1;
            else dir = -1;
            director.time += dir * rewindPower;
        }
    }

    private void CheckBorders()
    {
        if (director.time < 0)
            director.time = 0;

        if (director.time >= director.duration - 2)
        {
            Debug.Log("Nearly over!");
            isPaused = true;
        }
    }


    private void Update()
    {
        CheckBorders();

        CheckPause();

        Rewind();
    }

}
