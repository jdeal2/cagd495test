using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Components
{


	public class Health: CustomComponentBase {
		public float CurStuff= 0f;
        public int startingHealth = 100;
        public int currentHealth;
		float pointValue;
		List<GameObject> collectedLoot;
        bool isDead = false;

		public void TotalStuff(){
			GameObject[] tempList = GameObject.FindGameObjectsWithTag ("Loot");
			float maxStuff = tempList.Length;
			pointValue = 1f / maxStuff;
			collectedLoot = new List<GameObject> ();
            currentHealth = startingHealth;

		}

		void OnTriggerEnter(Collider col){
			if (col.gameObject.tag == "Loot") {
				CurStuff += pointValue;
				collectedLoot.Add (col.gameObject);
				col.gameObject.SetActive (false);
			}
		}

        void OnCollisionEnter(Collider col)
        {
            if (col.gameObject.tag == "Enemy")
            {
                currentHealth -= 10;
                Debug.Log(col.gameObject);
            }
        }

        public void TakeDamage(float damage){
			for (int relicsLost = 0; relicsLost < damage; relicsLost++) {
				GameObject turnOn = collectedLoot [Random.Range (0, collectedLoot.Count)];
				collectedLoot.Remove (turnOn);
				turnOn.SetActive (true);
				CurStuff -= pointValue;
                if (currentHealth <= 0 && !isDead)
                {
                    Death();
                }
            }

		}

		public void Death(){
            isDead = true;

            Application.LoadLevel(Application.loadedLevel);

			Debug.Log ("death things");
		}


	}
}