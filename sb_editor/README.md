[![License](https://img.shields.io/github/license/jmarti856/eurosound.svg)](https://www.gnu.org/licenses/gpl-3.0.html)
[![Issues](https://img.shields.io/github/issues/jmarti856/eurosound.svg)](https://github.com/jmarti856/eurosound/issues)
[![GitHub contributors](https://img.shields.io/github/contributors/jmarti856/eurosound.svg)](https://github.com/jmarti856/eurosound/contributors)
[![GitHub Release](https://img.shields.io/github/v/release/jmarti856/eurosound.svg)](https://github.com/jmarti856/eurosound/releases/latest)
[![GitHub Release-Date](https://img.shields.io/github/release-date/jmarti856/eurosound.svg)]()

# EuroSound Editor
When the authoring tools for the PC game Sphinx and the Cursed Mummy were published, it included the original level editor "EuroLand", but unlike this, the soundbanks and musicsbanks editor was not included because it couldn't be recovered.

This tool, wrote in C#, recreates the original tool that Eurocom developed for this purpose. Originally with this recreated tool, you could only be able to export for PC, but since the version 1.0.1.4 is also possible to export for PS2.

### Features
In the Sphinx wiki you can see more in detail each file documentation: https://sphinxandthecursedmummy.fandom.com/wiki/SFX here is a little resume:
- `Normal soundbanks:` Every level stores all of the used sound effects in its own sound bank, each sound effect has a series of flags and properties and contains a variable array of raw sound samples.
- `Stream File:` Most of the long, streamed ambient sounds are actually stored here once instead of being duplicated in each soundbank.
- `Music soundbanks`: Each music track is defined via hashcode and stored in its own file. Almost every track has a lead-in time, a middle looping section, and —occasionally— a small ending that is not generally used.
- `SFX_Data.bin: `Contains a special binary array, even if most of that data is redundant; but it probably exists to avoid having to load each soundbank just to get properties like the length of a sound.


### Download
If you just want to use it or looking for setup file, click here to download:

[![GitHub All Releases](https://img.shields.io/github/v/release/jmarti856/eurosound.svg?style=for-the-badge)](https://github.com/jmarti856/eurosound/releases/latest)
