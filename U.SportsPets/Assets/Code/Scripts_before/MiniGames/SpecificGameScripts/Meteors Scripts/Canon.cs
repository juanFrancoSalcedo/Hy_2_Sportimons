using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGame;

namespace InGame
{
    public class Canon : MonoBehaviour, ISportCreatureCompetitor, IEndMatchAffiliable, IFeastable
    {
        public int colorIndex { get; set; }
        public SportCreature athlete { get; set; }
        public CharacterAnimationManager animatorControl { get; set; }
        [SerializeField] private Spawner spawner;
        [SerializeField] private Vector3 initForce;
        [SerializeField] int seat;

        private void Start()
        {
            spawner.OnShoted += ApplyForce;
            spawner.OnBulletsLacked += Reload;
            ConfigureCreature();
            CalculateFeatures();
            if (GameController.Instance != null)
            {
                GameController.Instance.OnMatchEnded += EndMatchDelegate;
            }
        }

        public void CalculateFeatures()
        {
            spawner.delayBetwenSpawn = 1 - ((float)(athlete.accuracy + athlete.reflexes) / 20) + 0.1f;
        }

        private void ApplyForce(int count, GameObject bullet)
        {
            bullet.GetComponent<Rigidbody>().linearVelocity = initForce;
        }

        private void Reload()
        {
            spawner.Reload();
            spawner.Shot();
        }

        public void ConfigureCreature()
        {
            athlete = MeshCreatureConfigurator.Instance.GetPlayerCreature(seat);
            colorIndex = 0;
            GameObject waitCreature = Instantiate(athlete.meshCreature, transform);
            waitCreature.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            waitCreature.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            animatorControl = waitCreature.GetComponent<CharacterAnimationManager>();
            animatorControl.textureControl.SetTextures(athlete, colorIndex);
            if (GetComponent<Renderer>()) { GetComponent<Renderer>().enabled = false; }
        }

        public void EndMatchDelegate()
        {
            this.enabled = false;
            Celebrate();
        }

        public void Celebrate()
        {
            animatorControl.transform.rotation = Quaternion.Euler(new Vector3(0,180, 0));
            animatorControl.SetAnimationCharacter("Celebration",true);
        }
    }

}

