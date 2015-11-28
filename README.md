# SourceSchemaParser
[![Build status](https://ci.appveyor.com/api/projects/status/79d9k8aqjs61m6or?svg=true)](https://ci.appveyor.com/project/JustinSkiles/sourceschemaparser)
[![NuGet](https://img.shields.io/nuget/v/SourceSchemaParser.svg)](https://www.nuget.org/packages/SourceSchemaParser)

This is a .NET library that attempts to make reading Valve's Source-engine schema files a little easier. The intention is to be able to consume .vdf and .txt (in VDF format) files into JSON and in memory objects.

## About this Library
This library was created to read various schema files in Valve's source-engine games. Schema files contain details about items, heroes, classes, and more. Unfortunately, the schema files are in VDF format, which is JSON-like, but significantly worse than JSON.

These are the rules on which the library was built:

  * Make the library as easy and user friendly as possible.
  * Document everything as thoroughly as possible.
  * Allow for conversion from VDF to JSON.
  * Replace string tokens with localized text where appropriate and possible.
  * Provide "flattened" objects from schemas such as "Items" and "Leagues" (still in progress).

## Install the Library from NuGet
See the library in the NuGet gallery [here](https://www.nuget.org/packages/SourceSchemaParser). You can install the library into your project directly from the package manager.

## How to Use the Library
  1. Read the `About` section so you understand why this library exists
  2. Install the library from NuGet
  3. Convert whatever VDF you have into JSON and use it however you want
  4. (future) Call methods to get "flattened" objects from schemas for easier consumption

## Sample Usage
```cs
// this will return a collection of schema items which describe "Dota League" specific items like league images, web page, and more
var leagues = SchemaFactory.GetDotaLeaguesFromText(schema, languageToLoad);

// this will convert VDF format to JSON from a VDF file
var vdf = File.ReadAllLines("path/to/file.vdf");
var json = VDFConverter.ToJson(vdf);

// this will get the latest dota 2 game item schema and convert it to JSON
HttpClient client = new HttpClient();
var schema = await client.GetStringAsync(schemaUrl);
var json = VDFConverter.ToJson(schema);
```
