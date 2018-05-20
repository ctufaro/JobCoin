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
  var node = new FullNodeBuilder()
   .UseNodeSettings(nodeSettings)
   .UseConsensus()
   .UseBlockStore()
   .UseMempool()
   .AddMining()
   .AddRPC()
   .Build();

  node.Run();
```

What's Next
----------

We plan to add many more features on top of the Stratis Bitcoin blockchain:
Sidechains, Private/Permissioned blockchain, Compiled Smart Contracts, NTumbleBit/Breeze wallet and more...

Running a FullNode
------------------

Our full node is currently in alpha.  

```
git clone https://github.com/stratisproject/StratisBitcoinFullNode.git  
cd StratisBitcoinFullNode\src

dotnet restore
dotnet build

```

To run on the Bitcoin network: ``` Stratis.BitcoinD\dotnet run ```  
To run on the Stratis network: ``` Stratis.StratisD\dotnet run ```  

Getting Started Guide
-----------
More details on getting started are available [here](https://github.com/stratisproject/StratisBitcoinFullNode/blob/master/Documentation/getting-started.md)

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

CI build
-----------

We use [AppVeyor](https://www.appveyor.com/) for our CI build and to create nuget packages.
Every time someone pushes to the master branch or create a pull request on it, a build is triggered and new nuget packages are created.

To skip a build, for example if you've made very minor changes, include the text **[skip ci]** or **[ci skip]** in your commits' comment (with the squared brackets).

If you want get the :sparkles: latest :sparkles: (and unstable :bomb:) version of the nuget packages here: 
* [Stratis.Bitcoin](https://ci.appveyor.com/api/projects/stratis/stratisbitcoinfullnode/artifacts/nuget/Stratis.Bitcoin.1.0.7-alpha.nupkg?job=Configuration%3A%20Release)



