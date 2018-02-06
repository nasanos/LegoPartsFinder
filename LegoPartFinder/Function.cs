using System;
using System.Collections.Generic;
using System.Linq;

namespace LegoPartFinder.Functions
{
    public class LegoList
    {
        public static List<string> Themes()
        {
            List<string> themeNames = new List<string>();

            using (var db = new SQLiteContext())
            {
                var selectThemes = db.Themes.ToList();

                foreach (var selectTheme in selectThemes)
                {
                    themeNames.Add(selectTheme.name);
                }
            }

            return themeNames;
        }

        public static Models.SetList SetsByTheme(string themeName)
        {
            Models.SetList thisSetList = new Models.SetList();
            List<Models.SetId> setIds = new List<Models.SetId>();

            using (var db = new SQLiteContext())
            {
                var selectThemes = db.Themes.Where(t => t.name.ToLower() == themeName.ToLower()).ToList();

                if (!selectThemes.Any())
                {
                    themeName = "No such theme is present.";
                }
                else
                {
                    var selectSets = db.Sets
                                       .Where(s => s.theme_id == selectThemes[0].id)
                                       .ToList();

                    foreach (var selectSet in selectSets)
                    {
                        Models.SetId thisSetId = new Models.SetId
                        {
                            set_name = selectSet.name,
                            set_num = selectSet.set_num,
                            part_count = selectSet.num_parts
                        };
                        setIds.Add(thisSetId);
                    }
                }

                thisSetList.theme_name = themeName;
                thisSetList.set_ids = setIds;
            }

            return thisSetList;
        }

        public static List<Models.SetPartsList> PartsBySet(string setNum)
        {
            List<Models.SetPartsList> thisPartsList = new List<Models.SetPartsList>();
            List<string> setNums = new List<string>();

            using (var db = new SQLiteContext())
            {
                var selectInvs = db.Inventories.Where(inv => inv.set_num.ToLower() == setNum.ToLower()).ToList();

                if (!selectInvs.Any())
                {
                    Models.SetId setId = new Models.SetId
                    {
                        set_num = "",
                        set_name = "No such theme is present.",
                        part_count = 0
                    };

                    Models.SetPartsList partsList = new Models.SetPartsList
                    {
                        set_id = setId
                    };

                    thisPartsList.Add(partsList);
                }
                else
                {
                    foreach (var selectInv in selectInvs)
                    {
                        var selectInvSets = db.Inventory_Sets.Where(invS => invS.inventory_id == selectInv.id);

                        if (!selectInvSets.Any())
                        {
                            setNums.Add(selectInv.set_num);
                        }
                        else
                        {
                            foreach (var selectInvSet in selectInvSets)
                            {
                                setNums.Add(selectInvSet.set_num);
                            }
                        }
                    }

                    foreach (string setNumInst in setNums)
                    {
                        var selectSet = db.Sets.Single(s => s.set_num == setNumInst);

                        Models.SetId thisSetId = new Models.SetId
                        {
                            set_num = selectSet.set_num,
                            set_name = selectSet.name,
                            part_count = selectSet.num_parts
                        };

                        var selectParts = (from inv in db.Inventories
                                           join ip in db.Inventory_Parts on inv.id equals ip.inventory_id
                                           join p in db.Parts on ip.part_num equals p.part_num
                                           join c in db.Colors on ip.color_id equals c.id
                                           where inv.set_num == setNumInst
                                           select new { part_num = p.part_num,part_name = p.name, part_color = c.name, is_spare = ip.is_spare })
                            .ToList();

                        List<Models.PartId> theseParts = new List<Models.PartId>();
                        foreach (var selectPart in selectParts)
                        {
                            Models.PartId thisPart = new Models.PartId
                            {
                                part_num = selectPart.part_num,
                                part_name = selectPart.part_name,
                                part_color = selectPart.part_color,
                                is_spare = selectPart.is_spare
                            };

                            theseParts.Add(thisPart);
                        }

                        Models.SetPartsList thisSetPart = new Models.SetPartsList
                        {
                            set_id = thisSetId,
                            parts = theseParts
                        };

                        thisPartsList.Add(thisSetPart);
                    }
                }
            }

            return thisPartsList;
        }
    }
}
