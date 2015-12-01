![rapid-server](http://files.glassocean.net/github/rapid-server.jpg)

**Status update November 30:** Rapid Server now incorporates a *fast path optimization* which aims to perform as few tasks and construct as few objects as possible while serving as many requests as possible under the right conditions.

![rapid-web-client](http://files.glassocean.net/github/nov30-rapidserver-fast-path.png)

By caching both requests and responses in memory, Rapid Server can skip nearly all of the processing for a client, cutting a full request/response cycle down to a few function calls mostly related to socket sends and receives. This is similar to Varnish or memcached, but built in. We're just using a ConcurrentDictionary as the key/value store for the request cache and response cache.

Rapid Server can also be used as a reverse proxy or load balancer similar to NGINX. This has already been tested with Rapid Server sitting in front of a group of servers (Apache, IIS, NGINX, Node.js, LighTPD), forwarding requests to a random server for handling, then passing responses back to the client.

Poorly performing string operations and other slow methods have been eliminated through extensive CPU profiling. Profiling now reveals the majority of time spent happens with RunMessageLoop and IOCP/Threadpool completions. Basically, the bottleneck is now the .NET Framework and Windows kernel. And there's not much we can do about it.

For the following CPU profile I ran ApacheBench against Rapid Server 20 times in a row:

![rapid-web-client](http://files.glassocean.net/github/nov30-rapidserver-fast-path-profile.png)

Compare this with the previous profile I did on November 23rd.

<hr>

Rapid Server
============

A very high performance web server utilizing .NET sockets and async I/O comparable to Node.js + Express and IIS 7.5.

Currently outperforms Node.js by up to 533% and nginx by up to 58% in Windows 7; competes with IIS 7.5. Handles the maximum concurrency allowed by ApacheBench (ab -n 100000 -c 20000) without any failures. Destroys the [C10K problem](http://en.wikipedia.org/wiki/C10k_problem).

**In this readme:** [Features](#features) - [Quick Start](#quick-start) - [Benchmarks](#benchmarks)- [Requirements](#requirements) - [FAQ](#faq) - [Roadmap](#roadmap) - [Status Updates](#status-updates) - [History](#history)

Screenshots, benchmarks and code to be released shortly.

![rapid-server-b](http://files.glassocean.net/github/rapid-server-b.jpg)

Features
--------
Current working features include:

* Event-driven, async I/O similar to Node.js (libuv) and IIS 7.5 for high performance and concurrency.
* Uses kernel-level I/O completion ports for async callback completions, and managed threadpool threads for async processing.
* Decent error handling and low failure rates (zero so far) during high congestion/concurrency.
* Output caching - frequently used resources are stored and served from an in-memory cache, greatly reducing number of I/O calls.
* Server is configurable with an XML file, allowing plain-text configuration of mimetypes, default documents, keep-alive (max requests/timeout), custom handlers aka interops (php, etc), compression (gzip/deflate), response headers, virtual hosts, etc.
* A client app is included for running benchmarks and observing the request/response cycle for any web server. Tested to work with IIS 7.5, Node.js, Apache, nginx and Rapid Server.

Quick Start
-----------

###For End Users

1. Download the stable release package and extract RapidServer.exe, RapidServer.dll and http.xml to a directory of your choice (e.g. "C:\RapidServer\").
2. Run (double-click) the RapidServer.exe file to start the web server listening on port 80 by default.
3. If you wish to make changes to the server configuration, edit the http.xml file and make sure to restart the server so the config gets reinitialized.

###For Developers

The following code will create a standard HTTP server that is ready to serve static files from a directory:

    Dim WithEvents server as Rapid.Http.Type1.Server("c:\myweb1")
    server.StartServer("127.0.0.1", 9999)

By overriding the HandleRequest event we can build a custom HTTP server:

    Private Sub server_HandleRequest(ByVal req As Rapid.Http.Request, ByVal res As Rapid.Http.Response, ByVal client As System.Net.Sockets.SocketAsyncEventArgs) Handles server.HandleRequest
      ' custom handling goes here e.g:
      If req.MimeType = "x-my-custom-mime-type" Then
        ' if our custom mime type is detected, modify the response
        res.StatusCode = 404
      End If
    End Sub

Or perhaps we want to allow our custom MimeType to be served as a static file with gzip compression. To do that we simply add the MimeType to the server config file:

    <MimeType FileExtension="myc" Compress="gzip" Expires="access plus 1 year">x-my-custom-mime-type</MimeType>

What about custom response headers? Just add them to the config file:

    <Header>Server: TheGiantHamster</Header>
    <Header>X-Powered-By: FerrisWheel</Header>

Benchmarks
----------
Out of box, Rapid.Http.Type1.Server currently outperforms Node.js in a Windows 7 environment. As I'm still new to using Node.js myself, this is exciting news but nothing to write home about yet. I'll need to make sure Node.js is configured with the recommended web server optimizations before considering this a conclusive result.

The Node.js test environment is setup as follows:

    var express = require("express");
    var app = express();
    app.use(express.static(__dirname + "/../test-static/"));
    var server = app.listen(9888);

I was curious about nginx performance so I got that installed. Then I ran the same benchmarks twice against all of the web servers in my test environment, which are configured to serve up the same "hello world" html page. Tests were performed using various benchmarking tools with keep-alive on or off for some variety. Tests were performed with typical consumer/commodity hardware - 2.8ghz Quad Core, 4gb RAM, 7200 rpm HDD, Windows 7 SP1.
    
Test 1 (ab -n 10000 -c 100):

| Server | RPS (1st run) | RPS (2nd run) |
|--------|---------------|---------------|
|rapid-server|4784|4648|
|iis 7.5|4763|4672|
|nginx|2819|2945|
|node.js|1185|1235|
|apache|843|819|

Test 2 (ab -n 10000 -c 1000):

| Server | RPS (1st run) | RPS (2nd run) |
|--------|---------------|---------------|
|iis 7.5|4104|4165|
|rapid-server|4072|4044|
|nginx|820|859|
|node.js|663|645|
|apache|fail|fail|

Test 3 (ab -n 100000 -c 20000):

| Server | RPS (1st run) | RPS (2nd run) |
|--------|---------------|---------------|
|iis 7.5|1334|1321|
|rapid-server|1322|1281|
|nginx|1035|1042|
|node.js|657|649|
|apache|fail|fail|

Test 4 (ab **-k** -n 10000 -c 1000):

| Server | RPS (1st run) | RPS (2nd run) |
|--------|---------------|---------------|
|iis 7.5|9148|9024|
|rapid-server|5125|4738|
|nginx|2456|2497|
|node.js|1108|1170|
|apache|fail|fail|

Test 5 (weighttp -n 10000 -c 100):

| Server | RPS (1st run) | RPS (2nd run) |
|--------|---------------|---------------|
|iis 7.5|3707|3646|
|rapid-server|3414|3489|
|nginx|2977|3013|
|node.js|1108|1170|
|apache|874|891|

Test 6 (weighttp **-k** -n 1000000 -c 100):

| Server | RPS (1st run) | RPS (2nd run) |
|--------|---------------|---------------|
|iis 7.5|8290|8105|
|rapid-server|7692|7669|
|nginx|3652|3759|
|node.js|1694|1648|
|apache|943|923|

Holy crud! Stay tuned...

Requirements
------------
Microsoft Windows, .NET Framework:

* Type1.Server requires .NET 2.0+ (implements EAP / APM / IAsyncResult / Socket.BeginXXX Socket.EndXXX)
* Type2.Server requires .NET 3.5+ (implements EAP / APM / SocketAsyncEventArgs / Shared Buffer / Socket.BeginXXX Socket.EndXXX)
* Type3.Server required .NET 4.0+ (implements TPL / Tasks / ContinueWith)

FAQ
---
*Is it production-ready?*
Not yet, I'm looking for beta testers and feedback. This server has never been used in the wild and the current version contains no security measures whatsoever. When the software hits version 1.0 it should be production-ready.

*Why not use the TcpClient, TcpListener, HttpWebRequest or HttpWebResponse classes already included in the .NET Framework?*
While these classes do abstract away a good portion of the code required to build a functional client or server app, I wanted something more flexible and low level along the lines of WinSock2 or Socket.IO so that I would essentially be learning how those work too. Furthermore, to make the TcpClient/TcpListener classes asynchronous, one needs to utilize the underlying Socket class, so why bother with the higher level abstractions? I also wanted to get a more thorough understanding of Sockets and the protocols we use everyday such as TCP, HTTP, etc.

*Why is it coded in VB.NET? Nobody cares about VB or Windows.*
Aside from being one of the most robust RAD frameworks out there, I enjoy coding in VB.NET and that's about all there is to it. The end result is a very "plain English" code base which makes the architecture extremely easy to understand, which should also mean that it's easy to port to your favorite language. Ask yourself this: if VB is so bad then why does the server perform so well? My only gripe about VB.NET happens to be that it's not a cross-platform language. I can read/write many languages, but that's not the point here. Maybe one day I'll port the code to Javascript or Dart, but until then...

*What about G-WAN?*
I'd like to compare with G-WAN, but they discontinued the Windows version. Their site states that Linux performance was far superior to Windows performance, so the Windows version was dropped, but the actual reasons are unclear. Also, the [tidbit](http://gwan.com/en_windows.html) about IIS using kernel-mode to "cheat" is merely an optimization feature (that is exactly what HTTP.sys does - handle requests in kernel-mode instead of user-mode), and the [claim](http://gwan.com/en_iis.html) about Windows kernel-mode drivers having the ability to crash the whole system is just short of fear mongering as the issue [does not apply](http://www.microsoft.com/technet/prodtechnol/WindowsServer2003/Library/IIS/a2a45c42-38bc-464c-a097-d7a202092a54.mspx) to IIS / HTTP.sys.

Roadmap
-------
Future milestones include:

* Create a web server manager that allows the admin to spin up instances of Rapid Server, Apache, node.js, NGINX, etc.
* Create a WAMP-like package.
* Implement a web-based management page and event log.
* Implement load balancer and reverse proxy modes. Try speeding up node.js in Windows by placing Rapid Server in front :)
* Implement URL rewriting to support .htaccess directives and be compatible with platforms such as Drupal.
* Implement more of the official HTTP spec - more headers, mimetypes, etc.
* Replace IAsyncResult with SocketAsyncEventArgs to prevent high volume object allocations and improve async performance.
* Implement an HTTP request cache similar to the output cache. This would basically avoid having to parse the request string into an HTTP request object and avoid the need to create the HTTP response object for every request that hits the server, when those requests have already been served before.
* Virtual hosts and directory/file security.
* Certificates, signing and encryption (SSL/HTTPS).
* PHP handler via FastCGI.
* Modularize the components, making them optional - build a light-weight server with only the stuff you need.
* Re-implement the Binary RPC server type. Should work similar to Apache Thrift (Facebook), Protobuf (Google) or Cap'n Proto.
* Implement new server types - chat server, game server, etc. Also implement their client classes.
* Improved error handling, logging and reporting.
* Implement and/or compare the three async methods provided by .NET via benchmarks (IAsyncResult/.NET 2.0-3.0, SocketAsyncEventArgs/.NET 3.5, Task Parallelism/.NET 4.0-4.5).
* More benchmarks and challenges. Can it outperform other web servers at most tasks? Can it handle C100K, C1000K?
* Clustering - use a round-robin/random/intelligent point of access pass-thru system that redirects new clients/requests to low activity worker processes/servers in the cluster that are on different IP addresses.
* Client clustering - Work around the 64K port limit imposed by the OS and TCP/IP stack, enabling N * 64K concurrency where N is the number of processes/servers in the cluster that are running on separate IP addresses. This should allow testing C∞K, limited only by the distributed hardware available.

Status Updates
--------------
**Status update November 23:** Decided to take a CPU profile with SlimTune and see what could be optimized.

![rapid-web-client](http://files.glassocean.net/github/nov2015-rapidserver-profile-file-exists.png)

This shows that one of the slowest operations during our entire handling of a request is when calling IO.File.Exists to check if a file exists on the hard drive. This is done to locate the default document. It's not a slow operation under normal conditions, but calling IO.File.Exists several thousand times per second while serving that many requests consumes more time than the rest of the code, and it adds up. Let's try it without IO.File.Exists:

![rapid-web-client](http://files.glassocean.net/github/nov2015-rapidserver-op-file-exists.PNG)

A noticeable improvement! That's an extra 500 to 2000 requests per second without IO.File.Exists. This brings Rapid Server very close to IIS 7.5 in terms of performance. In reality, we cannot eliminate IO.File.Exists because it is used to locate the default document, but we certainly don't need to do this on every request. The result can be cached, or it can be skipped when we already have a cached response, which is probably how this optimization will be finalized. In case you're wondering what the "S" means in the legend, it's short for static, these benchmarks were done against a static web page.

**Status update November 22:** Further demonstrating the Benchmark feature in Rapid Web Client. Similar graphs will be used for displaying server performance. Let's have a look at how Rapid Server, Apache, NGINX, IIS and node.js compare using ApacheBench. These servers are event-driven + asynchronous I/O, with the exception of Apache which is synchronous.

![rapid-web-client](http://files.glassocean.net/github/nov2015-rapidserver-benchmark1.png)
![rapid-web-client](http://files.glassocean.net/github/nov2015-rapidserver-benchmark2.png)

**Addendum:** Forgot to test LightTPD so I did that and it got around 1200 RPS.

More benchmarks to come. I'll also test some denial-of-service tools (LOIC, torshammer, etc) to see how well these servers hold up under pressure from common attack vectors.

**Status update November 19:** The Benchmark feature is now working in the Rapid Web Client. This is useful for testing the performance of various web servers and their implementations, or comparing two or more servers against each other. Rapid Web Client offers a GUI for ApacheBench and reads the results from it for easy data visualization. In the future this may also include WeighTTP, Siege and gobench support.

![rapid-web-client](http://files.glassocean.net/github/rapid-web-client.png)

**Status update November 16:** WordPress and phpMyAdmin are working great. No support for chunked file uploads yet, so you can't upload any media to WordPress yet. That's probably the last major hurdle for WordPress. Since Rapid Server does not support .htaccess / url rewriting, Drupal's install wizard fails on the last step (Configure site) when it does a check (and fails with a 404) for pretty urls. Url rewriting has been added to the Roadmap. I'll make a new section in this readme for noting compatibility with various platforms, and I'm going to move the older status updates into their own section at the bottom of this readme.

**Status update November 12:** Cookies have been implemented. WordPress login now works! Had to set an HTTP_COOKIE environment variable to have the cookie passed to the php-cgi.exe process.

**Status update November 11:** Milestone reached. Rapid Server can now handle the automated WordPress setup wizard in its entirety. It can complete all steps and create the database. This took a lot of debugging, I went in circles trying to figure out what's wrong with the CGI handler. Debugging .NET code, CGI code, and PHP code, this finally led me to discover the CGI environment variables were reaching the PHP scripts with lowercase names, even though I was setting them with uppercase names, so WordPress couldn't see a $_SERVER["SCRIPT_FILENAME"] because it was being sent as $_SERVER["script_filename"], which caused some paths to be correct and others to be incorrect (confusing). This turned out to be a bug in .NET 3.5 and upgrading the project to .NET 4.0 finally fixed it. It also made Piwik work. Now I need to implement cookie support, because WordPress requires cookies for logging in the user.

**Status update October 21:** Rapid Server is tested against real world scenarios during development. There are still a few kinks to iron out with PHP, redirects, and other RFC quirks, but for the most part this server is quite capable of hosting websites! Nothing close to production ready yet, but here's some progess:

* Wordpress: setup wizard works to create a site, login works, can make new pages/posts. Nearly 100% functional.
* Jekyll: generated static sites work, last time I checked.
* Static HTML: same as Jykell, it works. Perfectly handles a corporate website that uses a single-page app design (pure ajax), YouTube videos, Mandrill forms, etc.
* Grav: first page works, haven't tried any further.
* Drupal: first page works, haven't tried any further.
* phpMyAdmin: first page works, can login with config auth but not cookie auth (working on it!). Can also browse databases/tables.
* Pligg: first page works, haven't tried any further.
* Piwik: first page works.
* Joomla: first page works, haven't tried any further.
* Runs the latest version of PHP, MySQL, and the website platforms listed above. Should support older versions too, haven't tested this yet.
* Lightweight? Rapid Server library (dll) is 54 KB compiled; client/server apps (exe) are 48 KB compiled. Both have only 1 dependency - the .NET framework.

Currently top performer in the Paessler Webserver Stress Tool. Beats out IIS 7.5, Apache, NGINX, LightTPD in ramp tests with max users (4000). This was for static files, and more testing needs to be done.

History
-------
Based on research and concepts from my [.NET Sockets](https://github.com/perrybutler/dotnetsockets) project, this is the next evolution of it.
