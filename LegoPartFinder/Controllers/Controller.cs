using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LegoPartFinder.Controllers
{
    [Route("api/[controller]")]
    public class ThemesController : Controller
    {
        [HttpGet]
        public List<string> Get()
        {
            List<string> themeList = Functions.LegoList.Themes();

            return themeList;
        }
    }

    [Route("api/[controller]")]
    public class SetsByThemeController : Controller
    {
        [HttpGet("{themeName}")]
        public object Get(string themeName)
        {
            Models.SetList thisSetList = Functions.LegoList.SetsByTheme(themeName);
                     
            return thisSetList;
        }
    }

    [Route("api/[controller]")]
    public class PartsBySetController : Controller
    {
        [HttpGet("{setNum}")]
        public object Get(string setNum)
        {
            List<Models.SetPartsList> thisPartsList = Functions.LegoList.PartsBySet(setNum);

            return thisPartsList;
        }
    }
}
