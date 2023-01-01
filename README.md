## 🎓 Scraperor-v2 [scraping service]

You already know what this is

## 💡 Usage
##### ENDPOINT
```url
{{POST}}: ($DOMAIN)/api/scrape
```
### Example
##### Example body -> (Extract the bio-text from github user page)
```json
{
    "website": "https://github.com/Thiti-Dev",
    "pointer": {
        "look_for": {
            "tag": "div",
            "has_classes": [
                "user-profile-bio"
            ],
            "then_look_for": {
                "tag": "div"
            }
        }
    }
}
```

###### Response
```json
{
    "success": true,
    "contents": [
        "My github's bio, it can be any as I can change it anytime lol but for now at this commit date it was `I'm backkkk`",
    ]
}
```


##### Example body -> (Extract the definition from the longdo dict with the word ```kind```)
```json
{
    "website": "https://dict.longdo.com/search/kind",
    "pointer": {
        "look_for": {
            "tag": "tr",
            "has_classes": ["lang-rows","lang-TH"],
            "then_look_for": {
                "tag": "table",
                "has_classes": [
                    "search-result-table"
                ],
                "then_look_for": {
                    "tag": "td",
                    "then_look_for": {
                        "tag": "a"
                    }
                }
            }
        }
    }
}
```

###### Response
```json
{
    "success": true,
    "contents": [
        "ใจบุญ",
        "เกื้อกูล",
        "เมตตา",
        "กรุณา"
    ]
}
```

## 📕 CookBook
- The ```then_look_for``` prop can be nested infinitely
- you can exclude the tag property if you are intending to look for (*)wildcard tag element
- These 2 is in implementation backlog (too lazy for now, feel free to open PRs)
   - Custom Attribute-$LOOKUP
   - ID-$LOOKUP
