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
                gameController = gameControllerObject.GetComponent<GameController>();

            if (gameController == null)
                Debug.Log("Cannot find GameController object");
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) return;

            if (other.tag == "Destroyer")
            {
                if (explosion != null) Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                return;
            }

            if(explosion != null) Instantiate(explosion, transform.position, transform.rotation);
            switch (other.tag)
            {
                case "Player":
                    Instantiate(playerExplosion, other.transform.position, transform.rotation);
                    Instantiate(destroyer, other.transform.position, transform.rotation);
                    gameController.EndGame();
                    break;
                case "Bolt":
                    gameController.AddScore(scoreValue);
                    break;
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
