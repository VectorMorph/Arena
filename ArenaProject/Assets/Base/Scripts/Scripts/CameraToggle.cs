using UnityEngine;

public class CameraToggle : MonoBehaviour {

    [SerializeField]
    Behaviour[] DisableForThird;
    [SerializeField]
    Behaviour[] DisableForFirst;

    public bool ThirdPerson;


    private void Start()
    {
        for (int i = 0; i < DisableForFirst.Length; i++)
        {
            DisableForFirst[i].enabled = false;
            DisableForThird[i].enabled = true;
        }

        ThirdPerson = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("PerspectiveToggle") && ThirdPerson == false)
        {
            for (int i = 0; i < DisableForThird.Length; i++)
            {
                DisableForThird[i].enabled = false;
                DisableForFirst[i].enabled = true;
            }

            ThirdPerson = true;
        }
        else if(Input.GetButtonDown("PerspectiveToggle") && ThirdPerson == true)
        {
            for (int i = 0; i < DisableForFirst.Length; i++)
            {
                DisableForFirst[i].enabled = false;
                DisableForThird[i].enabled = true;
            }

            ThirdPerson = false;
        }
    }

}
