using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Vsol.Api.Shared.Domain
{
    public class EntityInfo<T> where T : new()
    {
        public T Map(object a, T b)
        {
            //var b = new T();

            foreach (PropertyInfo propA in a.GetType().GetProperties())
            {
                PropertyInfo propB = b.GetType().GetProperty(propA.Name);

                var Value = propA.GetValue(a, null);

                if (Value != null)
                    propB.SetValue(b, Value, null);
            }

            return b;
        }
    }
}
