# Arcane Slime Siege

A three-level 2D top-down twin-stick shooter built in Unity 6 and C#, ending in a two-phase boss fight.

## Features

- **Elemental combat:** switch between basic, fire and water shots. Attacks deal double damage to weak elements (fire beats water and ice, water beats fire) and half damage to matching ones
- **Two-phase boss:** the boss shoots through a breakable shield. Once the shield breaks, it fires twice as fast and switches to a three-way spread shot, with separate health bars for each phase
- **Three levels** plus story and win scenes
- **Environment mechanics:** campfires that heal over time, water hazards and elemental weapon pickups
- **Player systems:** WASD movement, mouse aiming with a custom crosshair, health and damage, a following camera and a pause menu

## Controls

| Action | Input |
| --- | --- |
| Move | WASD / arrow keys |
| Aim | Mouse |
| Shoot | Left click |
| Switch weapon | 1 (basic), 2 (fire), 3 (water) |
| Pause | Esc |

## Tech stack

Unity 6 · C# · Rigidbody2D physics · TextMeshPro

## Key scripts

- `Bullet.cs`: collision handling and elemental damage multipliers
- `BossAI.cs` / `BossHealth.cs`: boss aiming, phase change and shield logic
- `PlayerShooting.cs`: fire rate, weapon switching and unlocks

## How to run

1. Clone the repository
2. Open the project in Unity Hub (Unity 6000.0.59f2 or later)
3. Open `Assets/Scenes 1/StoryScene.unity` and press Play
