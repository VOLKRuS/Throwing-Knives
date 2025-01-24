using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core.Capabilities;
using ThrowingKnivesApi;
using static ThrowingKnivesApi.IThrowingKnivesSharedAPI;

namespace ExampleModule;

public class ExampleModule : BasePlugin
{
    public override string ModuleName => "[TK] Api example";
    public override string ModuleVersion => "1.0";
    public override string ModuleAuthor => "VOLK_RuS";
	
	private IThrowingKnivesSharedAPI? _api;
    private readonly PluginCapability<IThrowingKnivesSharedAPI> _pluginCapability = new("throwingknives:api");
	
	public override void OnAllPluginsLoaded(bool hotReload)
    {
        _api = _pluginCapability.Get();
        if (_api == null) return;
		
		_api!.OnKnifeThrow += OnKnifeThrow; //Регистрация события броска ножа
		_api!.OnKnifeHit += OnKnifeHit; //Регистрация события попадания метательного ножа
		_api!.OnKnifeDeath += OnKnifeDeath; //Регистрация убийства с метательного ножа
    }

    public override void Load(bool hotReload)
    {
        AddCommand("css_tkgetsetts", "", (player, info) =>
        {
            if (player == null) return;
			
			ThrowingKnivesApi.PlayerSettings setts = new ThrowingKnivesApi.PlayerSettings();
   
            setts = _api!.GetSettings(player);//Получаем настройки игрока
        });
		
		AddCommand("css_tkupdsetts", "", (player, info) =>
        {
            if (player == null) return;
			
			ThrowingKnivesApi.PlayerSettings setts = new ThrowingKnivesApi.PlayerSettings();
   
            setts = _api!.GetSettings(player);//Получаем настройки игрока
			
			setts.Amount = 666;
			
			_api!.UpdateSettings(player, setts);// Обновляем настройки
        });
    }
	
	private void OnKnifeThrow(CCSPlayerController player, PlayerSettings setts)
	{
		Server.PrintToChatAll($"Knife thrown by {player.PlayerName}");
	}
	
	private void OnKnifeHit(CCSPlayerController attacker, CCSPlayerController victim, bool headshot)
	{
		Server.PrintToChatAll($"Player {victim.PlayerName} got throwing knife hit from {attacker.PlayerName}");
	}
	
	private void OnKnifeDeath(CCSPlayerController attacker, CCSPlayerController victim, bool headshot)
	{
		Server.PrintToChatAll($"Player {attacker.PlayerName} killed {victim.PlayerName} by throwing knife");
	}
}