JobCoin (Web + Mixer)
===============

Mixer Implementation in C#
----------------------------

JobCoin is a Bitcoin Mixing/Tumbling implementation in C# on the [.NET Core](https://dotnet.github.io/) platform.  

[.NET Core](https://dotnet.github.io/) is an open source cross platform framework and enables the development of applications and services on Windows, macOS and Linux.  


The design
----------
* **JobCoin.WEB** - xxx
* **JobCoin.MIXR**  - xxx
* **JobCoin.MIXR.CL** - xxx
* **RESTApi** - xxx

Create a Blockchain in a .NET Core style programming
```
  var poller = new Poller(production);
  poller.Start(commissionAccount, houseAccount);
```

Compiling the Project
------------------

Make sure you have .NET Core 2.0 Installed

```
git clone https://github.com/ctufaro/JobCoin  
cd JobCoin

dotnet restore
dotnet build

```

To run: ``` dotnet run ```  

Getting Started Guide
-----------
[Here](http://ugoforstatic.azurewebsites.net/jobcoin.html) is a self-hosted link to the Web Front-End. The form is simple, fill is one or many multiple addresses you would like your JobCoins to be sent to. 

Development
-----------
Up for some blockchain development?

Check this guides for more info:
* [Contributing Guide](Documentation/contributing.md)
* [Coding Style](Documentation/coding-style.md)
* [Wiki Page](https://stratisplatform.atlassian.net/wiki/spaces/WIKI/overview)

There is a lot to do and we welcome contributers developers and testers who want to get some Blockchain experience.
You can find tasks at the issues/projects or visit our [C# dev](https://stratisplatform.slack.com/messages/csharp_development/) slack channel.

Testing
-------
* [Testing Guidelines](Documentation/testing-guidelines.md)

Notes
-----------





