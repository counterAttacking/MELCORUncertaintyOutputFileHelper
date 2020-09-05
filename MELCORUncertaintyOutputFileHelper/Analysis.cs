using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MELCORUncertaintyOutputFileHelper
{
    public class Analysis
    {
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

        public Nuclide nuclide24
        {
            set;
            get;
        }

        public Nuclide nuclide72
        {
            set;
            get;
        }

        public ReleaseFraction fraction24
        {
            set;
            get;
        }

        public ReleaseFraction fraction72
        {
            set;
            get;
        }
    }
}
