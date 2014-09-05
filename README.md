Rapid Server
============

A very high performance web server utilizing .NET sockets and async I/O comparable to Node.js + Express.

Based on research and concepts from my [dotnetsockets](https://github.com/perrybutler/dotnetsockets) project, this is the next evolution of it.

Currently outperforms Node.js in Windows 7 and competes with IIS 7.5. Handles the maximum concurrency allowed by ApacheBench (ab -n 100000 -c 20000) without any failures - destroys the C10K problem.

Screenshots, benchmarks and code to be released shortly.

Features
--------
Current working features include:

* Evented, async I/O similar to Node.js (libuv) for high performance and concurrency.
* Decent error handling and low failure rates (zero so far!) during high congestion/concurrency.
* Output caching - caches popular resources and serves them up from an in-memory cache, greatly reducing number of I/O calls.
* Server is configurable with an XML file similar to Apache. Allows plain-text configuration of MimeTypes, DefaultDocuments, Keep-Alive (max requests/timeout), Custom Handlers aka Interops (PHP, etc), Compression (gzip/deflate), Response Headers, Virtual Hosts, etc.

Roadmap
-------
Future milestones include:

* Implement more of the official HTTP spec - headers, mimetypes, etc.
* Virtual hosts and directory security.
* Certificates, signing and encryption (SSL/HTTPS).
* PHP handler via CGI and FastCGI.
* Modularize the components, making them optional - build a server with only the stuff you need.
* Re-implement the Binary RPC server type. Should work similar to Apache Thrift (Facebook) or Protobuf (Google).
* Implement new server types - chat server, game server, etc. Also implement their client classes.
* Improved error handling, logging and reporting.
* More benchmarks and challenges. Can it outperform other web servers at most tasks? Can it handle C100K, C1000K?
