namespace HealthSystem {
	#region Delegates
	/// <summary>
	/// The signature of a method that is called when the health changes.
	/// </summary>
	public delegate void ChangeHealthDelegate(IHealth sender, HealthEventArgs e);
	#endregion

	#region Change Log
	/// <summary>
	/// Interface that is used when any object must listen for when a <c>GameObjects</c> <c>Health</c>.
	/// The methods <c>TakeDamage</c> and <c>HealDamage</c> handle what happens when affecting the healt.
	/// The Properties <c>IsDead</c> is used to determine if the <c>GameOjects</c> <c>Health</c> is at zero.
	/// 
	/// Created By: Andrew Carlton 24/11/2015>
	/// </summary>
	#endregion
	public interface IHealth {
		#region Properties
		/// <summary>
		/// Tell if the <c>GameObjects</c> <c>currentHealth</c> it equal or less then zero.
		/// </summary>
		/// <value><c>true</c> if this instance is dead; otherwise, <c>false</c>.</value>
		bool IsDead {
			get;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Reduse <c>currentHealth</c> by the damage.
		/// </summary>
		/// <param name="amount">Amount of damage taken.</param>
		void TakeDamage(int amount);

		/// <summary>
		/// Heals up the <c>currentHealth</c> by an amount.
		/// </summary>
		/// <param name="amount">Amount to be healed by.</param>
		void HealDamage(int amount);

		void UpdateHealth(IHealth sender, HealthEventArgs e);
		#endregion
	}
}
