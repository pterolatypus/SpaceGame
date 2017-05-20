using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class EmptyOrbital : Orbital {

    public EmptyOrbital(int seed) : base(seed) { }

    public override void Load() {}
}
