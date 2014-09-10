Rapid Server
============

![rapid-server](http://files.glassocean.net/github/rapid-server.jpg)

A very high performance web server utilizing .NET sockets and async I/O comparable to Node.js + Express and IIS 7.5.

Currently outperforms Node.js in Windows 7 and competes with IIS 7.5. Handles the maximum concurrency allowed by ApacheBench (ab -n 100000 -c 20000) without any failures. Destroys the [C10K problem](http://en.wikipedia.org/wiki/C10k_problem). Handles concurrent (keep-alive) connections up to the maximum port limit imposed by the Windows OS and TCP/IP stack (usually around 64K after subtracting ports used/reserved by the OS).

Screenshots, benchmarks and code to be released shortly.

Features
--------
Current working features include:

* Event-driven, async I/O similar to Node.js (libuv) and IIS 7.5 for high performance and concurrency.
* Uses kernel-level I/O completion ports for async callback completions, and managed threadpool threads for async processing.
* Decent error handling and low failure rates (zero so far) during high congestion/concurrency.
* Output caching - frequently used resources are stored and served from an in-memory cache, greatly reducing number of I/O calls.
* Server is configurable with an XML file, allowing plain-text configuration of mimetypes, default documents, keep-alive (max requests/timeout), custom handlers aka interops (php, etc), compression (gzip/deflate), response headers, virtual hosts, etc.

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
* Clustering - use a round-robin/random/intelligent point of access pass-thru system that redirects new clients/requests to low activity worker processes/servers in the cluster that are on different IP addresses, working around the 64K port limit imposed by the OS and TCP/IP stack, enabling N * 64K concurrency where N is the number of processes/servers in the cluster that are running on separate IP addresses. This should allow C∞K, limited only by the distributed hardware available.

History
-------
Based on research and concepts from my [dotnetsockets](https://github.com/perrybutler/dotnetsockets) project, this is the next evolution of it.
