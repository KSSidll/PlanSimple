﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace PlanSimple.HelperClasses;

public static class EnumHelper
{
	public static string? Description(this Enum value)
	{
		var attributes = value.GetType().GetField(value.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), false);
		if (attributes != null && attributes.Any())
			return (attributes.First() as DescriptionAttribute)?.Description;

		TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
		return ti.ToTitleCase(value.ToString());
	}

	public static List<Tuple<Enum, string?>> GetAllValuesAndDescriptions(Type t)
	{
		if (!t.IsEnum)
			throw new ArgumentException($"{nameof(t)} must be an enum type");
		return Enum.GetValues(t).Cast<Enum>().Select(e => new Tuple<Enum, string?>(e, e.Description())).ToList();
	}
}