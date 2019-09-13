# Font-Awesome-Links-Property-Editor
Umbraco V8 Font Awesome Links Property Editor, render multiple links with FA Icons and configurable classes.

## Example usage
```c#
    @using Newtonsoft.Json.Linq;
    @{ 
        JArray fontAwesomeIcons = JArray.Parse(benefit.Value<string>("fontAwesomeIcon"));
    }

    @if (fontAwesomeIcons.Count() > 0)
    {
        foreach(var icon in fontAwesomeIcons)
        {
            <i class="@icon["className"]"></i>
        }
    }
```

## Properties
* className - the icons class name
* svg - the icons svg path
* label - the icons label

Docs written by [Chewbacca222222](https://github.com/Chewbacca222222) :heart:
