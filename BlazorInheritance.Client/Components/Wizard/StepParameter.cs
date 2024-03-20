using System.Reflection;

namespace BlazorInheritance.Client.Components.Wizard
{
	[AttributeUsage(AttributeTargets.Property)]
	public class StepParameter : Attribute
	{
		public string Name { get; set; }


		private static PropertyInfo getAttributeValue(Type objectType, string id, bool useNameAsId, Type propertyType)
		{
			var properties = objectType.GetProperties();

			foreach (var property in properties)
			{
				var attributes = property.GetCustomAttributes(typeof(StepParameter), true);
				foreach (var attribute in attributes)
				{
					if (attribute is StepParameter stepParam)
					{
						if (property.GetType() == propertyType && (useNameAsId && property.Name.Equals(id)) || (!useNameAsId && stepParam.Name == id))
						{
							return property;
						}
					}
				}
			}

			return null;
		}

		public static void PassParameters(object from, object to)
		{
			var properties = from.GetType().GetProperties();
			foreach (var property in properties)
			{
				var attributes = property.GetCustomAttributes(typeof(StepParameter), true);
				foreach (var attribute in attributes)
				{
					if (attribute is StepParameter stepParam)
					{
						object? fromValue = property.GetValue(from);
						string id = stepParam.Name;
						bool useNameAsId = false;

						if (string.IsNullOrEmpty(id))
						{
							id = property.Name;
							useNameAsId = true;
						}

						PropertyInfo toProperty = getAttributeValue(to.GetType(), id, useNameAsId, property.GetType());
						if (toProperty != null)
						{
							toProperty.SetValue(to, fromValue);
						}
					}
				}
			}
		}
	}

}
