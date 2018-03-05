public static class EventManager
{
	public delegate void BasicEvent();

	public static BasicEvent OnPlayerDeath = () => { };
	public static BasicEvent OnEnemyDeath = () => { };

	public static void ResetEvent(ref BasicEvent @event)
	{
		@event = () => { };
	}
}
