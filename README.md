Rapid Server
============

![rapid-server](http://files.glassocean.net/github/rapid-server.jpg)

A very high performance web server utilizing .NET sockets and async I/O comparable to Node.js + Express and IIS 7.5.

Currently outperforms Node.js and nginx in Windows 7; competes with IIS 7.5. Handles the maximum concurrency allowed by ApacheBench (ab -n 100000 -c 20000) without any failures. Destroys the [C10K problem](http://en.wikipedia.org/wiki/C10k_problem).

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

Quick Start
-----------
The following code will create a standard HTTP server that is ready to serve static files from a directory:

    Dim WithEvents server as Rapid.Http.Server("c:\myweb1")
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

Benchmarks
----------
Out of box, Rapid.Http.Type1.Server currently outperforms Node.js by 533% (4180 RPS vs. 660 RPS) in a Windows 7 environment. As I'm still new to using Node.js myself, this is exciting news but nothing to write home about yet. I'll need to make sure Node.js is configured with the recommended web server optimizations before considering this a conclusive result.

The Node.js test environment is setup as follows:

    var express = require("express");
    var app = express();
    app.use(express.static(__dirname + "/../test-static/"));
    var server = app.listen(9888);

I was curious about nginx performance so I got that installed. Then I ran the same benchmarks twice against all of the web servers in my test environment, which are configured to serve up the same "hello world" html page. Here are the results, sorted from best to worst.
    
ab -n 10000 -c 100:

| Server | RPS (1st run) | RPS (2nd run) |
|--------|---------------|---------------|
|rapid-server|4784|4648|
|iis 7.5|4763|4672|
|nginx|2819|2945|
|node.js|1185|1235|
|apache|843|819|

ab -n 10000 -c 1000:

| Server | RPS (1st run) | RPS (2nd run) |
|--------|---------------|---------------|
|iis 7.5|4104|4165|
|rapid-server|4072|4044|
|nginx|820|859|
|node.js|663|645|
|apache|fail|fail|

ab -n 100000 -c 20000:

| Server | RPS (1st run) | RPS (2nd run) |
|--------|---------------|---------------|
|iis 7.5|1334|1321|
|rapid-server|1322|1281|
|nginx|1035|1042|
|node.js|657|649|
|apache|fail|fail|

Holy crud! Stay tuned...

Roadmap
-------
Future milestones include:

* Implement more of the official HTTP spec - more headers, mimetypes, etc.
* Replace IAsyncResult with SocketAsyncEventArgs to prevent high volume object allocations and improve async performance.
* Implement an HTTP request cache similar to the output cache. This would basically avoid having to parse the request string into an HTTP request object and avoid the need to create the HTTP response object for every request that hits the server, when those requests have already been served before.
* Virtual hosts and directory/file security.
* Certificates, signing and encryption (SSL/HTTPS).
* PHP handler via CGI and FastCGI.
* Modularize the components, making them optional - build a light-weight server with only the stuff you need.
* Re-implement the Binary RPC server type. Should work similar to Apache Thrift (Facebook), Protobuf (Google) or Cap'n Proto.
* Implement new server types - chat server, game server, etc. Also implement their client classes.
* Improved error handling, logging and reporting.
* Implement and/or compare the three async methods provided by .NET via benchmarks (IAsyncResult/.NET 2.0-3.0, SocketAsyncEventArgs/.NET 3.5, Task Parallelism/.NET 4.0-4.5).
* More benchmarks and challenges. Can it outperform other web servers at most tasks? Can it handle C100K, C1000K?
* Clustering - use a round-robin/random/intelligent point of access pass-thru system that redirects new clients/requests to low activity worker processes/servers in the cluster that are on different IP addresses.
* Client clustering - Work around the 64K port limit imposed by the OS and TCP/IP stack, enabling N * 64K concurrency where N is the number of processes/servers in the cluster that are running on separate IP addresses. This should allow C∞K, limited only by the distributed hardware available.

History
-------
Based on research and concepts from my [.NET Sockets](https://github.com/perrybutler/dotnetsockets) project, this is the next evolution of it.
