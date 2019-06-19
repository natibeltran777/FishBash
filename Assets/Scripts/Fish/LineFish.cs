using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    public class LineFish : SimpleFish
    {
        //Sets pattern to a sine wave
        new void Start()
        {
            base.Start();
            pattern = FishMovement.Line;
        }

    }
}
