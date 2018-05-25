using TechTalk.SpecFlow;

namespace ModernNotes.Specs.Helpers
{
	internal static class ScenarioContextExtensions
	{
		public static T GetOrDefault<T>(this ScenarioContext context, string key) where T:class
		{
			if (!context.ContainsKey(key)) return default(T);
			return context[key] as T;
		}
	}
}
