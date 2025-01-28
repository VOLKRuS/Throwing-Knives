using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core.Capabilities;
using ThrowingKnivesApi;
using Microsoft.Extensions.Logging;
using static ThrowingKnivesApi.IThrowingKnivesSharedAPI;

namespace ExampleModule;

public class ExampleModule : BasePlugin
{
    public override string ModuleName => "[TK] Drop Last Knife";
    public override string ModuleVersion => "1.0";
    public override string ModuleAuthor => "VOLK_RuS";
	
	private IThrowingKnivesSharedAPI? _api;
    private readonly PluginCapability<IThrowingKnivesSharedAPI> _pluginCapability = new("throwingknives:api");
	
	public override void OnAllPluginsLoaded(bool hotReload)
    {
        _api = _pluginCapability.Get();
        if (_api == null) 
		{
			Logger.LogError("Missing Throwing Knives API");
		}

		_api!.OnKnifeThrow += OnKnifeThrow;
    }
	
	private void OnKnifeThrow(CCSPlayerController player, PlayerSettings setts)
    {
		if (player == null || setts.Amount > 0) return;
		foreach (var weapon in player.PlayerPawn.Value!.WeaponServices!.MyWeapons)
		{
			if (weapon.Value != null)
			{
				if (weapon.Value.DesignerName.Contains("knife") || weapon.Value.DesignerName.Contains("bayonet"))
				{
					var activeWeapon = player.PlayerPawn.Value!.WeaponServices.ActiveWeapon.Value?.As<CBaseEntity>();
					player.DropActiveWeapon();
					activeWeapon?.AddEntityIOEvent("Kill", activeWeapon, null, "", 0.1f);
				}
			}
		}
	}
	
	public override void Unload(bool hotReload)
	{
		_api!.OnKnifeThrow -= OnKnifeThrow;
	}
}