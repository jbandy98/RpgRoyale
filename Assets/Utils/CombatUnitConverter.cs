using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

public class CombatUnitConverter : JsonConverter
{
    static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings()
    {
        ContractResolver = new CombatUnitClassConverter()
    };

    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(CombatUnit));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        int choice = 0;
        try
        {
            choice = jo["ObjType"].Value<int>();
        } catch (ArgumentNullException e)
        {
            choice = 1;
        }
        switch (choice)
        {
            case 1:
                return JsonConvert.DeserializeObject<HeroUnit>(jo.ToString(), SpecifiedSubclassConversion);
            case 2:
                return JsonConvert.DeserializeObject<EnemyUnit>(jo.ToString(), SpecifiedSubclassConversion);
            default:
                throw new Exception();
        }
        throw new NotImplementedException();
    }

    public override bool CanWrite
    {
        get { return false; }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException(); // won't be called because canwrite is false
    }
}
