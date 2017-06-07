// Copyright Gamelogic (c) http://www.gamelogic.co.za

namespace Gamelogic.Extensions.Internal
{
#if !UNITY_5
	/// <summary>
	/// A dummy attribute meant to mimic Unity 5's HelpURLAttribute so that scripts for Unity 5 can also compile
	/// under Unity 4.
	/// </summary>
	[Version(2, 4)]
	[AttributeUsage(AttributeTargets.Class)]
	public class HelpURLAttribute : Attribute
	{
		public HelpURLAttribute(string help)
		{
			//Do nothing
		}
	}
#endif
}