using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] float reloadDelay = 1f;
    [SerializeField] float loadLevelDelay = 1f;
    [SerializeField] AudioClip rocketCrashSound;
    [SerializeField] AudioClip levelCompleteSound;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem explosionParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        AccessDebugCheats();    
    }

    void AccessDebugCheats()
    {
        ImmediateLevelTransition();
        TurnOffCollisions();
    }

    private void ImmediateLevelTransition()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    private void TurnOffCollisions()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggles collision On and Off
        }
    }

    void OnCollisionEnter(Collision other) {
        if (collisionDisabled) { return; }
        switch (other.gameObject.tag) 
        {
            case "Friendly":
                Debug.Log("Hit a friendly object");
                break;
            case "Finish":
                LandingSequence();
                break;
            case "Fuel":
                Debug.Log("You collected fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence() {
        if (isTransitioning == false) {
            audioSource.Stop();
            audioSource.PlayOneShot(rocketCrashSound);
            isTransitioning = true;
        }
        explosionParticles.Play();
        GetComponent<Movement>().enabled = false;       
        Invoke("ReloadLevel", reloadDelay);
    }

    void LandingSequence() {
        if (isTransitioning == false) {
            audioSource.Stop();
            audioSource.PlayOneShot(levelCompleteSound);
            isTransitioning = true;
        }
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadLevelDelay);
    }

    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
