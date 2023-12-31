using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private Animator _playerAnimator;
    private GameObject Player;

    public void StartGame(){
        SceneManager.LoadSceneAsync("House");
    }
    
    public async void LoadScene(string sceneName) {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        // non-aktif movement dan animasi
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerMovement>().enabled = false;
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        
        _playerAnimator = Player.GetComponent<Animator>();
        _playerAnimator.enabled = false;
    
        await Task.Delay(500);
        scene.allowSceneActivation = true;
    }
}
