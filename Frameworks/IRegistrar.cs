namespace Utilities.Frameworks
{
	/// <summary>
	/// Provides a registration pattern for arbitrary types.
	/// </summary>
	public interface IRegistrar<T>
	{
		void Register(T controller);
		void Deregister(T controller);
	}
}