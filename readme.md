## Sky

The best Windows window manager.

### Hot to use?

1. On your favorite terminal, add sky.exe to path. 
1. Create a configuraiton `sky --createsampleconfig`.
1. Modify your configuration using your favorite text editor.
1. Run `sky`.

### Json Configuration

Create sample configuration. The sample config will arrange up to 9 notepad processes in the main window. 

```
sky --createsampleconfig
```

```json
{
  "Items": [
    {
      "WidthGrid": 3.0,
      "HeightGrid": 3.0,
      "Width": 1.0,
      "Height": 1.0,
      "Top": 0.0,
      "Left": 0.0,
      "Name": "notepad",
      "DesktopGroup": "",
      "NameType": 1
    },
    ...
    {
      "WidthGrid": 3.0,
      "HeightGrid": 3.0,
      "Width": 1.0,
      "Height": 1.0,
      "Top": 1.0,
      "Left": 0.0,
      "Name": "notepad",
      "DesktopGroup": "",
      "NameType": 1
    }
  ]
}
```