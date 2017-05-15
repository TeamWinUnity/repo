using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyByContact : MonoBehaviour {
        public GameObject explosion;
        public GameObject playerExplosion;

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Boundary")
            {
                return;
            }

            Instantiate(explosion, transform.position, transform.rotation);

            if (other.tag == "Player")
            {
                Instantiate(playerExplosion, other.transform.position, transform.rotation);
                //GameController.GameOver();
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
