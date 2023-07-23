# PrioritySetter

The PrioritySetter mod for Beat Saber allows you to adjust the priority of the game process to optimize performance. I wrote this because I got sick of setting the priority myself.

*This mod was written at 1 AM on a Wednesday, because who needs sleep?*

## Configuration

The available options are as follows:

| Enum Value    | Description                                                                 |
|---------------|-----------------------------------------------------------------------------|
| `Idle`        | Specifies an idle priority for the process.                                 |
| `BelowNormal` | Specifies a below-normal priority for the process.                          |
| `Normal`      | Specifies a normal priority for the process.                                |
| `AboveNormal` | Specifies an above-normal priority for the process.                         |
| `High`        | Specifies a high priority for the process.                                  |
| `RealTime`    | Specifies a real-time priority for the process. **Requires administrator privileges.** |

**Note:** Setting the process priority to `RealTime` requires administrator privileges. I did implement it but the game **WILL NEVER ASK FOR ADMINISTRATOR PRIVILEDGES**. As such, this mod **DOES NOT SUPPORT** setting the priority to `RealTime`
