using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BarrierSample {
    internal class LinearNorm2 {
        const int numberOfProcessors = 12;
        float[] minValues;
        float[] maxValues;
        Barrier barrier;

        public void Execute(float[] data) {
            minValues = new float[numberOfProcessors];
            maxValues = new float[numberOfProcessors];
            using (barrier = new Barrier(numberOfProcessors)) {
                var tasks = CreateDataProcessors(data);
                Task.WaitAll(tasks);
            }
        }

        Task[] CreateDataProcessors(float[] data) {
            var tasks = new List<Task>();
            for (int i = 0; i < numberOfProcessors; i++)
                tasks.Add(ProcessData(data, i));
            return tasks.ToArray();
        }

        Task ProcessData(float[] data, int index) {
            int len = data.Length;
            int count = len / numberOfProcessors;
            return Task.Run(() => {
                int start = count * index;
                int end = index == numberOfProcessors - 1 ? len : (start + count);
                float min = data[start];
                float max = data[start];
                for (int i = start + 1; i < end; i++) {
                    min = Math.Min(min, data[i]);
                    max = Math.Max(max, data[i]);
                }
                minValues[index] = min;
                maxValues[index] = max;
                barrier.SignalAndWait();
                min = minValues.Min();
                max = maxValues.Max();
                float delta = max - min;
                for (int i = start; i < end; i++) {
                    data[i] = (data[i] - min) / delta;
                }
            });
        }
    }
}
