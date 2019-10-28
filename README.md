# Samples on how to extend Newtonsoft.Json for custom object mappings

I will show you several different techniques on how to map properties that have different names in our object and on JSON side.
Obviously we could just use [JsonProperty attribute](https://www.newtonsoft.com/json/help/html/JsonPropertyName.htm), but there are cases where:
* We do not want to have a Newtonsoft.Json dependecy in our data project.
* We need to serialize the same data to diferent jsons - for example we might have a configuration object that we need to send to both web API and store localy in json file, and property mappings are different between those.
* We need to combine multiple objects into one json object.
* Object(s) and json are structured differently.
* We need to hide certain properties from serialization/deserialization, but we do not want to use any attributes for that.

## Using a dictionary to map properties

First example is very simple and it works only if you need to map one object.

Imagine we have json:
```json
{
	'employee_name': 'Bob',
	'employee_last_name': 'Smith',
	'birthday': '1999-01-01T00:00:01'
}
```

Our C# object is like this:
```C#
public class Person
{
	public string Name { get; set; }
	public string LastName { get; set; }
	public DateTime Birthday { get; set; }
}
```

We can create custom Contract Resolver, which will use dictionary to specify mappings:
```C#
public class PersonContractResolver : BasicContractResolver {

	public override Dictionary<string, string> PropertyMapping => new Dictionary<string, string> {
		// use nameof() to not break mapping when refactoring objects
		{ nameof(Person.Name),     "employee_name" },
		{ nameof(Person.LastName), "employee_last_name" },
		{ nameof(Person.Birthday), "birthday" }
	};
}
```
BasicContractResolver extends DefaultContractResolver and is very simple:
```C#
public abstract class BasicContractResolver : DefaultContractResolver {

	/// <summary>
	/// Mapping between Class Property Name and JSON Property Name
	/// </summary>
	public abstract Dictionary<string, string> PropertyMapping { get; }

	/// <summary>
	/// By default ignores properties that are not in the PropertyMapping
	/// </summary>
	public virtual bool IgnoreNotMapped { get; } = true;

	protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization) {
		var property = base.CreateProperty(member, memberSerialization);
		if (PropertyMapping.TryGetValue(member.Name, out string name)) {
			property.PropertyName = name;
		} else if (IgnoreNotMapped) {
			property.Ignored = true;
		}
		// else we could throw error
		return property;
	}
}
```

So now we could read/deserialize JSON this way:
```C#
Person person = JsonConvert.DeserializeObject<Person>(json, new JsonSerializerSettings {
		ContractResolver = new PersonContractResolver()
	});
```

or write/serialize:
```C#
var json = JsonConvert.SerializeObject(person, new JsonSerializerSettings {
		ContractResolver = new PersonContractResolver()
	});
```

## To be continued