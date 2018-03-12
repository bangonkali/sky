## Sky

The best Windows window manager.

### Hot to use?

1. On your favorite terminal, add sky.exe to path. 
1. Create a configuraiton `sky --createsampleconfig`.
1. Modify your configuration using your favorite text editor.
1. Run `sky`.

### List Screens
```
➜  Debug git:(master) ✗ ./Sky --screens
Left       Top        Width      Height     Display          ?         
0          0          1366       768        \\.\DISPLAY1     Primary Display
```

### List Windows
```
➜  Debug git:(master) ✗ ./Sky --windows
Process Name                   Window Title                                                
chrome                         * Projects | Trello - Google Chrome
SnippingTool                   Snipping Tool
notepad++                      C:\Users\Gil\Documents\.sky - Notepad++
Hyper                          Hyper
SystemSettings                 Settings
WinStore.App                   Microsoft Store
devenv                         Sky - Microsoft Visual Studio
ApplicationFrameHost           Skype
```

### Json Configuration

Create sample configuration. The sample config will arrange up to 9 notepad processes in the main window. 

```
➜  Debug git:(master) ✗ ./Sky --createsampleconfig
```

```json
{
  "Items": [
    {
      "Padding": {
        "Left": -14,
        "Right": -14,
        "Top": -8,
        "Bottom": -8
      },
      "WidthGrid": 3.0,
      "HeightGrid": 3.0,
      "Width": 1.0,
      "Height": 1.0,
      "Top": 0.0,
      "Left": 0.0,
      "Name": "notepad",
      "DesktopGroup": "",
      "NameType": 1,
      "Display": "\\\\.\\DISPLAY1"
    },
    {
      "Padding": {
        "Left": -14,
        "Right": -14,
        "Top": -8,
        "Bottom": -8
      },
      "WidthGrid": 3.0,
      "HeightGrid": 3.0,
      "Width": 1.0,
      "Height": 1.0,
      "Top": 1.0,
      "Left": 0.0,
      "Name": "notepad",
      "DesktopGroup": "",
      "NameType": 1,
      "Display": "\\\\.\\DISPLAY1"
    },
    {
      "Padding": {
        "Left": -14,
        "Right": -14,
        "Top": -8,
        "Bottom": -8
      },
      "WidthGrid": 3.0,
      "HeightGrid": 3.0,
      "Width": 1.0,
      "Height": 1.0,
      "Top": 2.0,
      "Left": 0.0,
      "Name": "notepad",
      "DesktopGroup": "",
      "NameType": 1,
      "Display": "\\\\.\\DISPLAY1"
    },
    {
      "Padding": {
        "Left": -14,
        "Right": -14,
        "Top": -8,
        "Bottom": -8
      },
      "WidthGrid": 3.0,
      "HeightGrid": 3.0,
      "Width": 1.0,
      "Height": 1.0,
      "Top": 0.0,
      "Left": 1.0,
      "Name": "notepad",
      "DesktopGroup": "",
      "NameType": 1,
      "Display": "\\\\.\\DISPLAY1"
    },
    {
      "Padding": {
        "Left": -14,
        "Right": -14,
        "Top": -8,
        "Bottom": -8
      },
      "WidthGrid": 3.0,
      "HeightGrid": 3.0,
      "Width": 1.0,
      "Height": 1.0,
      "Top": 1.0,
      "Left": 1.0,
      "Name": "notepad",
      "DesktopGroup": "",
      "NameType": 1,
      "Display": "\\\\.\\DISPLAY1"
    },
    {
      "Padding": {
        "Left": -14,
        "Right": -14,
        "Top": -8,
        "Bottom": -8
      },
      "WidthGrid": 3.0,
      "HeightGrid": 3.0,
      "Width": 1.0,
      "Height": 1.0,
      "Top": 2.0,
      "Left": 1.0,
      "Name": "notepad",
      "DesktopGroup": "",
      "NameType": 1,
      "Display": "\\\\.\\DISPLAY1"
    },
    {
      "Padding": {
        "Left": -14,
        "Right": -14,
        "Top": -8,
        "Bottom": -8
      },
      "WidthGrid": 3.0,
      "HeightGrid": 3.0,
      "Width": 1.0,
      "Height": 1.0,
      "Top": 0.0,
      "Left": 2.0,
      "Name": "notepad",
      "DesktopGroup": "",
      "NameType": 1,
      "Display": "\\\\.\\DISPLAY1"
    },
    {
      "Padding": {
        "Left": -14,
        "Right": -14,
        "Top": -8,
        "Bottom": -8
      },
      "WidthGrid": 3.0,
      "HeightGrid": 3.0,
      "Width": 1.0,
      "Height": 1.0,
      "Top": 1.0,
      "Left": 2.0,
      "Name": "notepad",
      "DesktopGroup": "",
      "NameType": 1,
      "Display": "\\\\.\\DISPLAY1"
    },
    {
      "Padding": {
        "Left": -14,
        "Right": -14,
        "Top": -8,
        "Bottom": -8
      },
      "WidthGrid": 3.0,
      "HeightGrid": 3.0,
      "Width": 1.0,
      "Height": 1.0,
      "Top": 2.0,
      "Left": 2.0,
      "Name": "notepad",
      "DesktopGroup": "",
      "NameType": 1,
      "Display": "\\\\.\\DISPLAY1"
    }
  ]
}
```