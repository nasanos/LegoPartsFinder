using System;
using System.Collections.Generic;

namespace LegoPartFinder.Models
{
    public class SetId
    {
        public string set_name { get; set; }
        public string set_num { get; set; }
        public int part_count { get; set; }
    }

    public class PartId
    {
        public string part_num { get; set; }
        public string part_name { get; set; }
        public string part_color { get; set; }
        public string is_spare { get; set; }
    }

    public class SetList
    {
        public string theme_name { get; set; }
        public List<SetId> set_ids { get; set; }
    }

    public class SetPartsList
    {
        public SetId set_id { get; set; }
        public List<PartId> parts { get; set; }
    }
}
