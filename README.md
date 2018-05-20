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

Starting the poller is easy
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

JobCoin.WEB
-----------
[Here](http://ugoforstatic.azurewebsites.net/jobcoin.html) is a self-hosted link to the Web Front-End. The form is simple, fill is one or many multiple addresses you would like your JobCoins to be sent to.

![alt text](https://raw.githubusercontent.com/ctufaro/jobcoin/master/JobCoin.WEB/images/screenshot.jpg)

Once the user submits the addresses, they are prompted with the site's Service Terms. After reviewing them, and clicking 'Agree', the user is presented with a deposit address. The deposit address is actually a random generated name, retrieved from the project's RESTApi layer. Since most of the addresses used in the testing API were actual names (Alice, Bob), I thought using a name generator would thwart prying eyes trying to run analysis on the blockchain. 

RESTApi/SQL Backend
-----------
I leveraged an existing WebAPI/SQL project hosted on Azure. Here is the [link](https://github.com/ctufaro/UGoForAPI) to the project. The REST Api utilizes a SQL Datbase as a backend. Below is a screenshot of the Schema.

![alt text](https://raw.githubusercontent.com/ctufaro/jobcoin/master/JobCoin.WEB/images/screenshot2.jpg)

The database table is simple, store the user's forward addresses and the generated deposit addresses. Once thing to note is the status field.

* Status 1 - the user has just visited the web page and has generated a deposit address.
* Status 2 - the user has deposited funds to the generated address, the house and commissions have been paid also.
* Status 3 - the mixer has distributed the funds to the user's forward addresses. At this point we are finished.

There is a lot to do and we welcome contributers developers and testers who want to get some Blockchain experience.
You can find tasks at the issues/projects or visit our [C# dev](https://stratisplatform.slack.com/messages/csharp_development/) slack channel.

Testing
-------
* [Testing Guidelines](Documentation/testing-guidelines.md)

Notes
-----------





