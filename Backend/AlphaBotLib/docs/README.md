# AlphaBot2 Control Library for .NET 7

[![NuGet](https://img.shields.io/nuget/v/AlphaBotLib.HiQ.svg)](https://www.nuget.org/packages/AlphaBotLib.HiQ/)

## Overview

This .NET 7 class library provides convenient interfaces and abstractions for controlling various components of the AlphaBot2 robot. It simplifies the integration of AlphaBot2 components into your .NET applications.

## Components

### Buzzer

The `Buzzer` class enables the control of the onboard buzzer, allowing you to produce beep sounds easily.

### Camera

The `Camera` class provides functionality for streaming and capturing photos from the AlphaBot2's onboard camera.

### Light

The `Light` class facilitates the control of LED lights on the AlphaBot2. Note that this class relies on a 3rd party library, which must be separately installed on the Raspberry Pi. Additionally, running the program as a `sudo` user is required for the lights to function in this initial version.

### Motor

The `Motor` class allows you to control the motors of the AlphaBot2, providing high-level abstractions for motor operations.

### TR Sensor

The `TRSensor` class offers functionality to read data from the TR sensor on the AlphaBot2.

## Getting Started

To use this library in your .NET project, you can install it via NuGet. Open the NuGet Package Manager Console and run the following command:

```bash
dotnet add package AlphaBotLib.HiQ
```

## Usage

To get started with using the AlphaBot2 Control Library, follow these steps:

1. Install the NuGet package in your project.
2. Create an instance of the desired component class (e.g., `**Buzzer**`, `**Camera**`).
3. Utilize the provided methods and properties to control and interact with the AlphaBot2 components.

Feel free to explore the example application in the [WAYFS-AlphaBot repository](https://github.com/younus10s/WAYFS-AlphaBot) for a practical demonstration.

## License

This library is licensed under the [MIT License](https://mit-license.org/).