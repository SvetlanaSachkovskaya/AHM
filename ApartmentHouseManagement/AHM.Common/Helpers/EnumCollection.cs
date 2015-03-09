using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using AHM.Common.Extensions;

namespace AHM.Common.Helpers
{
    public class EnumCollection<T> : List<EnumItem> where T : struct
    {
        public EnumCollection(params T[] entities)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("This class only supports Enum types");
			}

	        var addItem = new Action<FieldInfo>(field =>
	        {
				var atts = field.GetCustomAttributes(false).OfType<DisplayAttribute>().FirstOrDefault();
                var entity = new EnumItem
				{
					Id = (int)field.GetValue(null),
					Name = atts == null ? field.Name : atts.Name
				};

				Add(entity);
	        });

			var fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
	        if (entities != null && entities.Length > 0)
	        {
		        foreach (var entity in entities)
		        {
			        var field = fields.FirstOrDefault(f => f.Name == entity.ToString());
			        if (field != null)
			        {
				        addItem(field);
			        }
		        }
	        }
	        else
	        {
				foreach (var field in fields)
				{
					addItem(field);
				}
	        }
        }
    }
}