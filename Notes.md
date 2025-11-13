

## Tasks vs Threads

var t1 = new Thread(() => {work});
var t2 = new Thread(() => {work});

t1.Start();
t2.Start();
t1.Join();
t2.Join();

VERSUS
var t1 = Task.Run(() => {work});
var t2 = Task.Run(() => {work});

Task.WaitAll(t1, t2);


BUT also:
Task.WhenAll(t1, t2); // does not block the calling thread (return or fire and forget)
await Task.WhenAll(t1, t2); // non blocking, returns to that spot when complete


Unclear why, but according to chatGPT:
Threads are better for CPU-bound operations, and
Tasks are better for I/O-bound operations.


## lock (object) vs Interlock 

Interlocked quicker, but only for simple operations
lock allows for transactions e.g., bank transfer where add and subtract must be atomic



