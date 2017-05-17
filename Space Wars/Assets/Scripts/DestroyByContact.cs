using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyByContact : MonoBehaviour {
        public GameObject explosion;
        public GameObject playerExplosion;
        public GameObject destroyer;

        private GameController gameController;
        public int scoreValue;

        void Start()
        {
            GameObject gameControllerObject = GameObject.FindWithTag("GameController");
            if (gameControllerObject != null)
            {
                gameController = gameControllerObject.GetComponent<GameController>();
            }

            if (gameController == null)
            {
                Debug.Log("Cannot find GameController object");
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Boundary") return;

            if (other.tag == "Destroyer")
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                return;
            }

            Instantiate(explosion, transform.position, transform.rotation);
            if (other.tag == "Player")
            {
                Instantiate(playerExplosion, other.transform.position, transform.rotation);
                Instantiate(destroyer, other.transform.position, transform.rotation);
                gameController.EndGame();
            } else if(other.tag == "Bolt") gameController.AddScore(scoreValue);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
