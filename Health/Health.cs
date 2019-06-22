namespace HealthSystem {
	#region Change Log
	/// <summary>
	/// The health of the <c>GameObject</c>. When the <c>currentHealth</c> is at zero
	/// the <c>GameObject</c> is dead. The <c>GameObjects</c> <c>currentHealth</c> is 
	/// effected when <C>TakeDamage</c> is called and healed when <c>HealDamage</c> is 
	/// called. When ever <c>CurrentHealth</c> <c>MaxHealth</c> and <c>TotalHealth</c>
	/// will call the event <c>ChangeHealthEvent</c> to notifiy any listeners.
	/// 
	/// Created By: Andrew Carlton 24/11/2015>
	/// </summary>
	#endregion
	public class Health : IHealth {
		#region Fields
		/// <summary>
		/// The total health that a <c>GameObject</c> can have. 
		/// </summary>
		/// <value>100</value>
		private int totalHealth = 100;	

		/// <summary>
		/// The max health that a <c>GameObject</c> can currently have.
		/// </summary>
		/// <value>100</value>
		private int maxHealth = 100;

		/// <summary>
		/// The current heath that a <c>GameObject</c> has.
		/// </summary>
		private int currentHealth = 0;
		#endregion
		
		#region Events
		/// <summary>
		/// Occurs when the health has changed in anyway.
		/// </summary>
		public event ChangeHealthDelegate ChangeHealthEvent;
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the <c>totalHealth</c> and fires the event <c>ChangeHealthEvent</c>.
		/// </summary>
		/// <value>The new total health.</value>
		public virtual int TotalHealth {
			get {return totalHealth;}
			set {
				totalHealth = (value < maxHealth) ? maxHealth : value;

				HandleChangeHealth();
			}
		}

		/// <summary>
		/// Gets or sets the <c>maxHealth</c> and fires the event <c>ChangeHealthEvent</c>.
		/// </summary>
		/// <value>The max health.</value>
		public virtual int MaxHealth {
			get {return maxHealth;}
			set {
				maxHealth = (value > totalHealth) ? totalHealth :
					(value < currentHealth) ? currentHealth : value;

				HandleChangeHealth();
			}
		}

		/// <summary>
		/// Gets or sets the <c>currentHealth</c> and fires the event <c>ChangeHealthEvent</c>.
		/// </summary>
		/// <value>The current health.</value>
		public virtual int CurrentHealth {
			get {return currentHealth;}
			set {
				currentHealth = (value > maxHealth) ? maxHealth : 
					(value < 0) ? 0 : value;

				HandleChangeHealth();
			}
		}

		/// <summary>
		/// Tell if the <c>GameObjects</c> <c>currentHealth</c> it equal or less then zero.
		/// </summary>
		/// <value><c>true</c> if this instance is dead; otherwise, <c>false</c>.</value>
		public virtual bool IsDead {
			get {return currentHealth <= 0;}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="HealthSystem.Health"/> class.
		/// </summary>
		/// <param name="max">Max health of the <c>GameObject</c>.</param>
		/// <param name="total">Total health of the <c>GameObject</c>.</param>
		public Health(int max = 100, int total = 100){
			TotalHealth = total;
			MaxHealth = max;
			CurrentHealth = MaxHealth;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HealthSystem.Health"/> class from
		/// and exsisting <c>Health</c> object.
		/// </summary>
		/// <param name="org">An exsiting <c>Health</c> object to be copied.</param>
		public Health(Health org) {
			totalHealth = org.totalHealth;
			maxHealth = org.maxHealth;
			currentHealth = org.currentHealth;

			ChangeHealthEvent = org.ChangeHealthEvent;
		}
		#endregion

		#region Virtual Methods
		/// <summary>
		/// Increases the <c>maxHealth</c>.
		/// </summary>
		/// <param name="amount">Amount that the new <c>maxHealth</c> will increass by.</param>
		public virtual void IncreaseHeath(int amount){
			MaxHealth += amount;
		}

		/// <summary>
		/// Reduse <c>currentHealth</c> by the damage.
		/// </summary>
		/// <param name="amount">Amount of damage taken.</param>
		public virtual void TakeDamage(int amount) {
			CurrentHealth -= amount;
		}
		
		/// <summary>
		/// Heals up the <c>currentHealth</c> by an amount.
		/// </summary>
		/// <param name="amount">Amount to be healed by.</param>
		public virtual void HealDamage(int amount) {
			CurrentHealth += amount;
		}

		/// <summary>
		/// Resets the <c>currentHealth</c> back to the <c>maxHealth</c>.
		/// </summary>
		public virtual void ResetHealth() {
			CurrentHealth = MaxHealth;
		}

		/// <summary>
		/// Sets the <c>currentHealth</c> to zero.
		/// </summary>
		public virtual void Dead() {
			CurrentHealth = 0;
		}
		
		/// <summary>
		/// If there are any listeners to the <c>ChangeHealthEvent</c> then it will send out
		/// the message to all the listeners.
		/// </summary>
		/// <param name="e">Any arguments that need to be passed along.</param>
		public virtual void HandleChangeHealth(HealthEventArgs e = null){
			if(ChangeHealthEvent != null)
				ChangeHealthEvent(this, e);
		}

		public virtual void UpdateHealth(IHealth sender, HealthEventArgs e){
			HandleChangeHealth(e);
		}

		/// <summary>
		/// Initializies an empty <c>Health</c> object with the current object.
		/// </summary>
		/// <param name="org">Org.</param>
		public virtual void Clone(ref Health org) {
			org.totalHealth = totalHealth;
			org.maxHealth = maxHealth;
			org.currentHealth = currentHealth;

			org.ChangeHealthEvent = ChangeHealthEvent;
		}
		#endregion
	}
}