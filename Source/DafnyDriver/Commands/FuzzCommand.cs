using System.Collections;
using System.CommandLine;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;


namespace Microsoft.Dafny;

class FuzzCommand : ICommandSpec {
  public IEnumerable<Option> Options => new Option[] {
      BoogieOptionBag.BoogieFilter,
    }.Concat(ICommandSpec.VerificationOptions).Concat(ICommandSpec.ConsoleOutputOptions)
    .Concat(ICommandSpec.ResolverOptions);

  public Command Create() {
    var result = new Command("fuzz", "Fuzz the given program");
    result.AddArgument(ICommandSpec.FilesArgument);
    return result;
  }

  public void PostProcess(DafnyOptions dafnyOptions, Options options, InvocationContext context) {
    dafnyOptions.Compile = false;
  }
}
