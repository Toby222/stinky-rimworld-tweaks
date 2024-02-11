# RimWorld Mod Template

This template is created for creating RimWorld mods in [VSCodium/Visual Studio Code](https://vscodium.com/)

## Setup

1. Ensure the extension `ms-dotnettools.csharp` (a.k.a. [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)) is installed
1. Run the task `link mod directory`.
   - This should symlink the mod directory to RimWorld's mod folder.
   - The script won't work if you have installed RimWorld anywhere else than the default steam install.
1. Run the task `build mod` to compile assemblies and About.xml
1. Run the task `run game` to launch the game
   - The script won't work if you have installed RimWorld anywhere else than the default steam install.

## Additional notes

- All attributes for `About.xml` are kept in `.vscode/mod.csproj`, `About.xml` will get overriden each build.
- You can run `nix fmt` to format all code in the repository.
- While this template is designed for VSCodium, it works just as well with any other editor, simply launch the scripts manually
