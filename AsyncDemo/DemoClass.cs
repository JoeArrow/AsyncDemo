using System.Threading.Tasks;
using System.Collections.Generic;

namespace AsyncDemo
{
    public class DemoClass
    {
        // ---------------------------------
        // Note: This is NOT an Async method

        public int RunDemo(List<int> data)
        {
            // --------------------------
            // No await is happening here

            var retVal = RunListAsync(data);
            return retVal.Result;
        }

        // ------------------------------------------------
        // This is the first async method

        public async Task<int> RunListAsync(List<int> data)
        {
            var retVal = 0;

            // ------------------------------------------
            // List of Tasks, so that each can be awaited

            var res = new List<Task<int>>();

            foreach(var datum in data)
            {
                // -----------------------------------
                // Build a list of Tasks, not integers

                res.Add(SlowMethodAsync(datum));
            }

            // ----------------------------------------
            // Await all of the Tasks added to our list

            foreach(var task in await Task.WhenAll(res))
            {
                retVal += task;
            }

            return retVal;
        }

        // ------------------------------------------------
        // This is awaited within the first async method.
        // So, it must be async as well.

        public async Task<int> SlowMethodAsync(int sec)
        {
            var retVal = 0;

            // -------------------------
            // Task.Delay() is awaitable

            await Task.Delay(sec * 1000);
            retVal = sec * 10;

            return retVal;
        }
    }
}
