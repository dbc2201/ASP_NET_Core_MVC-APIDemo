using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Dictionary<string, string> _items = new();

        public ValuesController()
        {
            _items["key1"] = "value1";
            _items["key2"] = "value2";
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            if (_items.ContainsKey(id) == false)
                return NotFound();

            return _items[id];
        }

        [HttpPost]
        public IActionResult Post(Entry entry)
        {
            if (_items.ContainsKey(entry.Key)) return BadRequest();

            _items.Add(entry.Key, entry.Value);
            return CreatedAtAction(nameof(Get), new {id = entry.Key}, entry);
        }
    }

    public class Entry
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}