using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Animator anim;
    public string id;
    public bool activationStatus = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    [ContextMenu("Generate CheckPoint ID")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            ActivateCheckPoint();
    }
    public void ActivateCheckPoint()
    {
        if(activationStatus == false)
        {
            if (AudioManager.instance == null)
            {
                Debug.LogError("AudioManager.instance is null!");
                return;
            }
            AudioManager.instance.PlaySFX(4,transform);
        }
        if(anim  == null)
        {
            Debug.LogError("Animator component not found on CheckPoint!");
            return;
        }

        activationStatus = true;
        anim.SetBool("active", true);
    }
}
