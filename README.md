[logo]: https://raw.githubusercontent.com/Geeksltd/Zebble.Sensors/master/Shared/NuGet/Icon.png "Zebble.Sensors"


## Zebble.Sensors

![logo]

A Zebble plugin to use device sensors in Zebble applications.


[![NuGet](https://img.shields.io/nuget/v/Zebble.Sensors.svg?label=NuGet)](https://www.nuget.org/packages/Zebble.Sensors/)

> This plugin make you able to access to the device sensors such as Accelerometer, Gyroscope, etc. on Android, IOS, and UWP platforms.

<br>


### Setup
* Available on NuGet: [https://www.nuget.org/packages/Zebble.Sensors/](https://www.nuget.org/packages/Zebble.Sensors/)
* Install in your platform client projects.
* Available for iOS, Android and UWP.
<br>


### Api Usage

Call `Zebble.Device.Sensors` from any project to gain access to APIs.

##### Compass (Smooth compass):
The built-in compass sensor returns data that is a bit shaky. It randomly changes in a 1-2 degree span. This causes problems if you need a smooth value for UI work (for example for Augmented Reality).

To solve this problem there is a Zebble plug-in named SmoothCompass. Instead of just the magnetic reading value of compass, it will use a combination of compass value, gyroscope and accelerometer to deliver a smooth and more natural result.
###### How to use it
1.In your UI module, create an instance of Smooth Compass.
<br>
2.In the constructor you can define the frequency of changes needed. The default is "Game" which means every 20ms, or 50 changes per second.
<br>
3.Handle its Changed event, which gives you the current compass reading (degree from North).
Dispose it in your module's Dispose event.
```csharp
Zebble.Device.Sensors.Compass.Changed.Handle(d => OnValueChanged(d));
            
// Parameter used to determine update frequency:
await Zebble.Device.Sensors.Compass.Start(SensorDelay.PURPOSE);
```
```csharp
Task OnValueChanged(double value)
{
    //use the value as you need.
}
```
<br>

##### Accelerometer (device angle):
Accelerometer gives you access to the angle of the device, relative to the earth core.
The following shows how you can use that in Zebble:
```csharp
Zebble.Device.Sensors.Accelerometer.Changed.Handle(ev => OnValueChanged(ev));
   
// Parameter used to determine update frequency:
await Zebble.Device.Sensors.Accelerometer.Start(SensorDelay.PURPOSE); 
```
<br>

##### Gyroscope (device motion speed):
Gyroscope gives you access to the realtime motion or rotation of the device in different directions.
The following shows how you can use that in Zebble:
```csharp
Zebble.Device.Sensors.Gyroscope.Changed.Handle(mv => OnValueChanged(mv));

Zebble.Device.Sensors..Gyroscope.Start(SensorDelay.PURPOSE);
```
```csharp
void OnValueChanged(MotionVector value)
{
    // Now you can use value.X, value.Y and value.Z
}
```
<br>

##### SensorDelay.PURPOSE:

The `Start()` method takes a parameter of type `SensorDelay`, which is an enum with the following options:

* Realtime -> As fast as supported by the device hardware
* Game: Every 16ms -> Suitable for smooth animations (60 FPS)
* UI: Every 60ms -> Suitable for changing UI elements other than animations
* Background: Every 200ms -> Suitable for non-UI processing, or when

##### Handling device shake event:
If you want your app to respond to the user shaking the phone, then you need to follow these instructions:

1.In Config.XML, enable shaking detection:
```xml
<Device.System.DetectShaking>true</Device.System.DetectShaking>
```
2.On the Zebble page or module that should respond to Shaking, add an event handler in the OnInitializing event:
```csharp
public override async Task OnInitializing()
{
    await base.OnInitializing();
    // ....
    Zebble.Device.Sensors.Accelerometer.DeviceShaken.Handle(OnDeviceShaken);        
}
```
3.Add the event handler:
```csharp
Task OnDeviceShaken()
{
    return Alert.Show("TADA! You device was shaken!");
}
```
4.Remove the handler when your page or module is disposed:
```csharp
public override void Dispose()
{
    Zebble.Device.Sensors.Accelerometer.DeviceShaken.RemoveHandler(OnDeviceShaken);
    base.Dispose();
}
```
<br>


### Properties
| Property     | Type         | Android | iOS | Windows |
| :----------- | :----------- | :------ | :-- | :------ |
| DEFAULT_DELAY           | SenrorDelay          | x       | x   | x       |
| IsActive           | bool          | x       | x   | x       |

<br>


### Events
| Event             | Type                                          | Android | iOS | Windows |
| :-----------      | :-----------                                  | :------ | :-- | :------ |
| Changed            | AsyncEvent<TValue&gt;    | x       | x   | x       |


<br>


### Methods
| Method       | Return Type  | Parameters                          | Android | iOS | Windows |
| :----------- | :----------- | :-----------                        | :------ | :-- | :------ |
| Start         | Task| delay -> SenrorDelay<br> errorAction -> OnError| x       | x   | x       |
| Stop         | void| - | x       | x   | x       |
| IsAvailable         | bool| -| x       | x   | x       |
