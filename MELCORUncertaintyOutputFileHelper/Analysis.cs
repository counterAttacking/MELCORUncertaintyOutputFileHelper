using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MELCORUncertaintyOutputFileHelper
{
    public class Analysis
    {
        public Nuclide nuclide24;
        public Nuclide nuclide72;
        public ReleaseFraction fraction24;
        public ReleaseFraction fraction72;

        public Analysis()
        {
            this.nuclide24 = new Nuclide();
            this.nuclide72 = new Nuclide();
            this.fraction24 = new ReleaseFraction();
            this.fraction72 = new ReleaseFraction();
        }

        public string name
        {
            set;
            get;
        }

        public double samg
        {
            set;
            get;
        }

        public double fpRelease
        {
            set;
            get;
        }

        public double availTime
        {
            set;
            get;
        }
    }
}
