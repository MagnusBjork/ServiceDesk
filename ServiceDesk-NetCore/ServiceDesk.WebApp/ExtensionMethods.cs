using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceDesk.WebApp.Services;

namespace ServiceDesk.WebApp
{
    public static class ExtensionMethods
    {

        public static List<SelectListItem> ToSelectedItems(this IList<OptionSetValue> values)
        {
            return values.Select(o => new SelectListItem()
            {
                Text = o.Text,
                Value = o.Value,
                Disabled = !o.Enabled
            }).ToList();
        }
    }

}