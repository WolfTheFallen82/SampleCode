using UnityEngine;
using System;
using System.Collections;

namespace HealthSystem {
	public class HealthScript : MonoBehaviour, IHealth {
		#region Fields
		public Health health = new Health(100, 1000);
		public GUIText CurrentHealth;
		public GUIText MaxHealth;
		public GUIText IsDeadText;
		#endregion

		#region Properties
		public virtual bool IsDead {
			get {return health.IsDead;}
		}
		#endregion

		#region Unity Methods
		// Use this for initialization
		void Start () {
			MaxHealth.text = "/" + health.MaxHealth;
			CurrentHealth.text = health.CurrentHealth.ToString();
	
			health.ChangeHealthEvent += UpdateHealth;
		}
	
		//Update is called once per frame
		void Update() {
			if(Input.GetKeyDown(KeyCode.A))
				health.TakeDamage(UnityEngine.Random.Range(1, 10));
		
			if(Input.GetKeyDown(KeyCode.D))
				health.HealDamage(UnityEngine.Random.Range(1, 10));
		}
		#endregion

		#region Virtual Methods
		/// <summary>
		/// Reduse <c>currentHealth</c> by the damage.
		/// </summary>
		/// <param name="amount">Amount of damage taken.</param>
		public virtual void TakeDamage(int amount) {
			health.TakeDamage(amount);
		}
		
		/// <summary>
		/// Heals up the <c>currentHealth</c> by an amount.
		/// </summary>
		/// <param name="amount">Amount to be healed by.</param>
		public virtual void HealDamage(int amount) {
			health.HealDamage(amount);
		}

		public virtual void UpdateHealth(IHealth sender, HealthEventArgs e) {
			IsDeadText.enabled = (sender.IsDead) ? true : false;
	
			CurrentHealth.text = health.CurrentHealth.ToString();
			CurrentHealth.color = (sender.IsDead) ? Color.red : Color.white;
		}
		#endregion
	}
}
