using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Modules.Admin;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using ThrowingKnivesApi;
using static ThrowingKnivesApi.IThrowingKnivesSharedAPI;

namespace TKFlags;

public class TKFlags : BasePlugin, IPluginConfig<Config>
{
    public override string ModuleName => "[TK] Flags";
    public override string ModuleVersion => "1.0";
    public override string ModuleAuthor => "VOLK_RuS";
	
	private IThrowingKnivesSharedAPI? _api;
    private readonly PluginCapability<IThrowingKnivesSharedAPI> _pluginCapability = new("throwingknives:api");
	
	public Config Config { get; set; } = new ();
	
	public override void OnAllPluginsLoaded(bool hotReload)
    {
        _api = _pluginCapability.Get();
        if (_api == null) return;
    }
	
	public void OnConfigParsed(Config config)
    {
        Config = config;
    }

    public override void Load(bool hotReload)
    {
		RegisterEventHandler<EventPlayerSpawn>(EventPlayerSpawn);
    }
	
	private HookResult EventPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
	{
		var player = @event.Userid;
		
		foreach(var flags in Config.Flags)
		{
			var key = flags.Key;
			
			if (key.StartsWith("#"))
            {
                if (AdminManager.PlayerInGroup(player, key))
                {
					var fl = Config.Flags[key];
					
					AddTimer(fl.Delay, () => {
						
						ThrowingKnivesApi.PlayerSettings setts = new ThrowingKnivesApi.PlayerSettings();
						setts = _api!.GetSettings(player);//Получаем настройки игрока
						
						setts.Amount = fl.Amount;
						setts.Limit = fl.Limit;
						setts.Steal = fl.Steal;  
						setts.Velocity = fl.Velocity;  
						setts.Damage = fl.Damage;  
						setts.ModelScale = fl.ModelScale;  
						setts.Gravity = fl.Gravity;  
						setts.Elasticity = fl.Elasticity;  
						setts.Lifetime = fl.Lifetime;  
						setts.Spin = fl.Spin;  
						setts.HeadshotDamage = fl.HeadshotDamage;  
						
						_api!.UpdateSettings(player, setts);// Обновляем настройки
					});
					
                    return HookResult.Continue;
                }
            }
            else if (key.StartsWith("@"))
            {
                if (AdminManager.PlayerHasPermissions(player, key))
                {
					var fl = Config.Flags[key];
					
					AddTimer(fl.Delay, () => {
						
						ThrowingKnivesApi.PlayerSettings setts = new ThrowingKnivesApi.PlayerSettings();
						setts = _api!.GetSettings(player);//Получаем настройки игрока
						
						setts.Amount = fl.Amount;
						setts.Limit = fl.Limit;
						setts.Steal = fl.Steal;  
						setts.Velocity = fl.Velocity;  
						setts.Damage = fl.Damage;  
						setts.ModelScale = fl.ModelScale;  
						setts.Gravity = fl.Gravity;  
						setts.Elasticity = fl.Elasticity;  
						setts.Lifetime = fl.Lifetime;  
						setts.Spin = fl.Spin;  
						setts.HeadshotDamage = fl.HeadshotDamage;  
						
						_api!.UpdateSettings(player, setts);// Обновляем настройки
					});
					
                    return HookResult.Continue;
                }
            }
			
			break;
		}
		
		return HookResult.Continue;
	}
}

public class Setts
{	
	[JsonPropertyName("delay")]
	public float Delay { get; set; } = 1.0f;
	
	[JsonPropertyName("amount")]
	public int Amount { get; set; } = 5;

	[JsonPropertyName("limit")]
	public int Limit { get; set; } = -1;
	
	[JsonPropertyName("steal")]  
	public bool Steal { get; set; } = true;  

	[JsonPropertyName("velocity")]  
	public float Velocity { get; set; } = 1200.0f;  

	[JsonPropertyName("damage")]  
	public int Damage { get; set; } = 50;  

	[JsonPropertyName("model_scale")]  
	public float ModelScale { get; set; } = 1.0f;  

	[JsonPropertyName("gravity")]  
	public float Gravity { get; set; } = 1.0f;  

	[JsonPropertyName("elasticity")]  
	public float Elasticity { get; set; } = 0.2f;  

	[JsonPropertyName("lifetime")]  
	public float Lifetime { get; set; } = 3.0f;  

	[JsonPropertyName("spin")]  
	public bool Spin { get; set; } = true;  

	[JsonPropertyName("headshot_damage")]  
	public int HeadshotDamage { get; set; } = 100;  
}

public class Config : BasePluginConfig
{
	public Dictionary<string, Setts> Flags { get; set; } = new() {
		{ "@css/root", new Setts { Delay = 1.5f, Amount = 555, Damage = 999 } },
		{ "#css/admin", new Setts { Delay = 1.5f, HeadshotDamage = 123, Elasticity = 1.0f, Amount = 191 } },
	};
}