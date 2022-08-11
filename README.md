# UniStory

A MapleStory emulator powered by Unity and still in development. Aim to support TMS v120 and later version.

There is no server implementation now. We plan to use other language to implement the server.

## Features

### Map

- [x] Tile
- [x] BGM
- [x] Foothold
- [x] Portal
- [x] Objs
- [x] Seats
- [x] Ladder / Rope
- [ ] Background (still in development)

### Physics

- [x] Foothold (still some bugs)
- [x] Gravity
- [ ] Move (still in development)

### Charactor

- [ ] All

### NetIO

- [ ] All


## Environment

- OS
  - Windows 10
- Unity Version
  - 2021.3.3f1
- IDE
  - Jetbrains Rider

## Development

You need to have wz files first(MapleStory assets pack). You can download these files on the Internet. We don't provide any download links, but you can find it at [here](#Links).

Put your wz files in `Assets/wz` (GMS v83 is OK, but you need to edit `WzMapleVersion` in `Loader.cs`). Now you can open Unity and write some code and test it in `SampleScene`!

## Links

### Client

[JourneyClient](https://github.com/SYJourney/JourneyClient)  
[HeavenClient](https://github.com/HeavenClient/HeavenClient)

### Server

[HeavenMS](https://github.com/ronancpl/HeavenMS)

### Wz Editor

[HaSuite / Harepacker resurrected](https://github.com/lastbattle/Harepacker-resurrected)
