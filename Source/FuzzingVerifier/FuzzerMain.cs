using Microsoft.Dafny;

namespace FuzzingVerifier;
  
public class Fuzzer {
  public static int Main(string[] args) {
    Console.WriteLine("This is the fuzzer");
    var ret = DafnyDriver.ThreadMain(args);
    return ret;
  }

}