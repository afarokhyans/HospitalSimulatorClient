# Hospital Simulator Client #

This is a simple standalone Windows WPF/MVVM application developed in Visual Studio 2017 which uses .NET framework 4.6.1.
This application includes three separate tab to perform the required functionality of the web service.
Patient registration tab allows user to registerd a patient and select a condition. Get Patients tab will get the list of registered patients from the web service and display them in the grid.
Get Consultation tab will get the list of all the consultation from the service. In order to use this application, the web service should be running first. 
For ease of use, this application is using the same Models that are being used in the Web Service. 


## How To Build ##

This project is developed in Visual Studio 2017 and requires .NET Framework 4.6.1. Once the project is download from GitHub, open the solution HospitalSimulatorClient.sln
Build the solution. The following NuGet packages have been used in this project:
- Microsoft.AspNet.WebApi
- MVVMLightLibs (MVVM framework Libraries)
- Newtonsoft.json

After a successful build, running HospitalSimulatorClient.exe will load the windows application which user can use for registering patients with the server.
The application will listen to URL http://localhost:5000/ which is set in the resources file.


## Developer ##

Alex Farokhyans