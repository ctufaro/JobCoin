JobCoin (Web + Mixer)
===============

Mixer Implementation in C#
----------------------------

JobCoin is a Bitcoin Mixing/Tumbling implementation in C# on the [.NET Core](https://dotnet.github.io/) platform.  

[.NET Core](https://dotnet.github.io/) is an open source cross platform framework and enables the development of applications and services on Windows, macOS and Linux.  


The design
----------
* **JobCoin.WEB** - no frills, bootstrapped web front-end that calls the REST service.
* **RESTApi/SQL Backend** - earlier WebAPI project with a SQL backend hosted on Azure.
* **JobCoin.MIXR**  - the mixing console application.
* **JobCoin.MIXR.CL** - class library used by the mixing console application.

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
I leveraged an existing WebAPI/SQL project hosted on Azure. Here is the [link](https://github.com/ctufaro/UGoForAPI) to the project. The REST Api utilizes a SQL Database as a backend. Below is a screenshot of the Schema.

![alt text](https://raw.githubusercontent.com/ctufaro/jobcoin/master/JobCoin.WEB/images/screenshot2.jpg)

The database table is simple, store the user's forward addresses and the generated deposit addresses. One thing to note is the status field:

* Status 1 - the user has just visited the web page and has generated a deposit address.
* Status 2 - the user has deposited funds to the generated address, the house and commissions have been paid also.
* Status 3 - the mixer has distributed the funds to the user's forward addresses. At this point we are finished.

JobCoin.MIXR
-----------
Simple program.cs with a call to the poller in the Main method.

JobCoin.CL
-----------
* **Models.cs** - POCO (Plain Old C# Objects) used by the poller.
* **Utilities.cs** - Shuffle and Random Distribution Methods. See notes below on the Shuffling and Distribution.
* **Poller.cs**  - the poller runs within a loop, on every iteration of the loop we queue the work onto the ThreadPool using TPL. See notes below on Polling.

Polling
-----------
When the polling begins we asynchronously get a list of all deposited addresses (Status = 1) from the database via a REST call. We then poll the [JobCoin network](https://jobcoin.gemini.com/headstone/api) for all transactions. Once we retrieve these two collections, we use LINQ and run a query finding any jobcoins sent to the network. At this point we payout the 3% commission, and send the funds to the house account using the SendToHouseAsync method. We then flags these transactions with a Status equaling 2 in the database. Lastly we poll again for transactions with a Status of 2. We then Shuffle/Distribute the funds, see below.

Shuffling/Distribution
-----------
Transactions queried with a Status of 2 are shuffled using a simple generic Fisher-Yates shuffle. Link can be found [here.](https://www.dotnetperls.com/fisher-yates-shuffle) Once shuffled, we then query our database for the payout amounts and the destination addresses. To better thwart analyzers we do the following:

1. Take a single distribution amount and spread this amount over N random integers where the sum of N equals the payout amount. The java implementation can be found [here.](https://stackoverflow.com/questions/22380890/generate-n-random-numbers-whose-sum-is-m-and-all-numbers-should-be-greater-than)
2. We store the distributions from 1 as a string array. For amounts with decimals, we simply append the decimal amount to a randomly chosen element from the array returned in step 1.
3. Once we have our array, we then randomly choose a number from 1 - 10. This represents a sleep time for our thread. Once the thread awakens from this random sleep time, the distributions will then be made.
4. Since we queue all our address distributions on the ThreadPool, concurrency is not guaranteed. This will work in our favor against analyzers.


Vulnerabilities
-----------








