using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatorAPI.Models
{
    public class ItemTypeViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? Type { get; set; }
        public string IndemnityPrompt { get; set; }
        public string IndemnityText { get; set; }
        public List<ProductInputAttribute> InputAttributes { get; set; }
    }

    public class ProductInputAttribute
    {
        public string SystemCode { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public List<TableValue> Table { get; set; }
    }

    public class PackageViewModel
    {
        public int PackageID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<ItemTypeViewModel> Items { get; set; }
    }
}