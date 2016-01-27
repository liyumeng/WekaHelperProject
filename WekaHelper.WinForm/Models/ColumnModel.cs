using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WekaHelper.WinForm.Models
{
    public class ColumnModel
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public string Example { get; set; }

        //该列值的候选集合，当Type为Nominal标称型时有效
        public HashSet<String> ValueSet { get; set; }

        public bool HasChosen { get; set; }

        public ColumnModel(string name = null)
        {
            Name = name;
            HasChosen = true;
            Type = ColumnType.Nominal;
            ValueSet = new HashSet<string>();
        }

    }
}
