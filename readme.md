# LawnMowers test

## Behaviour

* Lawn Mower will stop if it tries to get outside of the lawn and "Mower tried to cut rare plants" output will be printed for it
* I assume that Lawn Mower is removed from the Lawn after finishing its instructions, so a collision with others are not possible
* An empty line must be entered to mark the end of input

## Implementation

* Solution is written in C# 6
* It is a DNX Console App, it uses new "ASP.NET 5 like" execution environment (DNX) and project structure ([documentation](http://docs.asp.net/en/latest/dnx/console.html))
 * It should be cross plaform although I only tried running it on Windows
* xUnit with FluentAssertions used for testing
* The project suits very well to the functional programming style
 * Only immutable data structures are used
 * All functions are pure
 * Iterators (yield keyword) are used extensively
 * C# 6 features such as get-only properties help with achieving immutability
 * A functional language such as F# would be even more suitable for this kind of project 
* Please check the git commit history. Every commit introduces only one logical piece of code to make code review easier. Of course the development didn't take place in this order, but thanks to git history rewrite features we can always push nice commits

## How to run

### From Visual Studio 2015

Older versions of Visual Studio cannot be used as solution uses C# 6 and DNX.

1. Get VS 2015
1. Update VS dnvm (.NET Version Manager) by downloading and running AspNet5.ENU.RC1.exe file from this package https://www.microsoft.com/en-us/download/details.aspx?id=49959
1. Get `1.0.0-rc1-final` DNX version for both CoreCLR and old CLR by running the following commands in the command line
 *  `dnvm install 1.0.0-rc1-final -r coreclr`
 *  `dnvm install 1.0.0-rc1-final -r clr`
 * (it is possible that Visual Studio will do it automatically when opening the solution)
1. Open LawnMowers.sln
1. Run LawnMowers.Console project
1. Run tests by opening Test Explorer window and hitting Run All (there should be 71 tests discovered)

### Without Visual Studio

Check **Upgrading DNVM or running without Visual Studio** section in [official documentation](https://github.com/aspnet/home#upgrading-dnvm-or-running-without-visual-studio).

Step by step guide:

1. Install dnvm (.NET SDK Manager) by running the following command in powershell:
 * `&{$Branch='dev';$wc=New-Object System.Net.WebClient;$wc.Proxy=[System.Net.WebRequest]::DefaultWebProxy;$wc.Proxy.Credentials=[System.Net.CredentialCache]::DefaultNetworkCredentials;Invoke-Expression ($wc.DownloadString('https://raw.githubusercontent.com/aspnet/Home/dev/dnvminstall.ps1'))}`
 * (this step is not needed if VS 2015 is installed on the machine)
1. Get `1.0.0-rc1-final` DNX version for CoreCLR by running
 *  `dnvm install 1.0.0-rc1-final -r coreclr`
1. Navigate to src/LawnMowers.Console
1. Switch to CoreCLR version of the DNX by running `dnvm use 1.0.0-rc1-final -r coreclr` (only needed if there are some other versions installed.. can be checked by `dnvm list`)
1. Run `dnu restore` to get NuGet packages
1. Run `dnx run` to start the application
1. To run tests
 1. Navigate to test/LawnMowers.Console.Tests
 1. Run `dnu restore`
 1. Run `dnx test`
 1. To run domain tests navigate to test/LawnMowers.Domain.Tests and repeat

## Sample 1

Input:

```
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM

```

Output:

```
1 3 N
5 1 E
```

## Sample 2

Input:

```
5 5
1 2 N
LMLMLMLMMMMMMMMMMMMMMM
3 3 E
MMRMMRMRRM

```

Output:

```
Mower tried to cut rare plants
5 1 E
```
