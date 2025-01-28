# Throwing Knives

Приобрести приватную версию плагина можно как напрямую [VOLK_RuS](https://discord.com/users/222462171500642304), так и через дискорд канал [Stellar Team](https://discord.com/invite/stellarteam) в полу-автоматическом режиме.

В версии 2.0 появился новый параметр, позволяющий изменить кнопку броска — "KnifeThrowButton": 1, где 1 - это левая кнопка мыши

Все ID кнопок можно посмотреть тут - https://docs.cssharp.dev/api/CounterStrikeSharp.API.PlayerButtons.html

Примеры можно посмотреть в modules/[TK] Example

Возможности API:

```c#
public class PlayerSettings //Используется для получения/обновления настроек игрока
{
  public int Amount { get; set; } = 5;
  public int Limit { get; set; } = -1;
  public bool Steal { get; set; } = true;
  public float Velocity { get; set; } = 1200.0f;
  public int Damage { get; set; } = 50;
  public float ModelScale { get; set; } = 1.0f;
  public float Gravity { get; set; } = 1.0f;
  public float Elasticity { get; set; } = 0.2f;
  public float Lifetime { get; set; } = 3.0f;
  public bool Spin { get; set; } = true;
  public int HeadshotDamage { get; set; } = 100;
}

public interface IThrowingKnivesSharedAPI
{
  event Action<CCSPlayerController, PlayerSettings> OnKnifeThrow;                  // Событие броска ножа
  event Action<CCSPlayerController, PlayerSettings> OnSettingsUpdate;              // Событие обновления настроек (Не вызывается при ручном обновлении)
  event Action<CCSPlayerController, CCSPlayerController, bool> OnKnifeHit;         // Событие попадания ножа по игроку (attacker, victim, headshot)
  event Action<CCSPlayerController, CCSPlayerController, bool> OnKnifeDeath;       // Событие убийства игрока с помощью метательного ножа (attacker, victim, headshot)
	
  public void UpdateSettings(CCSPlayerController player, PlayerSettings settings); // Обновление настроек
  public PlayerSettings GetSettings(CCSPlayerController player);                   // Получение настроек
}
```

# Настройки для VIP группы:
```Json
"ThrowingKnives":
{
  "Amount": 5,
  "Limit": -1,
  "Steal": false,
  "Velocity": 1200,
  "Damage": 60,
  "ModelScale": 1,
  "Gravity": 1,
  "Elasticity": 0.3,
  "Lifetime": 3,
  "Spin": true,
  "HeadshotDamage": 150
}
```
