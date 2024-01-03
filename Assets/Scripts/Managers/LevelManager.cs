using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private Animator _playerAnimator;
    private GameObject Player;

    [SerializeField] PlayerData playerData;

    public void StartGame(){
        SceneManager.LoadSceneAsync("House");
    }

    private void OnEnable() {
        GameEventsManager.instance.levelEvents.onLevelLoad += LoadScene;
    }

    private void OnDisable() {
        GameEventsManager.instance.levelEvents.onLevelLoad -= LoadScene;
    }
    
    private async void LoadScene(string sceneName, Vector2 spawnLocation) {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        // non-aktif movement dan animasi
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerMovement>().enabled = false;
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        
        _playerAnimator = Player.GetComponent<Animator>();
        _playerAnimator.enabled = false;
    
        await Task.Delay(500);
        playerData.spawnLocation = spawnLocation;
        Debug.Log(playerData.spawnLocation);
        scene.allowSceneActivation = true;
    }
}
