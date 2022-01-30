using BepInEx;

namespace ProliferatorMultiplier
{
	[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
	public class ProliferatorMultiplier : BaseUnityPlugin
	{
		private const string PluginGuid = "Proliferator_multiplier_configuration";
		private const string PluginName = "ProliferatorMultiplier";
		private const string PluginVersion = "0.1";


		private void Awake()
		{
			Logger.LogInfo($"Plugin {PluginName} is loaded!");

			Patch_Proliferator.ProliferatorStart(Config);
		}


		private void OnDestroy()
		{
			Patch_Proliferator.ProliferatorEnd();
		}
	}
}