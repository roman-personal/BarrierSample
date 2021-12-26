using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrierSample {
    internal class LinearNorm1 {
        public void Execute(float[] data) {
            float min = data[0];
            float max = data[0];
            for (int i = 1; i < data.Length; i++) {
                min = Math.Min(min, data[i]);
                max = Math.Max(max, data[i]);
            }
            float delta = max - min;
            for (int i = 0; i < data.Length; i++) {
                data[i] = (data[i] - min) / delta;
            }
        }
    }
}
