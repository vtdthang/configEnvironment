using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication5.Core
{
    public class TypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        public override T Parse(object value)
        {          
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, T value)
        {
            parameter.Value = JsonConvert.SerializeObject(value);
        }      
    }

    public class ArrayTypeHandler<T>: SqlMapper.TypeHandler<List<T>>
    {
        public override List<T> Parse(object value)
        {
            return JsonConvert.DeserializeObject<List<T>>(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, List<T> value)
        {
            parameter.Value = JsonConvert.SerializeObject(value);
        }
    }
}