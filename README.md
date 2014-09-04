Rapid Server
============

A very high performance web server utilizing .NET sockets and async I/O comparable to Node.js + Express.

Based on research and concepts from my [dotnetsockets](https://github.com/perrybutler/dotnetsockets) project, this is the next evolution of it.

Currently outperforms Node.js in Windows 7 and competes with IIS 7.5. Handles the maximum concurrency allowed by ApacheBench (ab -n 100000 -c 20000) without any failures - destroys the C10K problem.

Screenshots, benchmarks and code to be released shortly.
