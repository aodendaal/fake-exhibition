using UnityEngine;
using UnityEngine.UI;

public class PlayerBusy : MonoBehaviour
{
    private PlayerInfo playerInfo;

    public bool IsBusy => busyObject != null;

    private GameObject busyObject;

    private float timeInitial;
    private float timeRemaining;

    [SerializeField]
    private Slider progressSlider;

    private void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        progressSlider.gameObject.SetActive(false);

    }

    public void ActBusy(GameObject busyObject, float duration)
    {
        if (this.busyObject != null)
        {
            Debug.LogError("Already busy");
        }

        this.busyObject = busyObject;
        timeInitial = duration;
        timeRemaining = duration;

        progressSlider.gameObject.SetActive(true);

        playerInfo.animator.SetBool("IsBusy", true);
    }

    private void Update()
    {
        if (!GameController.isStarted || GameController.isPaused)
            return;

        if (IsBusy)
        {
            if (!Input.GetButton($"{playerInfo.Input}Fire1"))
            {
                progressSlider.gameObject.SetActive(false);
                busyObject.GetComponent<ICompletable>().Cancel(gameObject);
                busyObject = null;

                playerInfo.animator.SetBool("IsBusy", false);
                return;
            }

            timeRemaining -= Time.deltaTime;
            progressSlider.value = 1 - (timeRemaining / timeInitial);

            if (timeRemaining <= 0)
            {
                progressSlider.gameObject.SetActive(false);
                busyObject.GetComponent<ICompletable>().Complete(gameObject);
                busyObject = null;

                playerInfo.animator.SetBool("IsBusy", false);
            }
        }
    }
}