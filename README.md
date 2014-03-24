# VimeoApi .NET Library #

Full .NET wrapper for the Vimeo New Api version 3 https://developer.vimeo.com/api

## Current Version and Status ##

Current version is 0.1. Status is "beta".

## Unit Tests ##

Using VimeoApi.Tests is simple. Just follow these steps:

- If you have not got a Vimeo app yet, register your app (https://developer.vimeo.com/apps)
- Setup the required config in App.config and Web.config
- Select the Authentication Method for your Api:

```c#
[TestInitialize]
public void Init()
{
	_httpSimulator = AppStart.SetDependencyResolver();
	_videosApi = AppStart.InitVimeoClient<AuthenticatedViaRedirectVimeoClient>().Videos();
}
```

- If you want to use AuthenticatedViaRedirectVimeoClient, you have to generate your own Access token:
	- Run VimeoApi.Web and navigate to /Auth/Display
	- Once you are redirected to vimeo, Login with your vimeo account and grant the required permissions.
	- You will be redirected back to /Auth/Display. Now copy your Access token
	- On the VimeoApi.Tests.Setup.AppStart.InitVimeoClient method update the Access token with your own.


## Dependencies ##

- OAuth2 (https://github.com/titarenko/OAuth2)
- RestSharp (http://restsharp.org/)
- Newtonsoft.Json (http://json.codeplex.com/)
- Simple Ajax Uploader (https://github.com/LPology/Simple-Ajax-Uploader)

## Features ##

- Authentication Methods:
	- Unauthenticated Requests
	- Authenticated Requests (Pre-grenerated Access Token)
	- Authenticated Via Redirect Requests
- Endpoints:
	- Users
	- Videos
- Simple Upload
- Streaming Upload
- Unit Tests

## License ##

The MIT License

Copyright (c) 2014 Ahmed El-Kilani

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
