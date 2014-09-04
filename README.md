Rapid Server
============

A very high performance web server utilizing .NET sockets and async I/O comparable to Node.js + Express.

Based on research and concepts from my [dotnetsockets](https://github.com/perrybutler/dotnetsockets) project, this is the next evolution of it.

Currently outperforms Node.js in Windows 7 and competes with IIS 7.5. Handles 15,000 concurrent connections without any failures (ab -n 30000 -c 15000) - destroys the C10K problem.

Screenshots, benchmarks and code to be released shortly.
