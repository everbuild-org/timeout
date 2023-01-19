# 🕒 Timeout

_A simple power-saving tool_

## 📚 Table of Contents

- [📦 Installation](#-installation)
    - [📫 Pre-compiled](#-pre-compiled)
    - [🛠️ Compile it yourself](#️-compile-it-yourself)
- [🔧 Usage](#-usage)
- [🧭 Support](#-support)
- [📝 License](#-license)
- [🧑‍🦱 Credits](#-credits)
- [📜 Changelog](#-changelog)

## 📦 Installation

This tool only supports the windows operating system since it uses windows
services to operate. To install this tool, first choose if you want to
install it pre-compiled or compile it yourself.

### 📫 Pre-compiled

To install the pre-compiled version, download the latest release from the
[releases page](https://github.com/stellarverse/timeout/releases) and run
the installer. Please ntoe that the installer is not signed, so you might
get a warning from windows. 

For network-wide installations, you can create a new group policy to deploy
the installer to all computers in the network.

### 🛠️ Compile it yourself

Like mentioned above, this tool only supports the windows operating system. 
Aditionally, you need to have the [.NET 4.8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) or later installed.

To compile the tool, clone the repository and open the solution in Visual
Studio. Then, just build the solution and you're done.

## 🔧 Usage

Once installed, the tool will automatically start and run in the background 
on startup without any user interaction. To change the settings, you can
use the windows registry editor to change the following values:

- `HKEY_LOCAL_MACHINE\SOFTWARE\Stellarverse\Timeout\TimerInterval` - The
    interval in seconds between each check if the computer should be put to
    sleep. The default value is 300 seconds (5 minutes).


- `HKEY_LOCAL_MACHINE\SOFTWARE\Stellarverse\Timeout\LockedCycles` - The
    amount of timer cycles the computer has to be locked before it is put to sleep. The default value is 3 cycles (15 minutes assuming the timer interval is 5 minutes).

- `HKEY_LOCAL_MACHINE\SOFTWARE\Stellarverse\Timeout\ShutdownNotBeforeHours`
    This time indicates the earliest time the computer can be shut down. 
    This is useful if you want to prevent the computer from shutting down
    automatically during the day. The default value is 0 hours (midnight, 
    effectively disabling this feature).

- `HKEY_LOCAL_MACHINE\SOFTWARE\Stellarverse\Timeout\ShutdownNotBeforeMinutes`
    This time indicates the earliest time the computer can be shut down. 
    This is useful if you want to prevent the computer from shutting down
    automatically during the day. The default value is 0 minutes (midnight, 
    effectively disabling this feature).

- `HKEY_LOCAL_MACHINE\SOFTWARE\Stellarverse\Timeout\DryMode` - If this
    value is set to 1, the computer will not shut down automatically. This 
    is useful for testing purposes. The default value is 0 (disabled). In 
    this mode, the tool will still log the actions it would have taken.

The Log file can be found at `%temp%\TimeoutErrorLog.txt`.

## 🧭 Support

For commercial support, please visit [stellarverse.de/products/timeout](https://stellarverse.de/products/timeout). 

## 📝 License

This project is licensed under the [MIT License](LICENSE).

## 🧑‍🦱 Credits

This was made from and with [Goethe RobotX Jr](https://github.com/Goethe-RobotX-Jr)

## 📜 Changelog

- 1.0.0: Works