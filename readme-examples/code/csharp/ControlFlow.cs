using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TAP {
class Program {
    public static void PrintThread(string msg) =>
        Console.WriteLine(msg + ", thread: " + Thread.CurrentThread.ManagedThreadId);

    static async Task<int> ContentLengthAsync(Uri address) {
        using var client = new HttpClient();

        PrintThread("ContentLengthAsync start getStringTask");
        Task<string> getStringTask = client.GetStringAsync(address);
        PrintThread("getStringTask yielded control");

        DoSomethingUseful();

        PrintThread("awaiting getStringTask");
        string content = await getStringTask;
        PrintThread("getStringTask finished continuing");

        return content.Length;
    }

    static void DoSomethingUseful() {
        PrintThread("Doing something useful");
    }

    static async Task Main(string[] args) {
        PrintThread("Main");
        Task<int> getLengthTask = ContentLengthAsync(new Uri("https://www.fh-ooe.at/"));
        PrintThread("getLengthTask yielded control");
        int result = await getLengthTask;
        PrintThread("getLengthTask finished continuing");
    }
    /* output:      Main, thread: 1
                    ContentLengthAsync start getStringTask, thread: 1
                    getStringTask yielded control, thread: 1
                    Doing something useful, thread: 1
                    awaiting getStringTask, thread: 1
                    getLengthTask yielded control, thread: 1
                    getStringTask finished continuing, thread: 7
                    getLengthTask finished continuing, thread: 7    */
}

// class MyClass {
//     public static void PrintThread(string msg) =>
//         Console.WriteLine(msg + ", thread: " + Thread.CurrentThread.ManagedThreadId);
//
//     static async Task<int> GetContentLengthAsync(Uri address) {
//         var client = new HttpClient();
//
//         Task<string> getStringTask = client.GetStringAsync(address);
//         var uri
//
//         Task<string> HttpClient.GetStringAsync(Uri uri)
//
//         DoSomethingUseful();
//
//         string content = await getStringTask;
//
//         return content.Length;
//     }
//
//     static void DoSomethingUseful() {
//         PrintThread("Doing something useful");
//     }
// }

}