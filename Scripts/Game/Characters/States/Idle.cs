using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Characters.States
{
    internal abstract class Idle : State
    {
        internal static readonly string name = "Idle";
        internal override string Name { get { return name; } }
    }
}
