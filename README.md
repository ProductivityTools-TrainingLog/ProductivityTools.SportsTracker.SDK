<!--Category:Powershell--> 
 <p align="right">
    <a href="https://www.nuget.org/packages/ProductivityTools.SportsTracker.SDK/"><img src="Images/Header/Nuget_border_40px.png" /></a>
    <a href="http://productivitytools.tech/get-datename/"><img src="Images/Header/ProductivityTools_green_40px_2.png" /><a> 
    <a href="https://github.com/ProductivityTools-TrainingLog/ProductivityTools.SportsTracker.SDK"><img src="Images/Header/Github_border_40px.png" /></a>
</p>
<p align="center">
    <a href="http://productivitytools.tech/">
        <img src="Images/Header/LogoTitle_green_500px.png" />
    </a>
</p>

# Sports Tracker SDK
 
Library exposes methods which allow to manage trainings on the https://sports-tracker.com/ website.
<!--more-->

 ```C#
Methods
- AddTraining(Training training)
- AddTraining(Training training, byte[] gpxFile)
- AddTraining(Training training, List<byte[]> image)
- AddTraining(Training training, byte[] gpxFile, List<byte[]> image)
- AddTraining(TrainingType trainingType, string description, int duration, DateTime startTime)
- ImportGpxFile(byte[] content)
- DeleteTraining(string workoutKey)

```

To use SportsTracker you need to create it with

```c#
var sportsTracker = new SportsTracker(this.Config["login"], this.Config["password"]);
```

 
 <!--og-image-->
 ![Example](Images/Test.png)
 .
