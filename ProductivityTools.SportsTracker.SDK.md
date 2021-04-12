<a name='assembly'></a>
# ProductivityTools.SportsTracker.SDK

## Contents

- [SportsTracker](#T-ProductivityTools-SportsTracker-SDK-SportsTracker 'ProductivityTools.SportsTracker.SDK.SportsTracker')
  - [#ctor(username,password,logging)](#M-ProductivityTools-SportsTracker-SDK-SportsTracker-#ctor-System-String,System-String,System-Boolean- 'ProductivityTools.SportsTracker.SDK.SportsTracker.#ctor(System.String,System.String,System.Boolean)')
- [Training](#T-ProductivityTools-SportsTracker-SDK-Model-Training 'ProductivityTools.SportsTracker.SDK.Model.Training')
  - [AverageSpeed](#P-ProductivityTools-SportsTracker-SDK-Model-Training-AverageSpeed 'ProductivityTools.SportsTracker.SDK.Model.Training.AverageSpeed')
  - [Description](#P-ProductivityTools-SportsTracker-SDK-Model-Training-Description 'ProductivityTools.SportsTracker.SDK.Model.Training.Description')
  - [Distance](#P-ProductivityTools-SportsTracker-SDK-Model-Training-Distance 'ProductivityTools.SportsTracker.SDK.Model.Training.Distance')
  - [Duration](#P-ProductivityTools-SportsTracker-SDK-Model-Training-Duration 'ProductivityTools.SportsTracker.SDK.Model.Training.Duration')
  - [EnergyConsumption](#P-ProductivityTools-SportsTracker-SDK-Model-Training-EnergyConsumption 'ProductivityTools.SportsTracker.SDK.Model.Training.EnergyConsumption')
  - [SharingFlags](#P-ProductivityTools-SportsTracker-SDK-Model-Training-SharingFlags 'ProductivityTools.SportsTracker.SDK.Model.Training.SharingFlags')
  - [StartDate](#P-ProductivityTools-SportsTracker-SDK-Model-Training-StartDate 'ProductivityTools.SportsTracker.SDK.Model.Training.StartDate')
  - [StartTime](#P-ProductivityTools-SportsTracker-SDK-Model-Training-StartTime 'ProductivityTools.SportsTracker.SDK.Model.Training.StartTime')
  - [TotalDistance](#P-ProductivityTools-SportsTracker-SDK-Model-Training-TotalDistance 'ProductivityTools.SportsTracker.SDK.Model.Training.TotalDistance')
  - [TotalTime](#P-ProductivityTools-SportsTracker-SDK-Model-Training-TotalTime 'ProductivityTools.SportsTracker.SDK.Model.Training.TotalTime')
  - [TrainingType](#P-ProductivityTools-SportsTracker-SDK-Model-Training-TrainingType 'ProductivityTools.SportsTracker.SDK.Model.Training.TrainingType')
  - [WorkoutKey](#P-ProductivityTools-SportsTracker-SDK-Model-Training-WorkoutKey 'ProductivityTools.SportsTracker.SDK.Model.Training.WorkoutKey')

<a name='T-ProductivityTools-SportsTracker-SDK-SportsTracker'></a>
## SportsTracker `type`

##### Namespace

ProductivityTools.SportsTracker.SDK

<a name='M-ProductivityTools-SportsTracker-SDK-SportsTracker-#ctor-System-String,System-String,System-Boolean-'></a>
### #ctor(username,password,logging) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| username | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | User name to the Sports-Tracker page |
| password | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Password to the Sports-Tracker page |
| logging | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | When set to true it will print all webrequest content to the console |

<a name='T-ProductivityTools-SportsTracker-SDK-Model-Training'></a>
## Training `type`

##### Namespace

ProductivityTools.SportsTracker.SDK.Model

<a name='P-ProductivityTools-SportsTracker-SDK-Model-Training-AverageSpeed'></a>
### AverageSpeed `property`

##### Summary

Average speed in km/h

<a name='P-ProductivityTools-SportsTracker-SDK-Model-Training-Description'></a>
### Description `property`

##### Summary

Description

<a name='P-ProductivityTools-SportsTracker-SDK-Model-Training-Distance'></a>
### Distance `property`

##### Summary

Distance of the training in kilometers

<a name='P-ProductivityTools-SportsTracker-SDK-Model-Training-Duration'></a>
### Duration `property`

##### Summary

Duration in TimeSpan

<a name='P-ProductivityTools-SportsTracker-SDK-Model-Training-EnergyConsumption'></a>
### EnergyConsumption `property`

##### Summary

Calories

<a name='P-ProductivityTools-SportsTracker-SDK-Model-Training-SharingFlags'></a>
### SharingFlags `property`

##### Summary

Sharing flags, 19 is public

<a name='P-ProductivityTools-SportsTracker-SDK-Model-Training-StartDate'></a>
### StartDate `property`

##### Summary

Date and time of the training

<a name='P-ProductivityTools-SportsTracker-SDK-Model-Training-StartTime'></a>
### StartTime `property`

##### Summary

Readonly property which returnes start time in epoch time

<a name='P-ProductivityTools-SportsTracker-SDK-Model-Training-TotalDistance'></a>
### TotalDistance `property`

##### Summary

Read only property which returns distance in meters

<a name='P-ProductivityTools-SportsTracker-SDK-Model-Training-TotalTime'></a>
### TotalTime `property`

##### Summary

Read only property which gives duration time in seconds

<a name='P-ProductivityTools-SportsTracker-SDK-Model-Training-TrainingType'></a>
### TrainingType `property`

##### Summary

Training Type

<a name='P-ProductivityTools-SportsTracker-SDK-Model-Training-WorkoutKey'></a>
### WorkoutKey `property`

##### Summary

SportsTracker Training Key
