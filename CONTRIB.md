Contributing
--------------------------

- Please submit PRs from a personal Fork

- Use PascalCasing not camelCasing for member names, converting to 
  camel casing via DataMember(Name="camelCasing") or 
  EnumMember(Value="camelCasing")

- For non-C# compatible names (with spaces or colons, etc.), remove 
  the character for the C# name and specify it in the 
  Data/EnumMember attribute (see Scopes.cs as an example)

- Please copy any class and member documentation text available into 
  xmldoc as appropriate (see existing code for an example - 
  typically this is on GitHubApi classes)

- Enums need a CustomEnumValueSerializer added in JsonExtensions.Init()

- Enums used as parameters in operation calls should have a 
  FooEnumTypeExtensions class with a ToParameterValue method in the 
  same file as the Enum (see RepositoryTypes as an example)
