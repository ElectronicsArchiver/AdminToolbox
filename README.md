# AdminToolbox
This is a plugin for SCL:Secret Lab servers. 
It offers multiple admin tools like 
* Friendly fire logs in server console
* Godmode, ability to turn off damage for players
* Heal & SetHP
* Warp Points
* Teleport to cordinates
* Prevent the round from ending
* Player Joined messages in server log

and more commands, and other features that do not exist in the base game (yet).
There is also more to come!

## Plugin Installation:
To install:
1. Grab newest version of AdminToolbox: [Latest Release](https://github.com/Rnen/AdminToolbox/releases/latest)
> If you're using **Smod 2.2.1**, use this instead: [For Smod-2.2.1](https://github.com/Rnen/AdminToolbox/releases/tag/1.0)
2. Navigate to your SCP Secret Lab folder.
2. Drag AdminToolbox.dll into the sm_plugins folder

## ServerMod
ServerMod is a server side plugin system with a bunch of additional configuration options, bug fixes, security patches and some optimisations built in.
 * SMOD can be found here: [Smod Github](https://github.com/Grover-c13/Smod2)
 * SMOD Discord: https://discord.gg/8nvmMTr

## Administration COMMANDS / Gameplay COMMANDS
Command | Value Type | Value Type | Description
--- | :---: | :---: | ---
PLAYERS |   |   | Lists all players + IP's + SteamID's.
GOD | Player | Boolean | Turns on/off godmode for that player. Use without (bool) to toggle. Add `nodmg` after (bool) to turn on NoDmg as as well for this player
GOD | LIST |   | List all players with GODMODE enabled
NODMG | Player | Boolean | Turns on/off damageOutput for that player. Use without (bool) to toggle.
NODMG | LIST |  | List all players with NoDmg enabled
HP/SETHP | Player | Integer | Sets player HP to (Integer).
HEAL | Player | Integer | Heals player for (Integer) amount. Use without (Integer) to set to Role default max HP.
TPX | Player1 | Player2 | Teleports Player1 to Player2. (Will not grab all players names that contain the string you entered like TP does, only the closest match)
TUT/TUTORIAL | Player |  | Sets player role to TUTORIAL.
ROLE  | Player | ROLEID | Sets player to ROLE without teleporting them to ROLE SPAWN or giving items. (Previously **CLASS** Command)
~~KEEP/KEEPSETTINGS~~| ~~Player~~| ~~Boolean~~ |UNFINISHED, DOES NOTHING ATM ~~Enables keeping player settings on round restart.~~
PLAYER | Player |   | Lists all values for the specified Player
ROUNDLOCK / RLOCK | Boolean |   | Turns on/off round ending
BREAKDOORS / BD | Player | Boolean | Turns on/off breaking doors for that player.
LOCKDOWN | Boolean |  | Locks all doors for all players
SPECTATOR / SPEC | Player | Boolean | Player will never spawn naturally. Handy for admins spectating suspicious activity

>`*` can be used instead of `Player` to target **all** players on the server

>Find a complete list of Role ID's & Item ID's [HERE](https://github.com/Rnen/AdminToolbox/blob/master/.github/RESOURCES.md)

### Advanced Commands (Theese are more experiemental and is hard to use)
Command | Value Type | Value Type |  Value_Type | Description
--- | :---: | :---: | :---: | ---
WARP | ADD | Player | WarpName | Adds a warp point where `Player` is
WARP | REMOVE | WarpName |   | Removes `WarpName` from WarpPoints
WARP | LIST |   |   |  Lists current warp points
WARP | Player | WarpName |   |  Teleports `Player` to `WarpName`
POS | Player | ADD  |  x=5 y=10 | Teleports player 5 on X axis, 10 on Y axis (up).  (Dont need to use all X Y Z)
POS | Player | SET  |  x=50 y=0 z=-50  | Sets player position to X:50 Y:0 Z:-50
POS | Player | GET | |  Gets XYZ position of `Player`

> Player input dont need the full name, it will grab the closest looking name to what you entered
## ^Theese commands work in both server console and remote admin!^

## Config Additions
Type Info:
- Boolean: True or False value
- Integer: Any whole number
- Float: A number with decimals (Formatting like "1.5" and "1,5" both work and are the same value)
- List: A list of items separated by ",", for example: `list: 1,2,3,4,5`
- Dictionary: A dictionary of items separated by ":", and each entry separated by ",", for example: `dictionary: 1:2,2:3,3:4`
- Seconds: Time in seconds, usually a value of -1 disables the feature
- Minutes: Time in minutes, usually a value of -1 disables the feature
- R: If the config option has an R before it, it means that you can use a random value in it. A random value is defined by having "{}", items listed like "weight%value" where if you don't put a weight it defaults to a weight of 1, separated by "|", for example: `rlist: {1%1|2%7|6},3,6,{15%3|2|45%2}`
- STEAMID64: [Find yours here!](https://steamid.io/lookup)

>Crossed out config options are removed, unless otherwise specified in the description

## If you do not intend to change the default values, theres no need to include any of theese config settings
### General Settings
Config Option | Value Type | Default Value | Description
--- | :---: | :---: | ---
admintoolbox_tutorial_dmg_allowed | List | -1 | What damage types the TUTORIAL role is allowed to take. -1 equals basically godmode
admintoolbox_endedRound_damageMultiplier | Integer | 1 | Multiplies all damage by this number after round ends. For maximum chaos enter high number (10 or something) To turn off dmg on round ended, enter `0`.
admintoolbox_debug_friendly_kill | Boolean | True | Displays teamkills in server console.
admintoolbox_debug_player_kill | Boolean | False | Displays all non-friendly kills in server console.
admintoolbox_debug_scp_and_self_killed | Boolean | False | Displays suicides and SCP kills in server console.
admintoolbox_debug_friendly_damage | Boolean | False | Displays team damage in server console.
admintoolbox_debug_player_damage | Boolean | False | Displays non-team damage in server colsole.
admintoolbox_debug_damagetypes | List | 5, 13, 14, 15, 16, 17 | What damage types to detect.
~~admintoolbox_writeTkToFile~~ | ~~Boolean~~ | ~~False~~ | This isnt actually working yet ~~When enabled it creates a "AdminToolbox_TKLog.txt" in your server folder (Not sure how this performs with multiple servers running)~~
admintoolbox_debug_player_player_joinANDleave | Boolean | False | Writes Playername in server console when player joins & leaves (Currently only works for players joining)
admintoolbox_intercom_extended_IDs_whitelist | STEAMID64 List | | Whitelist of people who use admintoolbox's intercom duration settings. See example below
admintoolbox_intercom_extended_duration | Seconds | 1000 | How long whitelisted people can talk. 
admintoolbox_intercom_extended_cooldown | Seconds | 0 | Cooldown after talking. 

Whitelist example:
```yaml
admintoolbox_intercom_extended_IDs_whitelist: 00000000000000000, 0000000000000001, 00000000000000003
```
>Find a complete list of DamageID's [HERE](https://github.com/Rnen/AdminToolbox/blob/master/.github/RESOURCES.md#damagetype-ids)
#

### Place any suggestions/problems in [issues](https://github.com/Rnen/AdminToolbox/issues)!

# Thanks & Enjoy.

