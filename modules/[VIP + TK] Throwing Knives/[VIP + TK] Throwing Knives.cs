using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using VipCoreApi;
using ThrowingKnivesApi;
using static ThrowingKnivesApi.IThrowingKnivesSharedAPI;

namespace TK_VIPCSSharp;

public class TK_VIPCSSharp : BasePlugin
{
	public override string ModuleName => "[VIP + TK] Throwing Knives";
	public override string ModuleVersion => "1.0";
	public override string ModuleAuthor => "VOLK_RuS";
	
	private ThrowingKnives _ThrowingKnives = null!;
	
	private IVipCoreApi? _VipApi;    
	private PluginCapability<IVipCoreApi> _VipCapability { get; } = new("vipcore:core");
	
	private IThrowingKnivesSharedAPI? _TKApi;
	private readonly PluginCapability<IThrowingKnivesSharedAPI> _TKCapability = new("throwingknives:api");
	
	public override void OnAllPluginsLoaded(bool hotReload)
	{
		_VipApi = _VipCapability.Get();
		if (_VipApi == null) return;
			
		_TKApi = _TKCapability.Get();
		if (_TKApi == null) return;
			
		_ThrowingKnives = new ThrowingKnives(_VipApi);
		_VipApi.RegisterFeature(_ThrowingKnives);
			
		_TKApi!.OnSettingsUpdate += OnSettingsUpdate;
	}

	public override void Unload(bool hotReload)
	{
		_VipApi?.UnRegisterFeature(_ThrowingKnives);
	}
	
	private void OnSettingsUpdate(CCSPlayerController player, PlayerSettings setts)
	{
		if(_VipApi!.IsClientVip(player) && _VipApi!.PlayerHasFeature(player, "ThrowingKnives") && _VipApi!.GetPlayerFeatureState(player, "ThrowingKnives") is IVipCoreApi.FeatureState.Enabled)
		{
			var settings = _VipApi!.GetFeatureValue<PlayerSettings>(player, "ThrowingKnives");
			_TKApi!.UpdateSettings(player, settings);
		}
	}
}

public class ThrowingKnives : VipFeatureBase
{
    	public override string Feature => "ThrowingKnives";

	public ThrowingKnives(IVipCoreApi api) : base(api)
	{
		
	}
}
